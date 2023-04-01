using System.Collections.Generic;

namespace zero.Shared.Models.Plan
{
    public class CRouteCardDTO
    {
        public int Id { get; set; }
        public string CRouteCardRouteNumber { get; set; }
        public string CRouteCardHoursDurationTime { get; set; }
        public int CRouteCardServiceWorkersId { get; set; }
        public int Type { get; set; }
    }
}
