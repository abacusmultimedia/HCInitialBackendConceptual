using zero.Shared.Models.GDPRAccess;
using zero.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Interfaces
{
    public interface IAccessRequestService
    {
        Task<Result<List<LookupDto>>> GetReasonsLookUp();
        Task<Result<List<LookupDto>>> GetSystemsLookUp();

        Task<Result<List<AccessFormDto>>> GetAllEntries(LogsGridFilters filters );
        Task<Result<bool>> RegisterEntery(AccessFormDto parameter);

    }
}
