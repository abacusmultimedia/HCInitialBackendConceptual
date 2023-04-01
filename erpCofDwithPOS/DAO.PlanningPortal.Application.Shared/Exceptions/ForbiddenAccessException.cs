using System;

namespace zero.Shared.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base()
    {
    }
}