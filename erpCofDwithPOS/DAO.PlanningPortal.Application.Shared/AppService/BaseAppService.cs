using zero.Shared.Response;
using DAO.PlanningPortal.Common.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace zero.Shared.AppService;

/// <summary>
/// Base Service class which will be implemented by each Application Service.
/// It contains the basic methods to return Success and Error Responses.
/// </summary>
public abstract class BaseAppService
{
    public ServiceResponse SuccessResponse()
    {
        return new ServiceResponse(true);
    }

    public ServiceResponse SuccessResponse(object data)
    {
        return new ServiceResponse(data, true);
    }

    public ServiceResponse ErrorResponse()
    {
        return new ServiceResponse(false);
    }

    public ServiceResponse ErrorResponse(params string[] errors)
    {
        return new ServiceResponse(null, false, errors);
    }

    public ServiceResponse ErrorResponse(object data, params string[] errors)
    {
        return new ServiceResponse(data, false, errors.ToArray());
    }

    public void LogDebugObject(ILogger logger, params object[] model)
    {
        if (logger.IsDebugEnabled())
        {
            foreach (var obj in model)
            {
                logger.LogDebug($"{obj}");
            }
        }
    }
}