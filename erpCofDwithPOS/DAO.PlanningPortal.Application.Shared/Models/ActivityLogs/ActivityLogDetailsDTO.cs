using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.ViewModels.ActivityLogs
{
    public class ActivityLogDetailsDTO
    {
        public int Id { get; set; } 
        public long? TenantId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ActivityId { get; set; }


    }
}
