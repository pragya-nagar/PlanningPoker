using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlanningPoker.Action.Interface;
using PlanningPoker.Common;
using PlanningPoker.DataContract.Request;
using PlanningPoker.Domain.Entities;

namespace PlanningPoker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
     
        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }
        [Authorize(Roles = "ScrumMaster")]
        [HttpPost("CreateGame")]
        public IActionResult CreateGame([FromBody]GameInsertRequest GameInsertRequest)
        {
            try
            {
                var game = _mapper.Map<Game>(GameInsertRequest);
                game.CreatedBy = GameInsertRequest.UserId;
                game.CreatedOn = DateTime.Now;
                game.UpdatedBy = GameInsertRequest.UserId;
                game.UpdatedOn = DateTime.Now;
                game.RowGuid = Guid.NewGuid();
                var games = _gameService.Create(game);
                if (games is null)
                    return StatusCode(StatusCodes.Status400BadRequest, ErrorMessage.GameAddUpdateError);
                else
                    return StatusCode(StatusCodes.Status200OK, games);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "ScrumMaster")]
        [Route("GetAllGames")]
        [HttpGet]
        public IActionResult GetGame(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var game = _gameService.GetGames(pageIndex, pageSize);
                return StatusCode(StatusCodes.Status200OK, game);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "ScrumMaster")]
        [HttpGet("{id}")]
        public IActionResult GetGameById(long id)
        {
            try
            {
                var game = _gameService.GetById(id);
                return StatusCode(StatusCodes.Status200OK, game);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

        }

        [Authorize(Roles = "ScrumMaster")]
        [HttpGet("GetScrumMasterGameDetailsById")]
        public IActionResult GetGameDetailsById(long ScrumMasterId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var gameDetails = _gameService.GetGameDetails(ScrumMasterId, pageIndex, pageSize);
                return StatusCode(StatusCodes.Status200OK, gameDetails);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
