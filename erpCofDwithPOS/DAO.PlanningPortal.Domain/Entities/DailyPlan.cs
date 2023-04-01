using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(DailyPlan))]
    public class DailyPlan : IBaseEntity<int>, IFullAuditableEntity
    {
        public int Id { get; set; }
        public int ServiceWorkerId { get; set; }
        public int TransportTypeId { get; set; }
        public int OrdeningGroupId { get; set; }
        public int OrdeningNo { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        public Route Route { get; set; }

        public bool PayforOwnVehicle { get; set; }
        public bool Return { get; set; }

        [ForeignKey("ServiceWorkerId")]
        public ServiceWorker ServiceWorker { get; set; }

        [ForeignKey("TransportTypeId")]
        public virtual TransportType Type { get; set; }

        [ForeignKey("OrdeningGroupId")]
        public OrdeningGroup OrdeningGroup { get; set; }

        public bool IsPublished { get; set; }

        /// Auditable Entities 
        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        public int? LastModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
        /// Auditable Entities
    }
}