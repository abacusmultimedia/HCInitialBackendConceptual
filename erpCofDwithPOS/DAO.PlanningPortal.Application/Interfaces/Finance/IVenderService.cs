using zero.Shared.Response;
using System.Collections.Generic;
using System.Threading.Tasks; 
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.Finance;

namespace DAO.PlanningPortal.Application.Interfaces.Finance
{
    public interface IVenderService
    {
        Task<Result<long>> AddEdit(VenderDTO parameter);
        Task<Result<List<VenderDTO>>> GetList();
        Task<Result<List<LookupDto>>> GetLookups();
        Task<Result<bool>> Delete(long Id);
    }
}



