using BK.Utility.Caching;
using DAO.PlanningPortal.Application.Interfaces;
using DAO.PlanningPortal.Application.LocalDTos;
using zero.Shared.AppService;
using zero.Shared.Models.ActivityLogs;
using zero.Shared.Repositories; 
using zero.Shared.Response;
using zero.Shared.ViewModels.ActivityLogs;
using DAO.PlanningPortal.Common.Extensions;
using DAO.PlanningPortal.Common.Extensions.QuerableExtensions;
using DAO.PlanningPortal.Common.Sessions;
using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Domain.Enums;
using DAO.PlanningPortal.Utility.Caching;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using zero.Application.Shared.Resources;

namespace DAO.PlanningPortal.Application.Services
{
    public class ActivityLogService : BaseAppService, IActivityLogService
    {
        private readonly IUserSession _userSession;
        private readonly ICacheManager _cacheManager;
        private UserManager<ApplicationUser> _userManager { get; }
        private const string ACTIVITYTYPE_ALL_KEY = "Nop.activitytype.all";
        private readonly IGenericRepositoryAsync<ActivityLog> _activityLogRepositoryAsync;
        private readonly IGenericRepositoryAsync<Route> _routeRepositoryAsync;
        private readonly IGenericRepositoryAsync<BasePlan> _dailyBasePlanRepositoryAsync;
        private readonly IGenericRepositoryAsync<ActivityType> _activityTypeRepositoryAsync;
        private readonly IGenericRepositoryAsync<ApplicationUser> _userRepository;
        private readonly IGenericRepositoryAsync<ActivityLogType> _activityLogTypeRepositoryAsync;
        private readonly IGenericRepositoryAsync<ActivityLogDetail> _activityLogDetailRepositoryAsync;

        public ActivityLogService(
            ILogger<UserAppService> logger,
            IUserSession userSession,
            ICacheManager cacheManager,
            UserManager<ApplicationUser> userManager,
            IGenericRepositoryAsync<ActivityLog> activityLogRepositoryAsync,
            IGenericRepositoryAsync<Route> routeRepositoryAsync,
            IGenericRepositoryAsync<BasePlan> dailyBasePlanRepositoryAsync,
            IGenericRepositoryAsync<ApplicationUser> userRepository,
            IGenericRepositoryAsync<ActivityType> activityTypeRepositoryAsync,
            IGenericRepositoryAsync<ActivityLogType> activityLogTypeRepositoryAsync,
            IGenericRepositoryAsync<ActivityLogDetail> activityLogDetailRepositoryAsync
            )
        {
            _logger = logger;
            _userSession = userSession;
            _userManager = userManager;
            _cacheManager = cacheManager;
            _userRepository = userRepository;
            _activityLogRepositoryAsync = activityLogRepositoryAsync;
            _routeRepositoryAsync = routeRepositoryAsync;
            _dailyBasePlanRepositoryAsync = dailyBasePlanRepositoryAsync;
            _activityTypeRepositoryAsync = activityTypeRepositoryAsync;
            _activityLogTypeRepositoryAsync = activityLogTypeRepositoryAsync;
            _activityLogDetailRepositoryAsync = activityLogDetailRepositoryAsync;
        }

        private ILogger<UserAppService> _logger { get; }

