 
using DAO.PlanningPortal.Application.Interfaces;
using DAO.PlanningPortal.Application.Interfaces.Inventory;
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

namespace DAO.PlanningPortal.API.Controllers.CustomerVender
{
    [Route("/api/Batch")]
    public class BatchController : BaseApiController
    {

        public BatchController(IInventoryService appService)
        {
            _appService = appService;
        }

        public IInventoryService _appService { get; }

        [HttpPost]
        [Route("AddEdit")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<long>))]
        public async Task<ActionResult<long>> AddEditCustomer(BatchDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.Batch_AddEdit(model);
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
        [Route("AssignBatch")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<long>))]
        public async Task<ActionResult<long>> AssignBatch(AssignBatchtoAgentDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.Batch_AssignUser(model);
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
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<BatchDto>>))]
        public async Task<ActionResult<List<BatchDto>>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.Batch_GetList();
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
        [Route("GetAllAssignedtoMe")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<BatchDto>>))]
        public async Task<ActionResult<List<BatchDto>>> GetAllAssignedtoMe()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetAllAssignedtoMe();
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
        [Route("GetByID/{id}")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<BatchDto>> GetByID(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = new BatchDto();
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
