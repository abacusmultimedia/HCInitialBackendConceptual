using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities;

[Table(nameof(OrganizationUnit))]
public class OrganizationUnit : IBaseEntity<int>, IFullAuditableEntity
{
    public OrganizationUnit()
    {
        this.Users = new HashSet<ApplicationUser>();
        this.Configurations = new HashSet<Configuration>();
    }

    public int Id { get; set; }
    public int? CreatedBy { get; set; }

    [Required, Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int? LastModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModifiedOn { get; set; }

    [Required, DefaultValue(false)]
    public bool IsDeleted { get; set; }

    [Required, DefaultValue(true)]
    public bool IsActive { get; set; }

    /******************************************************/

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required, MaxLength(255)]
    public string DisplayName { get; set; }

    [Required]
    public int TenantId { get; set; }

    public Nullable<int> ParentId { get; set; }

    public Tenant Tenant { get; set; }
    public ICollection<ApplicationUser> Users { get; set; }
    public ICollection<Configuration> Configurations { get; set; }
}