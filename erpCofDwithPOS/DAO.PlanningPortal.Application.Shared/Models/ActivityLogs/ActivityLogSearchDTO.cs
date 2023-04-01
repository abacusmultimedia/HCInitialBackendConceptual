using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.ActivityLogs
{
    public class ActivityLogSearchDTO : PagingQuery
    {
        public string RouteNumber { get; set; } = string.Empty;
        public List<long> AreaIds { get; set; } 
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public List<int> ActivityType { get; set; }
        public List<int> PerformedBy { get; set; }

    }
}
