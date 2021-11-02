using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Action.Interface;
using PlanningPoker.Common;
using PlanningPoker.DataContract.Request;

namespace PlanningPoker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStoryEstimationController : ControllerBase
    {
        private readonly IUserStoryEstimationService _userStoryEstimationService;
        public UserStoryEstimationController(IUserStoryEstimationService userStoryEstimationService)
        {
            _userStoryEstimationService = userStoryEstimationService;
        }
        [HttpPost]
        public IActionResult UserStoryEstimationDetails(UserEstimationRequest model, long UserId, long GameId, long UserStoryId)
        {
            try
            {
                var result = _userStoryEstimationService.UserStoryEstimationDetails(model, UserId, GameId, UserStoryId);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
