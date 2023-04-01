using zero.Shared.Models;
using System;

namespace zero.Shared.Response;

public class Result : LogableModel
{
    internal Result()
    {
        Succeeded = true;
    }

    internal Result(bool succeeded, params string[] errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }

    public Exception Exception { get; set; }

    public static Result Success()
    {
        return new Result(true, new string[] { });
    }

    public static Result Failure(params string[] errors)
    {
        return new Result(false, errors);
    }
}

public class Result<T> : Result
{
    public T Data { get; set; }

    internal Result(bool succeeded, params string[] errors) : base(succeeded, errors)
    {
    }

    public Result(T data, bool succeeded, params string[] errors) : base(succeeded, errors)
    {
        this.Data = data;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(data, true, new string[] { });
    }

    public new static Result<T> Failure(params string[] errors)
    {
        return new Result<T>(false, errors);
    }

    public static Result<T> Failure(Exception exception, params string[] errors)
    {
        return new Result<T>(false, errors)
        {
            Exception = exception
        };
    }
}