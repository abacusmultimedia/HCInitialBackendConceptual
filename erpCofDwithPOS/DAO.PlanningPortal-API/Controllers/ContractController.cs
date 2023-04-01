 
using DAO.PlanningPortal.Application.Interfaces;
using DAO.PlanningPortal.Application.Interfaces.Transaction;
using zero.Shared.Models.Finance;
using zero.Shared.Models.Inventory;
using zero.Shared.Models.TransactionDTo;
using zero.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.API.Common.Controller;

namespace DAO.PlanningPortal.API.Controllers.CustomerVender
{
    [Route("/api/Contract")]
    public class ContractController : BaseApiController
    {

        public ContractController(ITransactionService appService)
        {
            _appService = appService;
        }

        public ITransactionService _appService { get; }

        [HttpPost]
        [Route("AddEdit")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<bool>> AddEditCustomer(ContractDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.AddContract(model);
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
        [Route("UpdateContractStatus")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<long>))]
        public async Task<ActionResult<long>> UpdateContractStatus(ContractStatusDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.UpdateContractStatus(model);
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
            var response = await _appService.GetListofContracts();
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


        [HttpPost]
        [Route("ValidateUndercontractItem")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<bool>> ValidateUndercontractItem(RemoteValidationItemTransaction model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.ValidateContractItem(model);
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
        [Route("ValidateUndercontractLedger")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<bool>> ValidateUndercontractLedger(RemoteValidationItemTransaction model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.ValidateContractLedger(model);
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
