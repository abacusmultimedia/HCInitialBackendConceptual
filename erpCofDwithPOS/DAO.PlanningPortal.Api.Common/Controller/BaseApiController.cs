using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace ERP.API.Common.Controller;

[ApiController]
public class BaseApiController : ControllerBase
{
    [NonAction]
    public string[] GetModelErrorResponse(ModelStateDictionary modelState)
    {
        return modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
    }
}