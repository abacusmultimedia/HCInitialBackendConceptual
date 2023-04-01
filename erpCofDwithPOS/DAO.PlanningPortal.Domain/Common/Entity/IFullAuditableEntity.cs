using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Common.Entity;

public interface IFullAuditableEntity : ICreateAuditableEntity, IUpdateAuditableEntity, ISoftDeleteAuditableEntity
{
}

public interface ISoftDeleteAuditableEntity
{
    public bool IsDeleted { get; set; }
}

public interface IDeleteAuditableEntity
{
    int? DeletedBy { get; set; }

    [Column(TypeName = "datetime")]
    DateTime DeletedOn { get; set; }
}

public interface ICreateAuditableEntity
{
    int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    DateTime CreatedOn { get; set; }
}

public interface IUpdateAuditableEntity
{
    int? LastModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    DateTime? LastModifiedOn { get; set; }
}

public interface IMayHaveTenant
{
    int? TenantId { get; set; }
}

public interface IMustHaveTenant
{
    int TenantId { get; set; }
}

public interface IMayHaveOrganizationUnit
{
    int? OrganizationUnitId { get; set; }
}

public interface IMustHaveOrganizationUnit
{
    int OrganizationUnitId { get; set; }
}