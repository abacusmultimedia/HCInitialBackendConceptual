using zero.Shared.Models.Finance;
using zero.Shared.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Interfaces.Dashboard
{
    public interface IDashboardService
    {
        Task<Result<List<AddLedgerDTO>>> GetWidgetList();
    }
}