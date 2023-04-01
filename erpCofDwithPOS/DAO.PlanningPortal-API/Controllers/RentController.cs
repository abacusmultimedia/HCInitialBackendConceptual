

using ERP.API.Common.Controller;
using DAO.PlanningPortal.Application.Interfaces;
using DAO.PlanningPortal.Application.Interfaces.Transaction;
using zero.Shared.Models.Finance;
using zero.Shared.Models.Inventory;
using zero.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.API.Controllers
{
    [Route("/api/RentalIncome")]
    public class RentController : BaseApiController
    {

        public RentController(ITransactionService appService)
        {
            _appService = appService;
        }

        public ITransactionService _appService { get; }

        [HttpPost]
        [Route("AddEdit")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<bool>> AddEdit(RentReceiptDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.AddRent(model);
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
        [Route("CollectRentByContract")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<bool>> CollectRentByContract([FromForm] RentReceiptByContractDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.RentCollectionByContract(model);
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
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<ContractDTO>>))]
        public async Task<ActionResult<List<ContractDTO>>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetListofCollection();
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
        public async Task<ActionResult<ItemDto>> GetByID(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = new ItemDto();
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
}