        public async Task<Result<List<ActivityLogTypeDTO>>> GetAllActivityTypes()
        {
            try
            {
                var activityTypes = await GetAllActivityTypesCached();
                var result = activityTypes.Where(x => x.Enabled).Select(x => new ActivityLogTypeDTO
                {
                    Id = x.Id,
                    Name = x.Name
                }).OrderBy(x => x.Name).ToList();

                return Result<List<ActivityLogTypeDTO>>.Success(result);
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred in {nameof(GetAllActivityTypes)}"); }

                return Result<List<ActivityLogTypeDTO>>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }

        public async Task<Result<PaginatedList<ActivityLogDTO>>> GetAllActivityLogs(ActivityLogSearchDTO request)
        {
            try
            {
                DateTime? fromDate;
                DateTime? toDate;
                var centuryStartYearDiff = ((DateTime.Now.Year / 100) * 100) - DateTime.Now.Year;
                fromDate = string.IsNullOrEmpty(request.FromDate) ? DateTime.Now.AddYears(centuryStartYearDiff).Date : Convert.ToDateTime(request.FromDate);
                toDate = string.IsNullOrEmpty(request.ToDate) ? DateTime.Now.Date : Convert.ToDateTime(request.ToDate);

                var queryable = (from activityLog in _activityLogRepositoryAsync.GetAllQueryable().Where(x => !x.IsDeleted)
                                 join users in _userRepository.GetAllQueryable() on activityLog.Userkey equals users.Id
                                 join activityLogDetail in _activityLogDetailRepositoryAsync.GetAllQueryable().Where(x => x.ActivityType == 1) on activityLog.ActivityLogDetailkey equals activityLogDetail.Id into tempActivityLogDetail
                                 from activityLogDetail in tempActivityLogDetail.DefaultIfEmpty()
                                 join route in _routeRepositoryAsync.GetAllQueryable() on Convert.ToInt32(activityLogDetail.ActivityId) equals route.Id into tempRoute
                                 from route in tempRoute.DefaultIfEmpty()
                                 join planActivityLogDetail in _activityLogDetailRepositoryAsync.GetAllQueryable().Where(x => x.ActivityType == 2) on activityLog.ActivityLogDetailkey equals planActivityLogDetail.Id into tempPlanActivityLogDetail
                                 from planActivityLogDetail in tempPlanActivityLogDetail.DefaultIfEmpty()
                                 join basePlan in _dailyBasePlanRepositoryAsync.GetAllQueryable() on Convert.ToInt32(planActivityLogDetail.ActivityId) equals basePlan.Id into tempBasePlan
                                 from basePlan in tempBasePlan.DefaultIfEmpty()
                                 select new ActivityLogDTO
                                 {
                                     Id = activityLog.Id,
                                     Comment = activityLog.Comment,
                                     Reason = activityLog.Reason,
                                     PerformedBy = users.FullName,
                                     PerformedOn = activityLog.CreatedOnUtc,
                                     TenantId = activityLogDetail!=null ? activityLogDetail.TenantId : 0,
                                     ActivityTypeId = activityLogDetail!=null ? activityLogDetail.ActivityType : 0,
                                     RouteName = route != null ? route.RouteName : null,
                                     //RouteId = basePlan != null ? Convert.ToString(basePlan.RouteId) : null,  
                                     ActivityLogTypekey = activityLog.ActivityLogTypekey,
                                     Userkey = activityLog.Userkey
                                 })
                                .WhereIf(request.AreaIds.Count > 0, x => request.AreaIds.Contains((long)x.TenantId))
                                .WhereIf(fromDate != null && toDate != null, x => x.PerformedOn.Date >= fromDate && x.PerformedOn.Date <= toDate)
                                .WhereIf(request.ActivityType.Count > 0, x => x.ActivityLogTypekey == request.ActivityType[0])
                                .WhereIf(request.PerformedBy.Count > 0, x => x.Userkey == request.PerformedBy[0])
                                .WhereIf(!string.IsNullOrEmpty(request.RouteNumber), x => x.RouteName == request.RouteNumber);

                var totalCount = await queryable.CountAsync();

                #region Sorting

                queryable = string.IsNullOrEmpty(request.Sorting) ? queryable.OrderByDescending(t => t.PerformedOn) : queryable.OrderBy(x => x.PerformedOn);

                #endregion Sorting

                var result = await PaginatedList<ActivityLogDTO>.CreateAsync(queryable, request.PageIndex, request.PageSize);

                return Result<PaginatedList<ActivityLogDTO>>.Success(result);
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(GetAllActivityLogs))); }

                return Result<PaginatedList<ActivityLogDTO>>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }

        public virtual async Task<Result<ActivityLog>> InsertActivity(ActivityLogTypeEnum systemKeyword, ActivityLogDetailsDTO details, string reason, params object[] commentParams)
        {
            try
            {
                var Currentuser = GetUser();
                var activityTypes = await GetAllActivityTypesCached();
                var activityType = activityTypes.Find(at => at.Id == (int)systemKeyword);
                if (activityType == null || !activityType.Enabled)
                    return null;
                ///comment = EnsureNotNull(comment);
                var comment = string.Format(activityType.Template, commentParams);
                //comment =  EnsureMaximumLength(comment, 4000);

                var activity = new ActivityLog
                {
                    Reason = reason,
                    Comment = comment,
                    Userkey = Currentuser.Id,
                    CreatedOnUtc = DateTime.UtcNow,
                    ActivityLogTypekey = (int)systemKeyword,
                    ActivityLogDetail = new ActivityLogDetail
                    {
                        ActivityId = details.ActivityId,
                        ActivityType = details.ActivityTypeId,
                        TenantId = _userSession.TenantId,
                    }
                };
                var response = await _activityLogRepositoryAsync.AddAsync(activity);
                return Result<ActivityLog>.Success(activity);
            }
            catch
            {
                return Result<ActivityLog>.Failure();
            }
        }

        protected virtual Task<List<ActivityLogTypeForCaching>> GetAllActivityTypesCached()
        {
            //cache
            string key = string.Format(ACTIVITYTYPE_ALL_KEY);
            return _cacheManager.Get(key, async () =>
           {
               var result = new List<ActivityLogTypeForCaching>();
               var activityLogTypes = await _activityLogTypeRepositoryAsync.GetAllAsync();
               foreach (var alt in activityLogTypes.ToList())
               {
                   var altForCaching = new ActivityLogTypeForCaching
                   {
                       Id = alt.Id,
                       Name = alt.Name,
                       Enabled = alt.Enabled,
                       Template = alt.Template,
                       SystemKeyword = alt.SystemKeyword
                   };
                   result.Add(altForCaching);
               }
               return result;
           });
        }

        private ApplicationUser GetUser()
        {
            return _userManager.Users.FirstOrDefault(x => x.Id == _userSession.UserId);
        }

        #region Private Helper Methods

        private string EnsureNotNull(string str)
        {
            return str ?? string.Empty;
        }

        private string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var pLen = postfix == null ? 0 : postfix.Length;

                var result = str.Substring(0, maxLength - pLen);
                if (!string.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }

            return str;
        }

        #endregion Private Helper Methods
    }
}