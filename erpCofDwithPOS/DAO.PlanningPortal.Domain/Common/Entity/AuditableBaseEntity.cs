using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Common.Entity;

public abstract class AuditableBaseEntity<T> : BaseEntity<T>, IAuditableEntity
{
    [Column(Order = 1)]
    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime", Order = 2)]
    public DateTime CreatedOn { get; set; }

    [Column(Order = 3)]
    public int? LastModifiedBy { get; set; }

    [Column(TypeName = "datetime", Order = 4)]
    public DateTime? LastModifiedOn { get; set; }

    [Column(Order = 5)]
    public bool IsActive { get; set; }

    [Column(Order = 6)]
    public bool IsDeleted { get; set; }
}