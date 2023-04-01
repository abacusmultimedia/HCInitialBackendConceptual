using System.Linq;

namespace zero.Shared.Response;

public class ServiceResponse
{
    /// <summary>
    /// This property can be used to redirect user to a specified URL.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Indicates success status of the result.
    /// Set <see cref="Error"/> if this value is false.
    /// </summary>
    public bool Success { get; set; } = true;

    /// <summary>
    /// Error details (Must and only set if <see cref="Success"/> is false).
    /// This property is marked obsolete. Use <see cref="Errors"/> instead.
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// List of Errors (Must and only set if <see cref="Success"/> is false).
    /// </summary>
    public string[] Errors { get; set; }

    /// <summary>
    /// This property can be used to indicate that the current user has no privilege to perform this request.
    /// </summary>
    public bool UnAuthorizedRequest { get; set; } = false;

    /// <summary>
    /// The actual result object of AJAX request.
    /// It is set if <see cref="ServiceResponse.Success"/> is true.
    /// </summary>
    public object Result { get; set; }

    /// <summary>
    /// Creates an <see cref="ServiceResponse"/> object with <see cref="Result"/> specified.
    /// <see cref="ServiceResponse.Success"/> is set as true.
    /// </summary>
    /// <param name="result">The actual result object of AJAX request</param>
    public ServiceResponse(object result, bool success)
    {
        Result = result;
        Success = success;
    }

    public ServiceResponse(object result, bool success, params string[] errors)
    {
        Message = errors.Count() > 0 ? errors[0] : string.Empty;
        Result = result;
        Success = success;
    }

    /// <summary>
    /// Creates an <see cref="ServiceResponse"/> object.
    /// <see cref="ServiceResponse.Success"/> is set as true.
    /// </summary>
    public ServiceResponse()
    {
        Success = true;
    }

    /// <summary>
    /// Creates an <see cref="ServiceResponse"/> object with <see cref="ServiceResponse.Success"/> specified.
    /// </summary>
    /// <param name="success">Indicates success status of the result</param>
    public ServiceResponse(bool success)
    {
        Success = success;
    }

    /// <summary>
    /// Creates an <see cref="ServiceResponse"/> object with <see cref="ServiceResponse.Error"/> specified.
    /// <see cref="ServiceResponse.Success"/> is set as false.
    /// </summary>
    /// <param name="errors">Error details</param>
    /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
    public ServiceResponse(bool unAuthorizedRequest = false, params string[] errors)
    {
        Message = errors.Count() > 0 ? errors[0] : string.Empty;
        UnAuthorizedRequest = unAuthorizedRequest;
        Success = false;
    }
}

public class ServiceResponse<T> : ServiceResponse
{
    public new T Result { get; set; }
}