
using Amazon.S3;
using ERP.API.Common.Controller;
using DAO.PlanningPortal.Application.Interfaces;
using zero.Shared.Models.Finance;
using zero.Shared.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.API.Controllers.CustomerVender;
[Route("/api/Transactions")]
public class TransactionsController : BaseApiController
{

    private readonly string _bucketName = "acntrenterpproofstorage";
    public TransactionsController(IAccountAppService appService)
    {
        _appService = appService;
    }

    public IAccountAppService _appService { get; }

    [HttpPost]
    [Route("AddEdit")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<bool>> AddEditCustomer(VenderDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }

        return null;
        var response = new CustomerDTO();
    }

    [HttpGet]
    [Route("Get")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<List<VenderDTO>>> Get()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }


        var response = new List<VenderDTO>()
            {
                new VenderDTO()
                {
                }
            };
        return response;
    }
    [HttpGet]
    [Route("GetById/{id}")]
    [ProducesDefaultResponseType(typeof(ServiceResponse<bool>))]
    public async Task<ActionResult<VenderDTO>> GetByID(long id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetModelErrorResponse(ModelState));
        }
        var response = new VenderDTO();
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

    [AllowAnonymous]
    [HttpGet("GetFile/{fileName}")]
    public async Task<ActionResult> GetFile(string fileName)
    {

        var client = new AmazonS3Client("AKIATZS5PLRAAQYVCPU7", "giByPDDCV3PV18ksbqhL70JEFqaaK+XrCs8rk9SG");
        var respon = await client.GetObjectAsync(_bucketName, fileName);
        return File(respon.ResponseStream, respon.Headers.ContentType);

    }

}

