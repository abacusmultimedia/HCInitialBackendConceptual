using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(DraftPlan))]
    public class DraftPlan : IBaseEntity<int>, ICreateAuditableEntity, ISoftDeleteAuditableEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int OrganizationUnitId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnit { get; set; }

        public int? WeekDayId { get; set; }

        [ForeignKey("WeekDayId")]
        public WeekDays WeekDays { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DailyPlanDate { get; set; }
        public byte PlanType { get; set; }
        public int RequestedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime RequestedOn { get; set; }
        public int? ApprovedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ApprovedOn { get; set; }

        public byte StatusId { get; set; }

        /// Auditable Entities 
       
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

        /// Auditable Entities 
    }
}
