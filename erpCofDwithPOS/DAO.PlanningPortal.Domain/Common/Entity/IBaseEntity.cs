namespace DAO.PlanningPortal.Domain.Common.Entity;

public interface IBaseEntity<TPrimaryKey>
{
    public TPrimaryKey Id { get; set; }
}