using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Common;
using PlanningPoker.DataContract.Request;
using PlanningPoker.WebApi.Domain;
using PlanningPoker.WebApi.Domain.Models;
using PlanningPoker.WebApi.Services;
using PlanningPoker.WebApi.Services.Interfaces;
using static PlanningPoker.Common.Enums;

namespace PlanningPoker.WebApi.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly Action.Interface.IRoleService _roleService;
        private readonly IMapper _mapper;
        public RolesController(Action.Interface.IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [Route("GetallUsersInfo")]
        [HttpGet]
        public IActionResult GetallUsersInfo(Status status, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var users = _roleService.GetAllUsers(status, pageIndex, pageSize);
                return StatusCode(StatusCodes.Status200OK, users);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("SearchByName")]
        public IActionResult GetUsersByName(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var users = _roleService.GetUserByName(pageIndex, pageSize);
                return StatusCode(StatusCodes.Status200OK, users);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("UpdateUserRole")]
        public IActionResult UpdateUserRole(IList<UpdateUserRoleRequest> model)
        {
            try
            {
                var userRole = _roleService.UpdateUserRole(model);
                return StatusCode(StatusCodes.Status200OK, userRole);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}

