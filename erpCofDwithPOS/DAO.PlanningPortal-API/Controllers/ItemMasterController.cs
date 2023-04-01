
using DAO.PlanningPortal.Application.Interfaces.Inventory;
using ERP.API.Common.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using zero.Application.Shared.Models.Inventory;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.POS;
using zero.Shared.Models.TransactionDTo;
using zero.Shared.Response;

namespace ERP.API.Controllers
{
    [Route("/api/ItemMaster")]
    public class ItemMasterController : BaseApiController
    {
        public ItemMasterController(IItemMasterService appService)
        {
            _appService = appService;
        }

        public IItemMasterService _appService { get; }

        [HttpPost]
        [Route("GetAll")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<ItemMasterDTO>>))]
        public async Task<ActionResult<List<ItemMasterDTO>>> Get(ItemMasterReqLazyLoad req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetAll(req);
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
        [Route("Post")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<ItemMasterCreationDTO>))]
        public async Task<ActionResult<ItemMasterCreationDTO>> Post(ItemMasterCreationDTO req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.Post(req);
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
        [Route("Put")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<ItemMasterCreationDTO>))]
        public async Task<ActionResult<ItemMasterCreationDTO>> Put(ItemMasterCreationDTO req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.Put(req);
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
        [ProducesDefaultResponseType(typeof(ServiceResponse<ItemMasterCreationDTO>))]
        public async Task<ActionResult<ItemMasterCreationDTO>> GetById(long id)
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

        [HttpGet]
        [Route("GetLookupBrand")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
        public async Task<ActionResult<List<LookupDto>>> GetLookupBrand()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetLookupBrand();
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
        [Route("GetLookupCategory")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
        public async Task<ActionResult<List<LookupDto>>> GetLookupCategory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetLookupCategory();
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
        [Route("CheckIfItemExist")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
        public async Task<ActionResult<bool>> CheckIfItemExist(ItemExistValidationRequestDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.ItemExists(item);
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
        [Route("GetLookupPaking")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
        public async Task<ActionResult<List<LookupDto>>> GetLookupPaking()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetLookupPaking();
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
        [Route("GetLookupColor")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
        public async Task<ActionResult<List<LookupDto>>> GetLookupColor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetLookupColor();
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
        [Route("GetLookupDepartment")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
        public async Task<ActionResult<List<LookupDto>>> GetLookupDepartment()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetLookupDepartment();
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
        [Route("GetLookupModel")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
        public async Task<ActionResult<List<LookupDto>>> GetLookupModel()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetLookupModel();
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
        [Route("GetLookupSubCategory")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<LookupDto>>))]
        public async Task<ActionResult<List<LookupDto>>> GetLookupSubCategory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetLookupSubCategory();
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
