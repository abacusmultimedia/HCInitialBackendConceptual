using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(UserOuMapping))]
    public class UserOuMapping : IBaseEntity<int>, ICreateAuditableEntity
    {
        public UserOuMapping()
        {
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int TenantId { get; set; }

        public int OrganizationUnitId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnit { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}