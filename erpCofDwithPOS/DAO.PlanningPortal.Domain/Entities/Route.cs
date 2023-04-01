using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(Route))]
    public class Route : IBaseEntity<int>, IFullAuditableEntity
    {
        public int Id { get; set; }
        public int FNR { get; set; }
        public int Mail { get; set; }
        public int JobId { get; set; }
        public double ALB { get; set; }
        public int TenantId { get; set; }
        public int OrganizationUnitId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnit { get; set; }

        public int RouteTypeId { get; set; }
        [ForeignKey("RouteTypeId")]
        public RouteType RouteType { get; set; }
        public float RouteSpeed { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string RouteName { get; set; }
        public double RouteLength { get; set; }

        /// Auditable Entities
        public int? CreatedBy { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime CreatedOn { get; set; }

        public int? LastModifiedBy { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
        /// Auditable Entities

        /// Virtual

        public ICollection<BasePlan> BasePlans { get; set; }
        public ICollection<DailyPlan> DailyPlans { get; set; }
    }
}