 
using DAO.PlanningPortal.Application.Interfaces;
using DAO.PlanningPortal.Application.Interfaces.Finance;
using DAO.PlanningPortal.Application.Interfaces.Inventory;
using DAO.PlanningPortal.Application.Interfaces.Transaction;
using zero.Shared.Models.Dashboard;
using zero.Shared.Models.Finance;
using zero.Shared.Models.Inventory;
using zero.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.API.Common.Controller;

namespace DAO.PlanningPortal.API.Controllers.CustomerVender
{
    [Route("/api/Dashboard")]
    public class DashboardController : BaseApiController
    {

        public DashboardController(ITransactionService appService,
            ILedgerService ledgerService,
            IInventoryService inventoryService)
        {
            _appService = appService;
            _ledgerService = ledgerService;
            _inventoryService = inventoryService;
        }

        public ILedgerService _ledgerService { get; }
        public ITransactionService _appService { get; }
        public IInventoryService _inventoryService { get; }


        [HttpGet]
        [Route("GetTransactionsWidgets")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<WidgetDto>>))]
        public async Task<ActionResult<List<WidgetDto>>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetTransactionsWidgets();
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
        [Route("GetLedgersWidgets")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<WidgetDto>>))]
        public async Task<ActionResult<List<WidgetDto>>> GetLedgers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _ledgerService.GetWidgets();
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
        [Route("GetItemsWidgets")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<WidgetDto>>))]
        public async Task<ActionResult<List<WidgetDto>>> GetItems()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _inventoryService.GetWidgets();
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
        [Route("GetContractWidgets")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<WidgetDto>>))]
        public async Task<ActionResult<List<WidgetDto>>> GetContracts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetContractWidgets();
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
        [Route("Refresh")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<long>>))]
        public async Task<ActionResult<List<WidgetDto>>> Refresh()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.MakeRentPayableforThisMonth();
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
