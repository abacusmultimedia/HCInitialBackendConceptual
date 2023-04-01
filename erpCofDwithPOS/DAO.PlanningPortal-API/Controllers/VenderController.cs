using ERP.API.Common.Controller;
using DAO.PlanningPortal.Application.Interfaces;
using DAO.PlanningPortal.Application.Interfaces.Finance;
using zero.Shared.Models.Finance;
using zero.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.API.Controllers.CustomerVender;
[Route("/api/Vender")]
public class VenderController : BaseApiController
{
    public VenderController(IVenderService appService)
    {
        _appService = appService;
    }

    public IVenderService _appService { get; }

    [HttpPost]
    [Route("AddEdit")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<bool>> AddEditCustomer(VenderDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _appService.AddEdit(model);
        if (response.Succeeded)
        {
            return Ok(response.Data);
        }
        else
        {
            return BadRequest(response.Errors);
        }
         
    }
    [HttpGet]
    [Route("Get")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<VenderDTO>>))]
    public async Task<ActionResult<List<VenderDTO>>> Get()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        } 
        var response = await _appService.GetList();
        if (response.Succeeded)
        {
            return Ok(response.Data);
        }
        else
        {
            return BadRequest(response.Errors);
        }

    }
    [HttpGet]
    [Route("GetById/{id}")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<VenderDTO>> GetByID(long id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = new VenderDTO();
        return response;
    }
    [HttpDelete]
    [Route("Delete/{id}")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<bool>> Delete(long id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        return false;
    }
}

