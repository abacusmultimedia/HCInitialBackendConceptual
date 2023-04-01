using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Domain.Entities.GDPR
{
    public class DataAccessRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RoleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StartDate { get; set; }
        public int AccessDuration { get; set; }
        public string ApprovedBy { get; set; }

        public int SystemID { get; set; }

        [ForeignKey("SystemID")]
        public Systems SelectedSystem { get; set; }
        public int Environment { get; set; }

        public int ReasonToAccessID { get; set; }

        [ForeignKey("ReasonToAccessID")]
        public ReasonToAccess ReasonToAccess { get; set; }



        public string ReasonInDetails { get; set; }
        public bool ViewedCustomerData { get; set; }
        public string AdminBy { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser user { get; set; }

    }


    public class Systems
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public ICollection<DataAccessRequest> DataAccessRequest { get; set; }

    }

    public class ReasonToAccess
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<DataAccessRequest> DataAccessRequest { get; set; }
    }


}
