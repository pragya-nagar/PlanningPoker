using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlanningPoker.WebApi.Services;
using PlanningPoker.WebApi.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

using PlanningPoker.Action.Interface;

namespace PlanningPoker.WebApi.Middleware
{
    public class Utilities : IUtilities
    {
        private readonly AppSettings _appSettings;
        private readonly IResetPasswordService _resetPasswordService;
        private readonly IEmailSender _emailSender;
        public Utilities(IOptions<AppSettings> appSettings, IResetPasswordService resetPasswordService, IEmailSender emailSender)
        {
            _appSettings = appSettings.Value;
            _resetPasswordService = resetPasswordService;
            _emailSender = emailSender;
        }
        public string GenerateResetToken(long id, string email)
        {

            byte[] keyForHmacSha256 = new byte[64];

            var securityKey = keyForHmacSha256;


            var tokenHandler = new JwtSecurityTokenHandler();
            if (_appSettings != null)
            {
                var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
                securityKey = key;
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
      {
                   new Claim(ClaimTypes.Name,id.ToString()),
                   new Claim(ClaimTypes.Email, email)
      }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            _resetPasswordService.AddForUser(id, email, tokenString);

            return tokenString;
        }
        public string ResetMailAsync(long id, string email)
        {
            string sent = string.Empty;
            string resetToken = GenerateResetToken(id, email);
            sent = _emailSender.SendEmail(email, "botcardadmin@compunnel.net", "Account password reset", "https://localhost:44315/api/Login/" + resetToken);
            return sent;
        }
        public string GetSubFromToken(string token)
        {
            string email = string.Empty;
            var getToken = _resetPasswordService.GetToken(token);

            if (getToken != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                email = jwtToken.Claims.First(claim => claim.Type == "email").Value;
            }
            return email;
        }
    }
}
