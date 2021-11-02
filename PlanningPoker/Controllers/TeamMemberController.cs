using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Action.Interface;
using PlanningPoker.Common;
using static PlanningPoker.Common.Enums;

namespace PlanningPoker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly IInviteUserService _inviteUserService;
        public TeamMemberController(IInviteUserService inviteUserService)
        {
            _inviteUserService = inviteUserService;
        }

        [HttpGet("GetTeamMemberGameDetails")]
        public IActionResult GetTeamMemberGameDetails(UserAcceptance acceptance, long userid, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var result = _inviteUserService.GetTeamMembers(acceptance, userid, pageIndex, pageSize);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

        }
    }
}