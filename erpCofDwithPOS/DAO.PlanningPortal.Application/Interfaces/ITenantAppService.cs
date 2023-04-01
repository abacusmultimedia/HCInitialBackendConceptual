using zero.Shared.Models;
using zero.Shared.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Interfaces
{
    public interface ITenantAppService
    {
        Task<Result<List<TenantDto>>> GetAllTenants();
        Task<Result<List<OrganizationalUnitsWrtTenantsDTO>>> OrganizationalUnitsWrtTenants();
        Task<Result<List<LoginUserTenantsDTO>>> GetAllCurrentlyLoginUserTenants();
    }
}