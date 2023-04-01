using DAO.PlanningPortal.Domain.Common.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities;

[DisplayName("Users")]
public class ApplicationUser : IdentityUser<int>, IFullAuditableEntity, IMayHaveTenant, IMayHaveOrganizationUnit
{
    public ApplicationUser() : base()
    {
    }

    public int? CreatedBy { get; set; }

    [Required, Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int? LastModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModifiedOn { get; set; }

    [Required, DefaultValue(true)]
    public bool IsActive { get; set; }

    [Required, DefaultValue(false)]
    public bool IsDeleted { get; set; }

    public int? TenantId { get; set; }
    public int? OrganizationUnitId { get; set; }

    [Required, StringLength(256)]
    public string FullName { get; set; }

    public string Address { get; set; }

    public Tenant Tenant { get; set; }
    public OrganizationUnit OrganizationUnit { get; set; }
    public ICollection<UserOuMapping> UserOuMapping { get; set; }
}