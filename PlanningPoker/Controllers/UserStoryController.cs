using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Action.Interface;
using PlanningPoker.Common;


namespace PlanningPoker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStoryController : ControllerBase
    {
        private readonly IUserStoryservice _userStoryService;
        public UserStoryController(IUserStoryservice userStoryService)
        {
            _userStoryService = userStoryService;
        }

        [HttpPost("import")]
        public IActionResult Create(IFormFile formFile, int gameId, int userId)
        {
            try
            {
                _userStoryService.Import(formFile, gameId, userId);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("GetUserStory")]
        public IActionResult GetByGameId(long gameId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var story = _userStoryService.GetByGameId(gameId, pageIndex, pageSize);
                return StatusCode(StatusCodes.Status200OK, story);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("SearchUserStory")]
        public IActionResult SearchStory(long gameId, string story, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var result = _userStoryService.SearchStory(gameId, story, pageIndex, pageSize);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("GetUserStoryById")]
        public IActionResult GetByStoryId(long userstoryId)
        {
            try
            {
                var story = _userStoryService.GetByUserStoryId(userstoryId);
                return StatusCode(StatusCodes.Status200OK, story);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}