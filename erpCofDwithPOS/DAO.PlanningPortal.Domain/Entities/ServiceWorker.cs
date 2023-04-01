using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(ServiceWorker))]
    public class ServiceWorker : IBaseEntity<int>, IFullAuditableEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string SourceId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; }

        /// Auditable Entities
        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        public int? LastModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
        /// Auditable Entities

        ///  Virtuals
        public ICollection<Route> Routes { get; set; }
    }
}