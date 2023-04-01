using zero.Shared.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Api.Common.Middleware;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        //First, get the incoming request
        var request = await FormatRequest(context.Request);

        //Copy a pointer to the original response body stream
        var originalBodyStream = context.Response.Body;

        try
        {
            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                var response = await FormatResponse(context.Response);

                //TODO: Save log to chosen datastore

                //convert json to a stream
                await WriteOutputStream(context, response, originalBodyStream);
            }
        }
        finally
        {
            //and finally, reset the stream for downstream calls
            context.Response.Body = originalBodyStream;
        }
    }

    private async Task WriteOutputStream(HttpContext context, string content, Stream originalStream)
    {
        var contentType = context.Response.ContentType?.ToLower();
        contentType = contentType?.Split(';', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();   // Filter out text/html from "text/html; charset=utf-8"

        // Create a new stream with the modified body, and reset the content length to match the new stream
        var requestContent = new StringContent(content, Encoding.UTF8, contentType);
        context.Response.Body = await requestContent.ReadAsStreamAsync();//modified stream
        context.Response.ContentLength = context.Response.Body.Length;

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        await context.Response.Body.CopyToAsync(originalStream);
        context.Response.Body = originalStream;
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        var body = request.Body;

        //This line allows us to set the reader for the request back at the beginning of its stream.
        request.EnableBuffering();

        //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        //...Then we copy the entire request stream into the new buffer.
        await request.Body.ReadAsync(buffer, 0, buffer.Length);

        //We convert the byte[] into a string using UTF8 encoding...
        var bodyAsText = Encoding.UTF8.GetString(buffer);

        //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
        request.Body = body;

        return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        //We need to read the response stream from the beginning...
        response.Body.Seek(0, SeekOrigin.Begin);

        //...and copy it into a string
        string responseBody = await new StreamReader(response.Body).ReadToEndAsync();

        //We need to reset the reader for the response so that the client can read it.
        response.Body.Seek(0, SeekOrigin.Begin);

        //lets convert responseBody to something we can use
        var data = JsonConvert.DeserializeObject(responseBody);

        //create your wrapper response and convert to JSON
        var json = new ServiceResponse(data, true);

        //Return the string for the response
        return JsonConvert.SerializeObject(json, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });
    }
}