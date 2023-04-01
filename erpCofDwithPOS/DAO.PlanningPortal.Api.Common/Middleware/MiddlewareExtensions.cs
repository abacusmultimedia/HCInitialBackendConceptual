using zero.Shared.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace DAO.PlanningPortal.API.Common.Middleware;

public static class MiddlewareExtensions
{
    /// <summary>
    /// Global Exception Middleware Extension
    /// </summary>
    /// <param name="app">ApplicationBuilder</param>
    public static void UseGlobalException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature != null)
                {
                    context.Response.ContentType = "application/json";
                    //Serilog.Log.Error(exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                    await context.Response.WriteAsJsonAsync(new ServiceResponse
                    {
                        UnAuthorizedRequest = context.Response.StatusCode == StatusCodes.Status401Unauthorized,
                        Success = false,
                        Message = exceptionHandlerFeature.Error.Message
                    });
                }
            });
        });
    }

    /// <summary>
    /// Request Interceptor Middleware Extension that Logs the Request and Response, it also returns a Unified Response
    /// </summary>
    /// <param name="app">ApplicationBuilder</param>
    public static void UseRequestInterceptor(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestInterceptorMiddleware>();
    }
}