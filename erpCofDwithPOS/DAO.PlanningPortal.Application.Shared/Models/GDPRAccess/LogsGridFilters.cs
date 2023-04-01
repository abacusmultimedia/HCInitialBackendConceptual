using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.GDPRAccess
{
    public class LogsGridFilters
    {
        public int user { get; set; }
        public int SystemID { get; set; }
        public string? RoleID { get; set; }
        public string? AdminBy { get; set; }
        public int Environment { get; set; }
        public DateTime? StartDate { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime ?StartTime { get; set; }
        public int AccessDuration { get; set; }
        public int ReasonToAccessID { get; set; }
        //public bool ViewedCustomerData { get; set; } 

    }
}
