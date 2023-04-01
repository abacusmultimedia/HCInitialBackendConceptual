using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using zero.Shared.Response;
using DAO.PlanningPortal.Application.Interfaces.Finance;
using zero.Shared.Models.Finance;
using ERP.API.Common.Controller;

namespace DAO.PlanningPortal.API.Controllers.CustomerVender;
[Route("/api/Customers")]
public class CustomerController : BaseApiController
{
    public CustomerController(ICustomerService appService)
    {
        _appService = appService;
    }

    public ICustomerService _appService { get; }

    [HttpPost]
    [Route("AddEdit")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<bool>> AddEditCustomer(CustomerDTO model)
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
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<CustomerDTO>>))]
    public async Task<ActionResult<List<CustomerDTO>>> Get()
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
    [ProducesDefaultResponseType(typeof(ServiceResponse<CustomerDTO>))]
    public async Task<ActionResult<CustomerDTO>> GetByID(long id)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _appService.GetById(id);
        if (response.Succeeded)
        {
            return Ok(response.Data);
        }
        else
        {
            return BadRequest(response.Errors);
        }



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
        var response = await _appService.Delete(id);
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

