using System.Threading.Tasks;
using System.Collections.Generic;
using zero.Shared.Response;
using zero.Shared.Models.Finance;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.Dashboard;

namespace DAO.PlanningPortal.Application.Interfaces.Finance
{
    public interface ILedgerService
    {
        Task<Result<long>> AddEdit(AddLedgerDTO parameter);
        Task<Result<List<AddLedgerDTO>>> GetList();
        Task<Result<List<LookupDto>>> GetLookups(string name);
        Task<Result<bool>> Delete(long Id);
        Task<Result<List<WidgetDto>>> GetWidgets();
    }
}
