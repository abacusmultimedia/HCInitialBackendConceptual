using zero.Shared.Models.Plan;
using zero.Shared.Response;
using zero.Shared.ViewModels.Plan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Interfaces
{
    public interface IBasePlanService
    {
        Task ImportAsync(UploadPlanDto plan);
        Task<Result<List<DropdownModel>>> GetAllRoutes();
        Task<Result<List<DropdownModel>>> GetAllWeekDays();
        Task<Result<string>> ApproveBasePlan(List<int> ids);
        Task<Result<bool>> SyncServiceWorkers(List<SyncServiceWorker> request);
        Task<Result<List<DropdownModel>>> GetAllTransportTypes();
        Task<Result<List<DropdownModel>>> GetAllServiceWorkers();
        Task<Result<bool>> SaveDraftBasePlan(DraftBasePlanRequest request);
        Task<Result<List<BasePlanDTO>>> LoadBasePlanData(BasePlanSearchDto request);
        Task<Result<List<BasePlanDTO>>> LoadDailyPlanData(DailyPlanSearchDto request);
    }
}
