using System.Collections.Generic;

namespace zero.Shared.Models.Plan
{
    public class OrdeningCardDTO
    {
        public int OrdeningNo { get; set; }
        public string CardTitle { get; set; }
        public int CardServiceWorkerId { get; set; }
        public int CardTransportTypeId { get; set; }
        public int CardWeekDayId { get; set; }
        public List<CRouteCardDTO> CardRoutes { get; set; }
        public List<DraftOrPublishedDTO> DraftOrPublished { get; set; }

        //public List<string> CardRouteIds { get; set; }
    }
}
