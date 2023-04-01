 
using DAO.PlanningPortal.Application.Interfaces.Inventory;
using zero.Shared.Models.Inventory;
using zero.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.API.Common.Controller;

namespace DAO.PlanningPortal.API.Controllers.CustomerVender
{
    [Route("/api/Item")]
    public class ItemController : BaseApiController
    {
        public IInventoryService _appService { get; }

        public ItemController(IInventoryService appService)
        {
            _appService = appService;
        }


        [HttpPost]
        [Route("AddEdit")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<bool>> AddEditCustomer(ItemDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.Item_AddEdit(model);
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
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<ItemDto>>))]
        public async Task<ActionResult<List<ItemDto>>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.Item_GetList();
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
            var response = await _appService.Item_GetList();
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
            return false;
        }

    }
}
