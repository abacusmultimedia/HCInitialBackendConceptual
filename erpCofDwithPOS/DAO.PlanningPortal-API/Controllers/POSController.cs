
using DAO.PlanningPortal.Application.Interfaces.POS;
using zero.Shared.Models.Inventory;
using zero.Shared.Models.POS;
using zero.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using zero.Application.Shared.Models.POS;
using ERP.API.Common.Controller;

namespace DAO.PlanningPortal.API.Controllers
{

    [Route("/api/POS")]
    public class POSController : BaseApiController
    {
        public POSController(IPOS appService)
        {
            _appService = appService;
        }

        public IPOS _appService { get; }



        [HttpPost]
        [Route("GetPOSLookup")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<ItemPOSDto>>))]
        public async Task<ActionResult<List<ItemPOSDto>>> Get(ItemPOSLookupSearchReq req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.ItemsLookup(req);
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
        [Route("GetMaxDocNumber")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<string>))]
        public async Task<ActionResult<List<ItemPOSDto>>> GetMaxDocNumber()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = _appService.lastBill();
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
        [Route("GetItemBySubCode/{id}")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<POSGrid>))]
        public async Task<ActionResult<POSGrid>> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.ItemSelectionByID(id);
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
        [Route("PostPOS")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<POSSalesResonseDTO>))]
        public async Task<ActionResult<POSSalesResonseDTO>> PostPOS(POSInvoiceDocReqDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.PostPOS(request);
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
        [Route("GetRecallList")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<RecallList>>))]
        public ActionResult<List<RecallList>> GetRecallList()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = _appService.GetRecallList();
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
        [Route("GetBillbyNo")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<POSInvoiceDocReqDto>))]
        public async Task<ActionResult<string>> GetBillbyNo(string request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetBillbyNo(request);
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
        [Route("GetVatQRData")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<POSInvoiceDocDto>))]
        public ActionResult<string> GetVatQRData(decimal Amount, decimal VatAmount)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = _appService.GetQRCodeString(Amount, VatAmount);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }

        }


        #region Hold and Recall 



        [HttpPost]
        [Route("RecallbyNo")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<POSInvoiceDocReqDto>))]
        public async Task<ActionResult<string>> RecallbyNo(string request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.RecallbyNo(request);
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
        [Route("RemoveRecallById")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<bool>> RemoveRecallById(string request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.RemoveRecallById(long.Parse(request));
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
        [Route("HoldPOS")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<string>))]
        public async Task<ActionResult<string>> HoldPOS(POSInvoiceDocReqDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.HoldPOS(request);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }


        #endregion Hold and Recall 


    }
}
