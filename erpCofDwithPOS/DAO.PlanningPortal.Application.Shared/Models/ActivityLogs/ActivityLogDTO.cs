using zero.Shared.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.ActivityLogs
{
    public class ActivityLogDTO
    {
        public int Id { get; set; }
        public string RouteName { get; set; }
        public string RouteId { get; set; }
        public int ActivityTypeId { get; set; }
        public int Userkey { get; set; }
        public int ActivityLogTypekey { get; set; }
        public long? TenantId { get; set; }
        public string Comment { get; set; }
        public string Reason { get; set; }
        public string PerformedBy { get; set; }
        public DateTime PerformedOn { get; set; }
        
    }
}
