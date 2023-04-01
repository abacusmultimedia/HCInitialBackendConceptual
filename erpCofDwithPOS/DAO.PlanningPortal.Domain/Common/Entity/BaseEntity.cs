namespace DAO.PlanningPortal.Domain.Common.Entity;

public abstract class BaseEntity<TPrimaryKey>
{
    public virtual TPrimaryKey Id { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}