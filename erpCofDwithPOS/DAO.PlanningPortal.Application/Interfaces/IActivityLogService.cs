using zero.Shared.Models.ActivityLogs;
using zero.Shared.Response;
using zero.Shared.ViewModels.ActivityLogs;
using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Interfaces
{
    public interface IActivityLogService
    {
        Task<Result<ActivityLog>> InsertActivity(ActivityLogTypeEnum systemKeyword, ActivityLogDetailsDTO details, string reason, params object[] commentParams);
        Task<Result<List<ActivityLogTypeDTO>>> GetAllActivityTypes();
        Task<Result<PaginatedList<ActivityLogDTO>>> GetAllActivityLogs(ActivityLogSearchDTO request);
    }
}
