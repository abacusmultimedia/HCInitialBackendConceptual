using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(ActivityLogDetail))]
    public partial class ActivityLogDetail : IBaseEntity<int>
    {
        public int Id { get; set; }

        public int ActivityType { get; set; }
        [Required , Column(TypeName = "nvarchar(50)")]
        public string ActivityId { get; set; }
        public Nullable<long> TenantId { get; set; } 
        public virtual ActivityLog  ActivityLog { get; set; }

    }
}
