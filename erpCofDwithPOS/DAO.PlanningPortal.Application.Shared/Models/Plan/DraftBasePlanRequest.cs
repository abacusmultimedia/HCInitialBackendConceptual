using System.Collections.Generic;

namespace zero.Shared.Models.Plan
{
    public class DraftBasePlanRequest
    {
        public int TenantId { get; set; }
        public List<int> OrganizationUnitIds { get; set; }
        public int WeekDayId { get; set; }
        public List<BasePlanDTO> Data { get; set; }
    }
}
