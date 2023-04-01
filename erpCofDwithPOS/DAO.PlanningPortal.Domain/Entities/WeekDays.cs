using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(WeekDays))]
    public class WeekDays : IBaseEntity<int>, IFullAuditableEntity
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        /// Auditable Entities
        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        public int? LastModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
        /// Auditable Entities

        //Virtuals
        public ICollection<BasePlan> BasePlans { get; set; }
    }
}