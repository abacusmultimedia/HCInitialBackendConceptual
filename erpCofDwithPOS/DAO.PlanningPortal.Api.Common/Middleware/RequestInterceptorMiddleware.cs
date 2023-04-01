using zero.Shared.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.API.Common.Middleware;

/// <summary>
/// Request Interceptor middleware intercepts the Request coming into the server and Response going out of the Application.
/// It logs the Incoming Request.
/// It also logs the responses and also returns a Unified Response
/// </summary>
public class RequestInterceptorMiddleware
{
    #region Constructor, Variables and Properties

    private readonly ILogger<RequestInterceptorMiddleware> _Logger;
    private RequestDelegate Next { get; }
    private IWebHostEnvironment Env { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next">Request Delegate</param>
    /// <param name="env">Hosting Environment</param>
    /// <param name="logger">Logger</param>
    public RequestInterceptorMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<RequestInterceptorMiddleware> logger)
    {
        Next = next;
        Env = env;
        _Logger = logger;
    }

    #endregion Constructor, Variables and Properties

    #region Public Method

    /// <summary>
    /// Invoke the current middleware
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        Stream originalStream = context.Response.Body;

        // Process Incoming Request
        ProcessRequest(context.Request);

        Exception _exception = null;
        using (var memoryStream = new MemoryStream())
        {
            context.Response.Body = memoryStream;

            try
            {
                // Call the next delegate/middleware in the pipeline
                // Continue down the middleware pipeline, eventually returning to this class
                await Next(context);
            }
            catch (Exception exception)
            {
                _exception = exception;
                LogGlobalError(context, exception);
            }

            // Format Unified Response and Get the serialized object in string format
            string serviceResponseContent = await FormatResponse(context.Response, _exception);

            // Process Response
            ProcessResponse(context.Response, serviceResponseContent, _exception);

            // Write the new response content
            await WriteOutputStream(context, serviceResponseContent, originalStream);
        }
    }

    #endregion Public Method

    #region Private Methods

    /// <summary>
    /// Log Global Errors occurred in the middleware
    /// </summary>
    /// <param name="context">Http Context</param>
    /// <param name="exception">Exception</param>
    private void LogGlobalError(HttpContext context, Exception exception)
    {
        if (_Logger.IsEnabled(LogLevel.Error))
        {
            _Logger.LogError(exception, $"{context.Request?.Method} - {context.Response.StatusCode} - {context.Request?.Path.Value}");
        }
    }

    /// <summary>
    /// Process the HttpContext.Request
    /// It logs the Request based on the log levels
    /// </summary>
    /// <param name="request">HttpRequest</param>
    /// <returns></returns>
    private void ProcessRequest(HttpRequest request)
    {
        try
        {
            // Logging request object
            if (_Logger.IsEnabled(LogLevel.Information))
            {
                _Logger.LogInformation("Request starting - Host: {host}, Method: {method}, URL: {url}",
                                        request.Host,
                                        request.Method,
                                        request.Path.Value);
            }

            // Logging request object
            if (_Logger.IsEnabled(LogLevel.Debug))
            {
                if (request.Headers.Count > 0)
                {
                    _Logger.LogDebug("Request Headers", string.Join(',', request.Headers.Select(x => $"{x.Key}:{x.Value}")));
                }

                // If the request has Form, log the form data
                if (request.HasFormContentType)
                {
                    _Logger.LogDebug("Form Data: {data}",
                                string.Join(',', request.Form?.Select(x => $"{x.Key}:{x.Value}")));
                }
            }
        }
        catch (Exception exception)
        {
            if (_Logger.IsEnabled(LogLevel.Error))
            {
                _Logger.LogError(exception, $"An error occurred in {nameof(ProcessRequest)} - {nameof(RequestInterceptorMiddleware)}");
            }
        }
    }

    /// <summary>
    /// Process the HttpContext.Response
    /// It logs the Request based on the log levels.
    /// It also formats the response body in case of errors and unauthorized requests.
    /// </summary>
    /// <param name="response">HttpResponse</param>
    /// <param name="exception">Exception</param>
    /// <returns></returns>
    private void ProcessResponse(HttpResponse response, string responseContent, Exception exception = null)
    {
        try
        {
            LogLevel logLevel = LogLevel.Information;
            // If it is Server Error, set LogLevel=Critical
            // If it is Application specific Error, set LogLevel=Error
            if (response.StatusCode >= StatusCodes.Status500InternalServerError)
            {
                logLevel = LogLevel.Critical;
            }
            else if (response.StatusCode >= StatusCodes.Status400BadRequest || exception != null)
            {
                logLevel = LogLevel.Error;
            }

            _Logger.Log(logLevel, "Response finished - Host: {1}, Method: {2}, URL: {3}, StatusCode: {4}",
                                    response.HttpContext?.Request?.Host,
                                    response.HttpContext?.Request?.Method,
                                    response.HttpContext?.Request?.Path.Value,
                                    response.StatusCode);

            // Logging request object
            if (_Logger.IsEnabled(LogLevel.Debug))
            {
                _Logger.LogDebug($"Response Body: {responseContent}");
            }
        }
        catch (Exception ex)
        {
            if (_Logger.IsEnabled(LogLevel.Error))
            {
                _Logger.LogError(ex, $"An error occurred in {nameof(ProcessResponse)} - {nameof(RequestInterceptorMiddleware)}");
            }
        }
    }

    /// <summary>
    /// Formats the HttpContext.Response in case of errors/exception and unauthorized requests
    /// It also make sure for the Unified Response body and returns serialized object in string format.
    /// </summary>
    /// <param name="response">HttpResponse</param>
    /// <param name="exception">Exception</param>
    /// <returns></returns>
    private async Task<string> FormatResponse(HttpResponse response, Exception exception)
    {
        // Get Response Body
        string responseBody = string.Empty;

        //We need to read the response stream from the beginning...
        response.Body.Seek(0, SeekOrigin.Begin);

        //...and copy it into a string
        responseBody = await new StreamReader(response.Body).ReadToEndAsync();

        //We need to reset the reader for the response so that the client can read it.
        response.Body.Seek(0, SeekOrigin.Begin);

        // If Debug is enabled, write all the response body as well.
        if (_Logger.IsEnabled(LogLevel.Debug))
        {
            if (!string.IsNullOrEmpty(responseBody))
            {
                _Logger.LogDebug("Response Content: {0}", responseBody);
            }
        }

        // If the response body comes from the Services, deserialize it
        // If not create a Service response
        ServiceResponse apiResponse = null;

        if (response.StatusCode == StatusCodes.Status200OK || response.StatusCode == StatusCodes.Status201Created)
        {
            apiResponse = new ServiceResponse(GetDeserializeObject(responseBody), true);
        }
        else if (response.StatusCode == StatusCodes.Status204NoContent)
        {
            apiResponse = new ServiceResponse(false, "No content");
        }
        else if (response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            apiResponse = new ServiceResponse(true, "You are not authorized");
        }
        else if (response.StatusCode >= StatusCodes.Status400BadRequest)
        {
            apiResponse = new ServiceResponse(null, false, GetErrorDeserializeObject(responseBody).ToString());
        }

        // If It is development enable, write the error in Message
        if (Env.IsDevelopment())
        {
            apiResponse.Error = exception?.Message?.ToString();
        }

        // Write the new updated response
        return JsonConvert.SerializeObject(apiResponse, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
    }

    /// <summary>
    /// Write a content in the Output stream
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// <param name="content">String Content</param>
    /// <param name="originalStream">Original Stream to replace the new one</param>
    /// <returns></returns>
    private async Task WriteOutputStream(HttpContext context, string content, Stream originalStream)
    {
        var contentType = context.Response.ContentType?.ToLower();
        contentType = contentType?.Split(';', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();   // Filter out text/html from "text/html; charset=utf-8"
        if (string.IsNullOrEmpty(contentType))
        {
            contentType = "application/json";
            context.Response.ContentType = contentType;
        }

        // Create a new stream with the modified body, and reset the content length to match the new stream
        var requestContent = new StringContent(content, Encoding.UTF8, contentType);
        context.Response.Body = await requestContent.ReadAsStreamAsync();//modified stream
        context.Response.ContentLength = context.Response.Body.Length;

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        await context.Response.Body.CopyToAsync(originalStream);
        context.Response.Body = originalStream;
    }

    private static object GetDeserializeObject(string responseBody)
    {
        if (string.IsNullOrEmpty(responseBody)) return null;
        try
        {
            return JsonConvert.DeserializeObject(responseBody);
        }
        catch
        {
            return responseBody;
        }
    }

    private static object GetErrorDeserializeObject(string responseBody)
    {
        if (string.IsNullOrEmpty(responseBody)) return null;
        try
        {
            var obj = JsonConvert.DeserializeObject<string[]>(responseBody);
            return string.Join(", ", obj);
        }
        catch
        {
            return responseBody;
        }
    }

    #endregion Private Methods
}