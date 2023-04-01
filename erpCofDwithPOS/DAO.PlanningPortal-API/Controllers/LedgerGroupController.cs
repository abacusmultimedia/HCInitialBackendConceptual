 
using DAO.PlanningPortal.Application.Interfaces;
using zero.Shared.Models.Finance;
using zero.Shared.Models.Inventory;
using zero.Shared.Models.Security;
using zero.Shared.Response;
using DAO.PlanningPortal.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.API.Common.Controller;

namespace DAO.PlanningPortal.API.Controllers.CustomerVender;
[Route("/api/LedgerGroup")]
public class LedgerGroupController : BaseApiController
{
    public LedgerGroupController(IAccountAppService appService)
    {
        _appService = appService;
    }

    public IAccountAppService _appService { get; }

    [HttpPost]
    [Route("AddEdit")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<bool>> AddEditCustomer(LedgerGroupDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }

        return null;
        var response = new LedgerGroupDTO();
    }
    [HttpGet]
    [Route("Get")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<List<LedgerGroupDTO>>> Get()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }


        var response = new List<LedgerGroupDTO>()
            {
                new LedgerGroupDTO()
                {
                }
            };
        return response;
    }
    [HttpGet]
    [Route("GetById/{id}")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<LedgerGroupDTO>> GetByID(long id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = new LedgerGroupDTO();
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

