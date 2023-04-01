using DAO.PlanningPortal.Domain.Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(OrdeningGroup))]
    public class OrdeningGroup : IBaseEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string OrdeningGroupName { get; set; }
        public int TenantId { get; set; }
        public int OrganizationUnitId { get; set; }

        #region relations

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnit { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        #endregion relations
    }
}