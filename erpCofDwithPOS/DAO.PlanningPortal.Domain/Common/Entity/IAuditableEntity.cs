namespace DAO.PlanningPortal.Domain.Common.Entity;

public interface IAuditableEntity : ICreateAuditableEntity, IUpdateAuditableEntity, ISoftDeleteAuditableEntity
{
}