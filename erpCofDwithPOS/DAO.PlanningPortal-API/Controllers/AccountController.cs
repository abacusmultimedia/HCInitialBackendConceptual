 
using DAO.PlanningPortal.Application.Interfaces;
using zero.Shared.Models.Security;
using zero.Shared.Response;
using DAO.PlanningPortal.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ERP.API.Common.Controller;

namespace DAO.PlanningPortal.API.Controllers;

[Route("/api/Account")]
public class AccountController : BaseApiController
{
    public AccountController(IAccountAppService appService)
    {
        _appService = appService;
    }

    public IAccountAppService _appService { get; }

    [HttpPost]
    [Route("ChangePassword")]
    [Authorize(Policy = AuthPolicyName.AdminUserPolicy)]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePasswordDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }

        var response = await _appService.ChangePassword(model);
        if (response.Succeeded)
        {
            return Ok(response.Data);
        }
        else
        {
            return BadRequest(response.Errors);
        }
    }


    [HttpPost]
    [Route("RegisterUser")]
    [Authorize(Policy = AuthPolicyName.AdminUserPolicy)]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<bool>> RegisterUser([FromBody] ChangePasswordDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }

        var response = await _appService.ChangePassword(model);
        if (response.Succeeded)
        {
            return Ok(response.Data);
        }
        else
        {
            return BadRequest(response.Errors);
        }

    }
    [HttpPost]
    [Route("Login")] 
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse<TokenResponse>))]

    public async Task<ActionResult<TokenResponse>> Login([FromBody] UserLogin model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }

        var response = await _appService.LoginUser(model, HttpContext.Request);
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