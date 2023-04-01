using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities;

[Table(nameof(Configuration))]
public class Configuration : IBaseEntity<int>, IFullAuditableEntity, IMayHaveTenant, IMayHaveOrganizationUnit
{
    [Required]
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

    public int? TenantId { get; set; }
    public int? OrganizationUnitId { get; set; }

    /******************************************************/

    [Required, MaxLength(100)]
    public string Key { get; set; }

    [Required]
    public string Value { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public int? UserId { get; set; }

    public Tenant Tenant { get; set; }
    public OrganizationUnit OrganizationUnit { get; set; }
}