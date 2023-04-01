using ERP.API.Common.Controller;
using DAO.PlanningPortal.Application.Interfaces.Finance;
using DAO.PlanningPortal.Application.Interfaces.Transaction;
using zero.Shared.Models.Reporting;
using zero.Shared.Models.TransactionDTo;
using zero.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace DAO.PlanningPortal.API.Controllers;
[Route("/api/Reports")]
public class ReportsController : BaseApiController
{
    public ReportsController(ILedgerService appService, ITransactionService transactionService)
    {
        _appService = appService;
        _transactionService = transactionService;
    }

    public ILedgerService _appService { get; }
    public ITransactionService _transactionService { get; }

    [HttpGet]
    [Route("GetById/{id}")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<ChildTransactionDto>>))]
    public async Task<ActionResult<List<ChildTransactionDto>>> Get(long id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _transactionService.GetListByLedgerId(id);
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
    [Route("UpdateCollectionAmount")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<bool>> UpdateCollectionAmount(UpDateChildAmountOnly model)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }

        var response = await _transactionService.UpdatechildAmount(model);
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
    [Route("GetDayBook")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<TransactionDto>>))]
    public async Task<ActionResult<List<TransactionDto>>> GetDayBook()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _transactionService.GetDayBook();
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
    [Route("GetmyCollections")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<TransactionDto>>))]
    public async Task<ActionResult<List<TransactionDto>>> GetmyCollections()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _transactionService.GetAllmyCollections();
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
    [Route("GetTransactionAttachmentId/{id}")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<string>))]
    public async Task<ActionResult<string>> GetTransactionAttachmentId(long id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _transactionService.GetTransactionAttachment(id);
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
    [Route("GetOutstandingRents")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<List<OutstandingRentDto>>))]
    public async Task<ActionResult<List<OutstandingRentDto>>> GetOutstandingRents()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = await _transactionService.GetOutstandingRents();
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

