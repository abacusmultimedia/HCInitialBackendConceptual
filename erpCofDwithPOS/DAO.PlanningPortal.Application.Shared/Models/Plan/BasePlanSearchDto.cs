using System;

namespace zero.Shared.Models.Plan
{
    public class BasePlanSearchDto
    {
        public int TenantId { get; set; }
        public string WeekDayId { get; set; }
    }
    public class DailyPlanSearchDto
    {
        public int TenantId { get; set; }
        public DateTime date { get; set; }
    }
}
