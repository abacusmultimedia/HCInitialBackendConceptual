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
    [Table(nameof(ActivityLog))]
    public partial class ActivityLog : IBaseEntity<int>, ISoftDeleteAuditableEntity
    {
        public int Id { get; set; }
        [Required, Column(TypeName = "nvarchar(550)")]
        public string Comment { get; set; }
        public string Reason { get; set; }
        [Column(TypeName = "datetime")]
        public System.DateTime CreatedOnUtc { get; set; }


        /// Auditable Entities 
        public bool IsDeleted { get; set; }
        /// Auditable Entities 

        /// Relations
        public int Userkey { get; set; }
        [ForeignKey("Userkey")]
        public virtual ApplicationUser User { get; set; }
        public int ActivityLogTypekey { get; set; }
        [ForeignKey("ActivityLogTypekey")]
        public virtual ActivityLogType ActivityLogType { get; set; }
        public int ActivityLogDetailkey { get; set; }
        [ForeignKey("ActivityLogDetailkey")]
        public virtual ActivityLogDetail ActivityLogDetail { get; set; }

        /// Relations 
    }
}
