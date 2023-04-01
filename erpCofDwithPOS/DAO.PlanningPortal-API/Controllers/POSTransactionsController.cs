using DAO.PlanningPortal.Application.Interfaces.POS;
using ERP.API.Common.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using zero.Application.Shared.Models.POS;
using zero.Shared.Models.POS;
using zero.Shared.Response;

namespace DAO.PlanningPortal.API.Controllers
{

    [Route("/api/POSTransaction")]
    public class POSTransactionsController : BaseApiController
    {
        public POSTransactionsController(IPOS appService)
        {
            _appService = appService;
        }

        public IPOS _appService { get; }



        [HttpPost]
        [Route("PostPurchase")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<List<ItemPOSDto>>> Get(PurchaseInvoiceDto req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.PostPOSPurchaseInvoice(req);
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
        [Route("GetPurchaseInvoiceList")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<PurcahseListDto>>))]
        public async Task<ActionResult<List<PurcahseListDto>>> GetPurchaseInvoiceList(PurchaseListFilter req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetPurchaseInvoiceList(req);
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


