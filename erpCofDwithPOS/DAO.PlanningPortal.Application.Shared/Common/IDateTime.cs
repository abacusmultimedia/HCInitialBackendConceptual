using System;

namespace zero.Shared.Common;

public interface IDateTime
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}