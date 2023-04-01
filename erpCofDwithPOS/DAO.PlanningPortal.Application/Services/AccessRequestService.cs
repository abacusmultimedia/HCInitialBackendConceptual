using DAO.PlanningPortal.Application.Interfaces;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Repositories;
using zero.Shared.Response;
using DAO.PlanningPortal.Domain.Entities.GDPR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Services
{
    public class AccessRequestService : IAccessRequestService
    {

        private readonly IGenericRepositoryAsync<DataAccessRequest> _dataAccessRepositoryAsync;
        private readonly IGenericRepositoryAsync<Systems> _systemsRepositoryAsync;
        private readonly IGenericRepositoryAsync<ReasonToAccess> _reasonToAccessRepositoryAsync;

        public AccessRequestService(IGenericRepositoryAsync<DataAccessRequest> dataAccessRepositoryAsync,
                                    IGenericRepositoryAsync<Systems> systemsRepositoryAsync,
                                    IGenericRepositoryAsync<ReasonToAccess> reasonToAccessRepositoryAsync
        )
        {
            _dataAccessRepositoryAsync = dataAccessRepositoryAsync;
            _systemsRepositoryAsync = systemsRepositoryAsync;
            _reasonToAccessRepositoryAsync = reasonToAccessRepositoryAsync;
        }

        public async Task<Result<List<AccessFormDto>>> GetAllEntries(LogsGridFilters filters)
        {
            //var k = filters.StartDate.Value.Date;
            var response = _dataAccessRepositoryAsync.GetAllQueryable().Where(x =>
             (
             filters.user == x.UserId || filters.user == 0)
            && (filters.SystemID == x.SystemID || filters.SystemID == 0)
            //&& (filters.StartTime.Value.TimeOfDay == x.StartTime.TimeOfDay || filters.StartTime == null)
            //&& (filters.StartDate.Value.Date == x.StartDate.Date || filters.StartDate == null)
            && (x.AdminBy.ToUpper().Contains(filters.ApprovedBy.ToUpper().Trim()) || filters.ApprovedBy.Trim() == "")
            && (x.ReasonToAccessID == filters.ReasonToAccessID || filters.ReasonToAccessID == 0))
                   .Include(x => x.user)
                   .Include(y => y.SelectedSystem)
                   .Include(e => e.ReasonToAccess)
                   .Select(x => new AccessFormDto
                   {
                       Id = x.Id,
                       UserId = x.UserId,
                       StartDate = x.StartDate,
                       AdminBy = x.AdminBy,
                       SystemID = x.SystemID,
                       StartTime = x.StartTime,
                       ApprovedBy = x.ApprovedBy,
                       UserName = x.user.FullName,
                       Environment = x.Environment,
                       AccessDuration = x.AccessDuration,
                       ReasonInDetails = x.ReasonInDetails,
                       ReasonTitle = x.ReasonToAccess.Title,
                       SystemTitle = x.SelectedSystem.Title,
                       ReasonToAccessID = x.ReasonToAccessID,
                       ViewedCustomerData = x.ViewedCustomerData,
                   }).ToList();

            return Result<List<AccessFormDto>>.Success(response);
        }

        public async Task<Result<List<LookupDto>>> GetReasonsLookUp()
        {
            var response = _reasonToAccessRepositoryAsync.GetAll().Select(x =>
             new LookupDto
             {
                 Key = x.Id.ToString(),
                 Title = x.Title
             }).ToList();
            return Result<List<LookupDto>>.Success(response);
        }

        public async Task<Result<List<LookupDto>>> GetSystemsLookUp()
        {
            var response = _systemsRepositoryAsync.GetAll().Select(x =>
             new LookupDto
             {
                 Key = x.Id.ToString(),
                 Title = x.Title
             }).ToList();
            return Result<List<LookupDto>>.Success(response);
        }



        public async Task<Result<bool>> RegisterEntery(AccessFormDto parameter)
        {
            try
            {
                var entity = new DataAccessRequest()
                {
                    Id = parameter.Id,
                    UserId = parameter.UserId,
                    RoleID = parameter.RoleID,
                    StartTime = parameter.StartTime,
                    StartDate = parameter.StartDate,
                    AccessDuration = parameter.AccessDuration,
                    ApprovedBy = parameter.ApprovedBy,
                    SystemID = parameter.SystemID,
                    Environment = parameter.Environment,
                    ReasonToAccessID = parameter.ReasonToAccessID,
                    ReasonInDetails = parameter.ReasonInDetails,
                    ViewedCustomerData = parameter.ViewedCustomerData,
                    AdminBy = parameter.AdminBy
                };
                await _dataAccessRepositoryAsync.AddAsync(entity);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Success(false);
            }

        }
    }
}
