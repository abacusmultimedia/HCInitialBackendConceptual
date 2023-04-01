using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using zero.Shared.Response;
using DAO.PlanningPortal.Application.Interfaces.Finance;
using DAO.PlanningPortal.Application.Interfaces.Inventory;
using zero.Shared.Models.GDPRAccess;
using ERP.API.Common.Controller;

namespace DAO.PlanningPortal.API.Controllers;

[Route("/api/Lookups")]
public class LookupsController : BaseApiController
{
    public LookupsController(IInventoryService appService, ILedgerService ledgerService)
    {
        _appService = appService;
        _ledgerService = ledgerService;
    }

    public IInventoryService _appService { get; }
    public ILedgerService _ledgerService { get; }

    [HttpGet]
    [Route("itemGroups")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
    public async Task<ActionResult<List<LookupDto>>> ItemGroup()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _appService.ItemGroup_GetLookups();
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
    [Route("itemGroupsChildOnly")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
    public async Task<ActionResult<List<LookupDto>>> itemGroupsChildOnly()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _appService.ItemGroupsChildOnly_GetLookups();
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
    [Route("items")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
    public async Task<ActionResult<List<LookupDto>>> Item()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _appService.Item_GetLookups();
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
    [Route("ItemsBatch")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
    public async Task<ActionResult<List<LookupDto>>> ItemsBatch()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _appService.Item_BatchGetLookups();
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
    [Route("Ledgers")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
    public async Task<ActionResult<List<LookupDto>>> Ledgers()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _ledgerService.GetLookups("");
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
    [Route("Vender")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
    public async Task<ActionResult<List<LookupDto>>> Vender()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _ledgerService.GetLookups("Venders");
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
    [Route("Customer")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
    public async Task<ActionResult<List<LookupDto>>> Customer()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _ledgerService.GetLookups("Customers");
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

