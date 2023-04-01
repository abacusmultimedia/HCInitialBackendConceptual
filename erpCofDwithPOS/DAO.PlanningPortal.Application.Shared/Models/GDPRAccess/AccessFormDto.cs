using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.GDPRAccess
{
    public class AccessFormDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SystemID { get; set; }
        public string RoleID { get; set; }
        public string AdminBy { get; set; }
        public string UserName { get; set; }
        public int Environment { get; set; }
        public DateTime StartDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime StartTime { get; set; }
        public int AccessDuration { get; set; }
        public int ReasonToAccessID { get; set; }
        public string ReasonInDetails { get; set; }
        public bool ViewedCustomerData { get; set; }

        public string SystemTitle { get; set; }
        public string ReasonTitle { get; set; }

    }
}
