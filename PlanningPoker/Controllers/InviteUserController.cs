using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Action.Interface;
using PlanningPoker.Common;
using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;

namespace PlanningPoker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InviteUserController : ControllerBase
    {
        private readonly IInviteUserService _inviteUserService;
        public InviteUserController(IInviteUserService inviteUserService)
        {
            _inviteUserService = inviteUserService;
        }

        [HttpPost("InviteUser")]
        public IActionResult InviteUser(IList<InviteUserResponse> model, long gameId, string datetime, long userId)
        {
            try
            {
                var sendEmail = _inviteUserService.InviteUsers(model, gameId, datetime, userId);
                return StatusCode(StatusCodes.Status200OK, sendEmail);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("GetUsersByGameId")]
        public IActionResult GetUsersByGameId(long gameId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var users = _inviteUserService.GetUsersByGameId(gameId, pageIndex, pageSize);
                return StatusCode(StatusCodes.Status200OK, users);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("UserAcceptance")]
        public IActionResult Update(long gameid, long userId, [FromBody]InviteUserRequest model)
        {
            try
            {
                var result = _inviteUserService.UpdateUser(gameid, userId, model);
                return Ok(result);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}