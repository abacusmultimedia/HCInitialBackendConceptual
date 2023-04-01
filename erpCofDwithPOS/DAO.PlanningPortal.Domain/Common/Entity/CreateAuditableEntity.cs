using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Common.Entity;

public class CreateAuditableEntity<T> : BaseEntity<T>, ICreateAuditableEntity
{
    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }
}

public class CreateAuditableEntity : CreateAuditableEntity<int>
{
}