using DAO.PlanningPortal.Common.Models;
using System.Collections.Generic;

namespace DAO.PlanningPortal.Common.Sessions;

public interface IUserSession
{
    public int? TenantId { get; }
    public int? OrganizationUnitId { get; }
    public int? UserId { get; }
    public List<UserOuMappingDto> UserOuMappingData { get; }
}