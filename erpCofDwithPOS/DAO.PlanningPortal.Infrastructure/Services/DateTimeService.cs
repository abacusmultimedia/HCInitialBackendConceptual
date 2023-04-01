using zero.Shared.Common;
using System;

namespace DAO.PlanningPortal.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}