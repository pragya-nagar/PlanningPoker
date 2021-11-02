using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlanningPoker.WebApi.Domain.Models;
using PlanningPoker.WebApi.Middleware;
using PlanningPoker.Common;
using PlanningPoker.Action.Interface;
using PlanningPoker.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using static PlanningPoker.Common.Enums;
using Microsoft.Extensions.Logging;

namespace PlanningPoker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly Services.AppSettings _appSettings;
        //private readonly IEmailSender _emailSender;
        private readonly IResetPasswordService _resetPasswordService;
        private readonly IUtilities _utilities;
        private readonly IUserSessionService _userSessionService;
        private readonly ILogger _logger;

        public UserLoginController(
            IUtilities utilities,
            IUserService userService,
            IMapper mapper,
            IOptions<Services.AppSettings> appSettings,
            IResetPasswordService resetPasswordService,
            IUserSessionService userSessionService,
            ILogger<UserLoginController> logger)
        //IEmailSender emailSender)
        {
            _logger = logger;
            _utilities = utilities;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            //_emailSender = emailSender;
            _resetPasswordService = resetPasswordService;
            _userSessionService = userSessionService;
        }

        [Route("SendResetMail")]
        [HttpGet]
        public dynamic SendResetMail(string email)
        {
            try
            {
                dynamic result;
                var getUser = _userService.GetByEmail(email);
                var userId = getUser.UserId;
                if (userId != 0)
                {
                    string sent = _utilities.ResetMailAsync(userId, email);
                    result = new
                    {
                        code = ReturnCodes.DataGetSucceeded,
                        data = sent,
                    };
                }
                else
                {
                    result = new
                    {
                        code = ReturnCodes.DataGetFailed,
                        data = "Fail",
                    };
                }
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("UpdatePasswordOnMail")]
        public dynamic ResetPassword(long id, string token, [FromBody]ResetPasswordRequest model)
        {
            try
            {
                dynamic result;
                string tokenEmail = _utilities.GetSubFromToken(token);
                var user = _mapper.Map<User>(model);

                if ((!string.IsNullOrEmpty(tokenEmail)))
                {
                    bool reset = _userService.UpdatePasswordByEmail(id, model.Password, token);
                    result = new
                    {
                        code = reset ? ReturnCodes.DataUpdateSucceeded : ReturnCodes.DataUpdateFailed,
                    };
                }
                else
                {
                    result = new
                    {
                        code = ReturnCodes.DataUpdateFailed,
                    };

                }
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]ExtLoginRequest model)
        {
            try
            {
                var user = _userService.Authenticate(model.Email, model.Password);
                byte[] keyForHmacSha256 = new byte[64];

                var securityKey = keyForHmacSha256;
                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                var tokenHandler = new JwtSecurityTokenHandler();
                if (_appSettings != null)
                {
                    var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
                    securityKey = key;
                }
                string authorization = Request.Headers["Authorization"];
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role,((RoleName)user.RoleId).ToString())
                }, JwtBearerDefaults.AuthenticationScheme),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature)
                };
                _logger.LogInformation("User is authenticated");
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var userSession = _userSessionService.Create(user.UserId, tokenString);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    Id = user.UserId,
                    Username = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleName = ((RoleName)user.RoleId).ToString(),
                    Token = tokenString
                });
            }

            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserInsertRequest model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                var names = model.FullName.Split(' ');

                string firstName = names[0].ToLower();
                string lastName = names[1].ToLower();

                _userService.Create(user, model.Password, firstName, lastName);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [Route("GetAllUsers")]
        [HttpGet]
        public IActionResult GetAllUsers(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var users = _userService.GetAll(pageIndex, pageSize);
                return StatusCode(StatusCodes.Status200OK, users);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Route("GetUserById")]
        [HttpGet]
        public IActionResult GetUserById(long id)
        {
            try
            {
                var user = _userService.GetById(id);
                var model = _mapper.Map<RegUserResponse>(user);
                return StatusCode(StatusCodes.Status200OK, model);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [Route("DeleteUserById")]
        [HttpDelete]
        public IActionResult DeleteUserById(long id)
        {
            try
            {
                _userService.Delete(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("Logout")]
        public IActionResult Logout(long userId, string token)
        {
            try
            {
                var userSession = _userSessionService.Update(userId, token);
                return StatusCode(StatusCodes.Status200OK, userSession);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
