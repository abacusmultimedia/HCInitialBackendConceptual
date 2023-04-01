using ERP.API.Common.Controller;
using DAO.PlanningPortal.Application.Interfaces;
using zero.Shared.Models;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.User;
using zero.Shared.Response;
using DAO.PlanningPortal.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.API.Controllers
{
    [Route("/api/User")]
    public class UserController : BaseApiController
    {
        public UserController(IUserAppService appService)
        {
            _appService = appService;
        }

        private readonly IUserAppService _appService;

        [HttpPost, Route("GetAllUsers")]
        //[Authorize(Policy = AuthPolicyName.AdminPolicy) ]
        [ProducesDefaultResponseType(typeof(ServiceResponse<PaginatedList<ApplicationUserDto>>))]
        public async Task<ActionResult<PaginatedList<ApplicationUserDto>>> GetAllUsers(UserSearchDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }

            var response = await _appService.GetAllUsers(request);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

 

        [HttpPost, Route("GetAllUsersLookup")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<bool>> GetAllUsersLookup()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }

            var response = await _appService.GetUserLookUp();
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }


        [HttpGet, Route("GetUserById")]
        [Authorize(Policy = AuthPolicyName.AdminPolicy)]
        [ProducesDefaultResponseType(typeof(ServiceResponse<ApplicationUserDto>))]
        public async Task<ActionResult<ApplicationUserDto>> GetUserById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }

            var response = await _appService.GetUserById(id);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPost, Route("CreateUser")]
        [Authorize(Policy = AuthPolicyName.AdminPolicy)]
        [ProducesDefaultResponseType(typeof(ServiceResponse<string>))]
        public async Task<ActionResult<string>> CreateUser([FromBody] CreateUserRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }

            var response = await _appService.CreateUser(request);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPost, Route("UpdateUser")]
        [Authorize(Policy = AuthPolicyName.AdminPolicy)]
        [ProducesDefaultResponseType(typeof(ServiceResponse<string>))]
        public async Task<ActionResult<string>> UpdateUser([FromBody] UpdateRequestUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }

            var response = await _appService.UpdateUser(request);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPost, Route("DeleteUser")]
        [Authorize(Policy = AuthPolicyName.AdminPolicy)]
        [ProducesDefaultResponseType(typeof(ServiceResponse<string>))]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var response = await _appService.DeleteUser(id);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPost, Route("ToggleActiveStatus")]
        [Authorize(Policy = AuthPolicyName.AdminPolicy)]
        [ProducesDefaultResponseType(typeof(ServiceResponse<string>))]
        public async Task<ActionResult<string>> ToggleActiveStatus(int id)
        {
            var response = await _appService.ToggleActiveStatus(id);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpGet, Route("GetAllRoles")]
        //[Authorize(Policy = AuthPolicyName.AdminPolicy)]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<ApplicationRoleDto>>))]
        public async Task<ActionResult<List<ApplicationRoleDto>>> GetAllRoles()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }

            var response = await _appService.GetAllRoles();
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }
    }
}