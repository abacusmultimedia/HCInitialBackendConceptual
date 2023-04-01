using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities;

[Table(nameof(Tenant))]
public class Tenant : IBaseEntity<int>, ICreateAuditableEntity, IUpdateAuditableEntity, ISoftDeleteAuditableEntity
{
    public Tenant()
    {
        this.OrganizationUnits = new HashSet<OrganizationUnit>();
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

    [Required, MaxLength(255)]
    public string Name { get; set; }

    [Required, MaxLength(255)]
    public string DisplayName { get; set; }

    public ICollection<OrganizationUnit> OrganizationUnits { get; set; }
    public ICollection<ApplicationUser> Users { get; set; }
    public ICollection<Configuration> Configurations { get; set; }
}