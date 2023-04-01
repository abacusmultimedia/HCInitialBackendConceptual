using BK.Utility.Caching;
using DAO.PlanningPortal.Application.Interfaces;
using DAO.PlanningPortal.Application.LocalDTos;
using zero.Shared.AppService;
using zero.Shared.Models;
using zero.Shared.Repositories; 
using zero.Shared.Response;
using DAO.PlanningPortal.Common.Extensions;
using DAO.PlanningPortal.Common.Sessions;
using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Utility.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zero.Application.Shared.Resources;

namespace DAO.PlanningPortal.Application.Services
{
    public class TenantAppService : BaseAppService, ITenantAppService
    {
        #region Constructor, Dependencies, and Properties
        private IGenericRepositoryAsync<Tenant> _repository { get; }
        private readonly IUserSession _userSession;
        private IGenericRepositoryAsync<UserOuMapping> _userOrganizationalUnitMappingRepositoryAsync { get; }
        private IGenericRepositoryAsync<OrganizationUnit> _organizationalUnits { get; }
        private ILogger<UserAppService> _logger { get; }
        private readonly ICacheManager _cacheManager;
        private const string TENANTTYPE_ALL_KEY = "Nop.tenanttype.all";
        private const string ORGANIZATIONALUNITTYPE_ALL_KEY = "Nop.organizationalunittype.all";
        public TenantAppService(ILogger<UserAppService> logger,
                              IGenericRepositoryAsync<Tenant> repository,
                              IUserSession userSession,
                              IGenericRepositoryAsync<UserOuMapping> userOrganizationalUnitMappingRepositoryAsync,
                              IGenericRepositoryAsync<OrganizationUnit> organizationalUnits,
                              ICacheManager cacheManager)
        {
            _logger = logger;          
            _repository = repository;
            _userSession = userSession;
            _userOrganizationalUnitMappingRepositoryAsync = userOrganizationalUnitMappingRepositoryAsync;
            _organizationalUnits = organizationalUnits;
            _cacheManager = cacheManager;
        }

        #endregion Constructor, Dependencies, and Properties

        #region Public Methods

        /// <summary>
        /// This method will return all the tenants of planning portal
        /// </summary>
        /// <returns>All tenants list</returns>
        public async Task<Result<List<TenantDto>>> GetAllTenants()
        {
            try
            {
                var tenantTypes = await GetAllTenantTypesCached();
                var result = tenantTypes.Select(x => new TenantDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    DisplayName = x.DisplayName
                }).ToList();

                return Result<List<TenantDto>>.Success(result);
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred in {nameof(GetAllTenants)}"); }

                return Result<List<TenantDto>>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }

        public async Task<Result<List<LoginUserTenantsDTO>>> GetAllCurrentlyLoginUserTenants()
        {
            try
            {
                int userId = (int)_userSession.UserId;
                var userTenantMappingQuerable = _userOrganizationalUnitMappingRepositoryAsync.GetAllQueryable().Where(x => x.UserId == userId);
                var userTenantQuerable = _repository.GetAllQueryable();

                var querable = from userTenantMapping in userTenantMappingQuerable
                               join userTenants in userTenantQuerable on userTenantMapping.TenantId equals userTenants.Id
                               select new LoginUserTenantsDTO
                               {
                                   Id = userTenants.Id,
                                   Name = userTenants.Name,
                                   DisplayName = userTenants.DisplayName
                               };
                var result = await querable.Distinct().ToListAsync();
                return Result<List<LoginUserTenantsDTO>>.Success(result);
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred in {nameof(GetAllTenants)}"); }

                return Result<List<LoginUserTenantsDTO>>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }


        public async Task<Result<List<OrganizationalUnitsWrtTenantsDTO>>> OrganizationalUnitsWrtTenants()
        {
            try
            {
                var organizationalUnitwrtTenants = await GetOrganizationalUnitsWrtTenantsCached();
                var result = organizationalUnitwrtTenants.Select(x => new OrganizationalUnitsWrtTenantsDTO
                {
                    TenantId = x.TenantId,  
                    TenantName = x.TenantName,
                    IsSelected = false,
                    OrganizationalUnits = x.OrganizationalUnits.Select(y => new OrganizationalUnitDTO
                    {
                        Id = y.Id,
                        Name = y.Name,
                        DisplayName = y.DisplayName,
                        IsSelected = false
                    }).ToList(),
                }).ToList();
                return Result<List<OrganizationalUnitsWrtTenantsDTO>>.Success(result);
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred in {nameof(GetAllTenants)}"); }

                return Result<List<OrganizationalUnitsWrtTenantsDTO>>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }


        #endregion Public Methods

        protected virtual Task<List<TenantTypeForCaching>> GetAllTenantTypesCached()
        {
            //cache
            string key = string.Format(TENANTTYPE_ALL_KEY);
            return _cacheManager.Get(key, async () =>
            {
                var result = new List<TenantTypeForCaching>();
                var tenantTypes = await _repository.GetAllAsync();
                foreach (var alt in tenantTypes.ToList())
                {
                    var altForCaching = new TenantTypeForCaching
                    {
                        Id = alt.Id,
                        Name = alt.Name,
                        DisplayName= alt.DisplayName
                    };
                    result.Add(altForCaching);
                }
                return result;
            });
        }

        protected virtual Task<List<OrganizationalUnitsWrtTenantsForCaching>> GetOrganizationalUnitsWrtTenantsCached()
        {
            //cache
            string key = string.Format(ORGANIZATIONALUNITTYPE_ALL_KEY);
            return _cacheManager.Get(key, async () =>
            {
                var result = new List<OrganizationalUnitsWrtTenantsForCaching>();

                var querable = from tenants in _repository.GetAllQueryable().Include(x => x.OrganizationUnits)
                               select new OrganizationalUnitsWrtTenantsForCaching
                               {
                                   TenantId = tenants.Id,
                                   TenantName = tenants.DisplayName,
                                   IsSelected = false,
                                   OrganizationalUnits = tenants.OrganizationUnits != null ? tenants.OrganizationUnits.Select(x => new OrganizationalUnitTypeForCaching { 
                                       Id = x.Id, 
                                       Name = x.Name, 
                                       DisplayName = x.DisplayName,
                                       IsSelected = false
                                   }).ToList() : null,                                  
                               };

                foreach (var alt in querable.ToList())
                {
                    var altForCaching = new OrganizationalUnitsWrtTenantsForCaching
                    {
                        TenantId = alt.TenantId,
                        TenantName = alt.TenantName,
                        OrganizationalUnits = alt.OrganizationalUnits
                    };
                    result.Add(altForCaching);
                }
                return result;
            });
        }
    }
}