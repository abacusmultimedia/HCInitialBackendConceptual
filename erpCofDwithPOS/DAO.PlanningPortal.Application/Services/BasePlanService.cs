using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using DAO.PlanningPortal.Common.Extensions;
using DAO.PlanningPortal.Application.Interfaces;
using zero.Shared.Models.Plan;
using zero.Shared.Repositories;
using zero.Shared.ViewModels.Plan;
using zero.Shared.Response; 
using DAO.PlanningPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DAO.PlanningPortal.Application.LocalDTos;
using DAO.PlanningPortal.Utility.Caching;
using BK.Utility.Caching;
using DAO.PlanningPortal.Common.Sessions;
using DAO.PlanningPortal.Domain.Enums;
using zero.Application.Shared.Resources;

namespace DAO.PlanningPortal.Application.Services;

public class BasePlanService : IBasePlanService
{ 
    private const string TRANSPORTTYPE_ALL_KEY = "Nop.transporttype.all";
    private const string WEEKDAYS_ALL_KEY = "Nop.weekdays.all";
    private readonly ICacheManager _cacheManager;
    private readonly IUserSession _userSession;
    private IGenericRepositoryAsync<ServiceWorker> _serviceWorkersRepository { get; }
    private IGenericRepositoryAsync<TransportType> _transportTypeRepository { get; }
    private IGenericRepositoryAsync<WeekDays> _weekDaysRepository { get; }
    private IGenericRepositoryAsync<Route> _routesRepository { get; }
    private IGenericRepositoryAsync<UserOuMapping> _tenantsOuRepository { get; }
    private IGenericRepositoryAsync<BasePlan> _basePlanRepository { get; }
    private IGenericRepositoryAsync<DraftPlan> _draftPlanRepository { get; }

    ILogger<BasePlanService> _logger;

    private readonly IActivityLogService _activityLogService;

    public BasePlanService(  ILogger<BasePlanService> logger, IActivityLogService activityLogService,
        IGenericRepositoryAsync<ServiceWorker> serviceWorkersRepository,
        IGenericRepositoryAsync<Route> routesRepository, ICacheManager cacheManager,
        IGenericRepositoryAsync<TransportType> transportTypeRepository,
        IGenericRepositoryAsync<WeekDays> weekDaysRepository,
        IUserSession userSession,
        IGenericRepositoryAsync<UserOuMapping> tenantsOuRepository,
        IGenericRepositoryAsync<BasePlan> basePlanRepository,
        IGenericRepositoryAsync<DraftPlan> draftPlanRepository)
    {
        _logger = logger; 
        _serviceWorkersRepository = serviceWorkersRepository;
        _routesRepository = routesRepository;
        _transportTypeRepository = transportTypeRepository;
        _weekDaysRepository = weekDaysRepository;
        _cacheManager = cacheManager;
        _userSession = userSession;
        _basePlanRepository = basePlanRepository;
        _tenantsOuRepository = tenantsOuRepository;
        _activityLogService = activityLogService;
        _draftPlanRepository = draftPlanRepository;
    }
    public async Task ImportAsync(UploadPlanDto plan)
    {
        var rawData = await ReadFileAsync(plan);
        var (lst, count) = MapData(rawData); 
    }

    public Task<Result<List<BasePlanDTO>>> LoadBasePlanData(BasePlanSearchDto request)
    {
        try
        {
            int userId = (int)_userSession.UserId;
            var DraftBasePlan = _basePlanRepository.GetAllQueryable().Where(x => !x.IsDeleted && !x.IsPublished && x.WeekDayId == Convert.ToInt32(request.WeekDayId));
            
            if(DraftBasePlan.Count() > 0)
            {
                var querable = (from tenantsOuMapping in _tenantsOuRepository.GetAllQueryable().Where(x => x.TenantId == request.TenantId && x.UserId == userId)
                                join routes in _routesRepository.GetAllQueryable().Where(e => !e.IsDeleted) on new { tenantsOuMapping.TenantId, tenantsOuMapping.OrganizationUnitId } equals new { routes.TenantId, routes.OrganizationUnitId }
                                join basePlan in _basePlanRepository.GetAllQueryable().Where(x => !x.IsDeleted && !x.IsPublished &&
                                x.WeekDayId == Convert.ToInt32(request.WeekDayId)) on routes.Id equals basePlan.RouteId
                                select new
                                {
                                    routes.OrganizationUnitId,
                                    routes.OrganizationUnit.DisplayName,
                                    basePlan.WeekDay.Title,
                                    basePlan
                                }).ToList().GroupBy(x => x.OrganizationUnitId).Select(organizationalUnits => new BasePlanDTO
                                {
                                    OrganizationalUnitName = organizationalUnits.FirstOrDefault().DisplayName,
                                    OrganizationUnitId = organizationalUnits.FirstOrDefault().OrganizationUnitId,
                                    OrdeningGroup = organizationalUnits.GroupBy(x => x.basePlan.OrdeningGroupId).Select(ordeningGroups => new OrdeningGroupDTO
                                    {
                                        OrdeningGroupId = ordeningGroups.Key,
                                        OrdeningCards = ordeningGroups.GroupBy(x => x.basePlan.OrdeningNo).Select(ordeningCards => new OrdeningCardDTO
                                        {
                                            OrdeningNo = ordeningCards.Key,
                                            CardTitle = "Ord. " + ordeningCards.FirstOrDefault().basePlan.OrdeningNo + "-"
                                                        + ordeningCards.FirstOrDefault().basePlan.OrdeningGroupId + "-"
                                                        + ordeningCards.FirstOrDefault().Title + " " + "239010",
                                            CardServiceWorkerId = ordeningCards.FirstOrDefault().basePlan.ServiceWorkerId,
                                            CardTransportTypeId = ordeningCards.FirstOrDefault().basePlan.TransportTypeId,
                                            CardWeekDayId = ordeningCards.FirstOrDefault().basePlan.WeekDayId,
                                            CardRoutes = ordeningCards.Select(x => new CRouteCardDTO { Id = x.basePlan.RouteId, Type = (int)RoutEnums.Route }).ToList(),
                                            DraftOrPublished = ordeningCards.Select(x => new DraftOrPublishedDTO { Id = x.basePlan.Id, IsPublished = x.basePlan.IsPublished}).ToList(),
                                        }).ToList(),
                                    }).ToList(),
                                    CRouteGroup = new CRouteGroupDTO()
                                    {
                                        CRouteCards = new List<CRouteCardDTO>() {
                                        new CRouteCardDTO {
                                            Id=444512,
                                        CRouteCardHoursDurationTime = "10:45 pm" , CRouteCardRouteNumber  = "C9877" ,
                                        CRouteCardServiceWorkersId = 1 , Type = (int)RoutEnums.CRoute
                                    } ,    new CRouteCardDTO {
                                        Id = 4455541,
                                        CRouteCardHoursDurationTime = "11:45 pm" , CRouteCardRouteNumber  = "C8877" ,
                                        CRouteCardServiceWorkersId = 1,Type = (int)RoutEnums.CRoute
                                    }

                                    }
                                    },
                                }).ToList();

                var result = querable.ToList();
                return Task.FromResult(Result<List<BasePlanDTO>>.Success(result));

            }
            else
            {
                var querable = (from tenantsOuMapping in _tenantsOuRepository.GetAllQueryable().Where(x => x.TenantId == request.TenantId && x.UserId == userId)
                                join routes in _routesRepository.GetAllQueryable().Where(e => !e.IsDeleted) on new { tenantsOuMapping.TenantId, tenantsOuMapping.OrganizationUnitId } equals new { routes.TenantId, routes.OrganizationUnitId }
                                join basePlan in _basePlanRepository.GetAllQueryable().Where(x => !x.IsDeleted && x.IsPublished &&
                                x.WeekDayId == Convert.ToInt32(request.WeekDayId)) on routes.Id equals basePlan.RouteId
                                select new
                                {
                                    routes.OrganizationUnitId,
                                    routes.OrganizationUnit.DisplayName,
                                    basePlan.WeekDay.Title,
                                    basePlan
                                }).ToList().GroupBy(x => x.OrganizationUnitId).Select(organizationalUnits => new BasePlanDTO
                                {
                                    OrganizationalUnitName = organizationalUnits.FirstOrDefault().DisplayName,
                                    OrganizationUnitId = organizationalUnits.FirstOrDefault().OrganizationUnitId,
                                    OrdeningGroup = organizationalUnits.GroupBy(x => x.basePlan.OrdeningGroupId).Select(ordeningGroups => new OrdeningGroupDTO
                                    {
                                        OrdeningGroupId = ordeningGroups.Key,
                                        OrdeningCards = ordeningGroups.GroupBy(x => x.basePlan.OrdeningNo).Select(ordeningCards => new OrdeningCardDTO
                                        {
                                            OrdeningNo = ordeningCards.Key,
                                            CardTitle = "Ord. " + ordeningCards.FirstOrDefault().basePlan.OrdeningNo + "-"
                                                        + ordeningCards.FirstOrDefault().basePlan.OrdeningGroupId + "-"
                                                        + ordeningCards.FirstOrDefault().Title + " " + "239010",
                                            CardServiceWorkerId = ordeningCards.FirstOrDefault().basePlan.ServiceWorkerId,
                                            CardTransportTypeId = ordeningCards.FirstOrDefault().basePlan.TransportTypeId,
                                            CardWeekDayId = ordeningCards.FirstOrDefault().basePlan.WeekDayId,
                                            CardRoutes = ordeningCards.Select(x => new CRouteCardDTO { Id = x.basePlan.RouteId, Type = (int)RoutEnums.Route }).ToList(),
                                            DraftOrPublished = ordeningCards.Select(x => new DraftOrPublishedDTO { Id = x.basePlan.Id, IsPublished = x.basePlan.IsPublished }).ToList(),
                                        }).ToList(),
                                    }).ToList(),
                                    CRouteGroup = new CRouteGroupDTO()
                                    {
                                        CRouteCards = new List<CRouteCardDTO>() { 
                                        new CRouteCardDTO {
                                            Id=444512, 
                                        CRouteCardHoursDurationTime = "10:45 pm" , CRouteCardRouteNumber  = "C9877" ,
                                        CRouteCardServiceWorkersId = 1 , Type = (int)RoutEnums.CRoute, 
                                    } ,    new CRouteCardDTO {
                                        Id = 4455541,
                                        CRouteCardHoursDurationTime = "11:45 pm" , CRouteCardRouteNumber  = "C8877" ,
                                        CRouteCardServiceWorkersId = 1,Type = (int)RoutEnums.CRoute
                                    }

                                    }
                                    },
                                }).ToList();

                var result = querable.ToList();
                return Task.FromResult(Result<List<BasePlanDTO>>.Success(result));
            }
        }
        catch (Exception ex)
        {
            if (_logger.IsErrorEnabled())
            { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(LoadBasePlanData))); }

            return Task.FromResult(Result<List<BasePlanDTO>>.Failure(GlobalResource.SystemMalfunctionText));
        }
    }

    public async Task<Result<bool>> SaveDraftBasePlan(DraftBasePlanRequest request)
    {

        ///////// parsing Base Plan 
        #region  parsing Base Plan 

        var basePlansParsed = new List<BasePlan>();

        request.Data.ForEach(bpDto =>
        {
            bpDto.OrdeningGroup.ForEach(ordgrp =>
            {
                ordgrp.OrdeningCards.ForEach(card =>
                {
                    card.CardRoutes.ForEach(rout =>
                    {
                        var basePlan = new BasePlan()
                        {
                            RouteId = rout.Id,
                            IsDeleted = false,
                            IsPublished = false,
                            OrdeningNo = card.OrdeningNo,
                            WeekDayId = card.CardWeekDayId,
                            OrdeningGroupId = ordgrp.OrdeningGroupId,
                            TransportTypeId = card.CardTransportTypeId,
                            ServiceWorkerId = card.CardServiceWorkerId
                        };
                        basePlansParsed.Add(basePlan);
                    });
                });
            });
        });


        #endregion 
        //////// Ends  parsing Base Plan 




        try
        {
            var existingPlans = _draftPlanRepository.GetAllQueryable().Where(x => x.TenantId == request.TenantId && request.OrganizationUnitIds.Contains(x.OrganizationUnitId)
                                      && x.PlanType == (byte)PlanTypeEnum.Base && x.WeekDayId == request.WeekDayId && x.StatusId == (byte)DraftPlanStatusEnum.Pending).ToList();

            foreach (var ou in request.OrganizationUnitIds)
            {
                var draftPlan = existingPlans.FirstOrDefault(x => x.OrganizationUnitId == ou);
                if (draftPlan is not null)
                {
                    await UpdateDraftPlan(draftPlan);

                    List<int> existingBasePlan = (from basePlan in _basePlanRepository.GetAllQueryable()
                                                  join route in _routesRepository.GetAllQueryable() on basePlan.RouteId equals route.Id
                                                  where route.OrganizationUnitId == ou 
                                                  && basePlan.IsPublished == false
                                                  select new
                                                  {
                                                      basePlan.Id

                                                  }).Select(x => x.Id).ToList();

                    if (existingBasePlan.Count > 0) // if there is existing draft plan, then remove all rows
                    {
                        var basePlans = _basePlanRepository.GetAllQueryable().Where(x => existingBasePlan.Contains(x.Id)).ToList();

                        basePlans.ForEach(x => x.IsDeleted = true);
                        await _basePlanRepository.UpdateBulkAsync(basePlans);

                    }
                }
                else
                {
                    await AddDraftPlan(request, ou);
                }
                await _basePlanRepository.AddRangeAsync(basePlansParsed);


            }

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            if (_logger.IsErrorEnabled())
            { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(LoadBasePlanData))); }

            return Result<bool>.Failure(GlobalResource.SystemMalfunctionText);
        }
    }

    private async Task UpdateDraftPlan(DraftPlan exists)
    {
        exists.CreatedBy = _userSession.UserId;
        exists.CreatedOn = DateTime.UtcNow;

        await _draftPlanRepository.UpdateAsync(exists);
    }

    private async Task AddDraftPlan(DraftBasePlanRequest request, int ou)
    {
        var draftPlan = new DraftPlan
        {
            TenantId = request.TenantId,
            OrganizationUnitId = ou,
            WeekDayId = request.WeekDayId,
            StatusId = (byte)DraftPlanStatusEnum.Pending,
            PlanType = (byte)PlanTypeEnum.Base,
            RequestedBy = (int)_userSession.UserId,
            RequestedOn = DateTime.UtcNow,
            CreatedBy = _userSession.UserId,
            CreatedOn = DateTime.UtcNow
        };

        await _draftPlanRepository.AddAsync(draftPlan);
    }

    public async Task<Result<List<DropdownModel>>> GetAllServiceWorkers()
    {
        try
        {
            var querable = from serviceWorkers in _serviceWorkersRepository.GetAllQueryable()
                           select new DropdownModel
                           {
                               Id = serviceWorkers.Id,
                               Name = serviceWorkers.FullName,
                           };
            var result = await querable.ToListAsync();
            return Result<List<DropdownModel>>.Success(result);
        }
        catch (Exception ex)
        {
            if (_logger.IsErrorEnabled())
            { _logger.LogError(ex, $"An error occurred in {nameof(GetAllServiceWorkers)}"); }

            return Result<List<DropdownModel>>.Failure(GlobalResource.SystemMalfunctionText);
        }
    }

    public async Task<Result<List<DropdownModel>>> GetAllRoutes()
    {
        try
        {
            var querable = from routes in _routesRepository.GetAllQueryable()
                           select new DropdownModel
                           {
                               Id = routes.Id,
                               Name = routes.RouteName,
                           };
            var result = await querable.ToListAsync();
            return Result<List<DropdownModel>>.Success(result);
        }
        catch (Exception ex)
        {
            if (_logger.IsErrorEnabled())
            { _logger.LogError(ex, $"An error occurred in {nameof(GetAllRoutes)}"); }

            return Result<List<DropdownModel>>.Failure(GlobalResource.SystemMalfunctionText);
        }
    }

    public async Task<Result<List<DropdownModel>>> GetAllTransportTypes()
    {
        try
        {
            var transportTypes = await GetAllTransportTypesCached();
            var result = transportTypes.Select(x => new DropdownModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Result<List<DropdownModel>>.Success(result);
        }
        catch (Exception ex)
        {
            if (_logger.IsErrorEnabled())
            { _logger.LogError(ex, $"An error occurred in {nameof(GetAllTransportTypes)}"); }

            return Result<List<DropdownModel>>.Failure(GlobalResource.SystemMalfunctionText);
        }
    }

    public async Task<Result<List<DropdownModel>>> GetAllWeekDays()
    {
        try
        {
            var weekDays = await GetAllWeekDaysCached();
            var result = weekDays.Select(x => new DropdownModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Result<List<DropdownModel>>.Success(result);
        }
        catch (Exception ex)
        {
            if (_logger.IsErrorEnabled())
            { _logger.LogError(ex, $"An error occurred in {nameof(GetAllWeekDays)}"); }

            return Result<List<DropdownModel>>.Failure(GlobalResource.SystemMalfunctionText);
        }
    }

    public async Task<Result<string>> ApproveBasePlan(List<int> ids)
    {
        try
        {
            var querable = _basePlanRepository.GetAllQueryable().Where(x => ids.Contains(x.Id)).ToList();
            querable.ForEach(x => x.IsPublished = true);
            await _basePlanRepository.UpdateBulkAsync(querable);
            return Result<string>.Success(string.Format(BasePlanResource.BasePlanApproved, "Updated Successfully"));
        }
        catch (Exception ex)
        {
            if (_logger.IsErrorEnabled())
            { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(ApproveBasePlan))); }

            return Result<string>.Failure(GlobalResource.SystemMalfunctionText);
        }
    }


    public async Task<Result<bool>> SyncServiceWorkers(List<SyncServiceWorker> request)
    {
        try
        {
            if (request.Count == 0)
                return Result<bool>.Success(false);

            var requestDataMatch = _serviceWorkersRepository.GetAllQueryable().Where(x => request.Select(y => y.SourceId).Contains(x.SourceId) &&
                               request.Select(z => z.ServiceProviderId).Contains(x.Tenant.Id)).ToList();

            if (requestDataMatch.Count > 0)
            {
                requestDataMatch.ForEach(data =>
                {
                    data.FullName = request.FirstOrDefault(x => x.SourceId == data.SourceId)?.FullName;
                });

                await _serviceWorkersRepository.UpdateBulkAsync(requestDataMatch);
            }

            var serviceWorkersToBeAdded = request.Where(x => !requestDataMatch.Select(y => y.SourceId).Contains(x.SourceId)
            && !requestDataMatch.Select(z => z.Id).Contains(x.ServiceProviderId)).ToList();

            List<ServiceWorker> serviceWorkers = new List<ServiceWorker>();

            foreach (var sw in serviceWorkersToBeAdded)
            {
                serviceWorkers.Add(new ServiceWorker
                {
                    SourceId = sw.SourceId,
                    CreatedBy = _userSession.UserId,
                    CreatedOn = DateTime.UtcNow,
                    LastModifiedBy = _userSession.UserId,
                    LastModifiedOn = DateTime.UtcNow,
                    //IsDeleted = false,
                    //FullName = sw.FullName,
                    //TenantId = sw.ServiceProviderId,
                });
            }
            await _serviceWorkersRepository.AddRangeAsync(serviceWorkers);
            return Result<bool>.Success(true);   
        }
        catch (Exception ex)
        {
            if (_logger.IsErrorEnabled())
            { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(ApproveBasePlan))); }

            return Result<bool>.Failure(GlobalResource.SystemMalfunctionText);
        }
    }


    protected virtual Task<List<TransportTypesForCaching>> GetAllTransportTypesCached()
    {
        //cache
        string key = string.Format(TRANSPORTTYPE_ALL_KEY);
        return _cacheManager.Get(key, async () =>
        {
            var result = new List<TransportTypesForCaching>();
            var transportTypes = await _transportTypeRepository.GetAllAsync();
            foreach (var alt in transportTypes.ToList())
            {
                var altForCaching = new TransportTypesForCaching
                {
                    Id = alt.Id,
                    Name = alt.Title
                };
                result.Add(altForCaching);
            }
            return result;
        });
    }

    protected virtual Task<List<WeekDaysForCaching>> GetAllWeekDaysCached()
    {
        //cache
        string key = string.Format(WEEKDAYS_ALL_KEY);
        return _cacheManager.Get(key, async () =>
        {
            var result = new List<WeekDaysForCaching>();
            var weekDays = await _weekDaysRepository.GetAllAsync();
            foreach (var alt in weekDays.ToList())
            {
                var altForCaching = new WeekDaysForCaching
                {
                    Id = alt.Id,
                    Name = alt.Title
                };
                result.Add(altForCaching);
            }
            return result;
        });
    }


    #region Private Helper Methods
    private (List<RawDailyPlanDTO>, int) MapData(List<UploadPlanCSV> rawList)
    {
        int count = 0;
        var subDivisonChar = ',';
        List<RawDailyPlanDTO> routes = new List<RawDailyPlanDTO>();
        try
        {

            rawList.ForEach(raw =>
         {
             if (count > 0)
             {

                 var order = raw.Order.Split(subDivisonChar);
                 var cities = raw.CityName.Split(subDivisonChar);
                 var returnBack = ValidateBooleans(raw.Return.Split(subDivisonChar));
                 var paymentAgainstPersonavehicle = ValidateBooleans(raw.PaymentAgainstPersonalVeh.Split(subDivisonChar));
                 for (int i = 0; i < order.Length; i++)
                 {
                     try
                     {
                         routes.Add(new RawDailyPlanDTO
                         {
                             Day = i + 1,
                             Route = raw.Route,
                             Transpost = raw.Transpost,
                             RouteType = raw.RouteType,
                             PriceGroup = raw.PriceGroup,
                             Mail = SafeIntparse(raw.Mail),
                             Courier = SafeIntparse(raw.Courier),
                             ALBTid = SafeDoubleparse(raw.ALBTid),
                             District = SafeIntparse(raw.District),
                             FNetRute = SafeIntparse(raw.FNetRute),
                             Order = order.Length > i ? order[i] : "",
                             CityName = cities.Length > i ? cities[i] : "",
                             RouteLength = SafeDoubleparse(raw.RouteLength),
                             Return = returnBack.Length > i ? returnBack[i] : false,
                             PaymentAgainstPersonalVeh = paymentAgainstPersonavehicle.Length > i ? paymentAgainstPersonavehicle[i] : false,
                         });
                     }
                     catch (Exception ex)
                     {
                         if (_logger.IsErrorEnabled())
                         { _logger.LogError(ex, $"An error occurred while Mapping, on Count : {count},  for Route {raw.Route}, sub Iteration of index : {i}"); }

                     }
                 }
             }

             count++;
         });
        }
        catch (Exception ex)
        {
            if (_logger.IsErrorEnabled())
            { _logger.LogError(ex, $"An error occurred while creating list of RawDailyPalns in MapData, {count} successful"); }

        }
        var k = routes;
        return (routes, count);
    }

    private static int SafeIntparse(string x)
    {
        _ = int.TryParse(x.Trim(), out int response);
        return response;
    }

    private static double SafeDoubleparse(string x)
    {
        _ = Double.TryParse(x.Trim(), out double response);
        return response;
    }


    private static bool[] ValidateBooleans(string[] rawList)
    {

        bool[] response = new bool[7];
        for (int i = 0; i < rawList.Length; i++)
        {
            response[i] = rawList[i].Trim().ToUpper().StartsWith('J');
        }
        return response;
    }

    private async Task<List<UploadPlanCSV>> ReadFileAsync(UploadPlanDto plan)
    {
        List<UploadPlanCSV> uploadPlanList = new List<UploadPlanCSV>();
        if (plan.File.Length > 0)
        {
            using var memoryStream = new MemoryStream(new byte[plan.File.Length]);
            await plan.File.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            List<string> dataString = new List<string>();
            using (var reader = new StreamReader(memoryStream))
            {
                while (!reader.EndOfStream)
                {
                    var record = Regex.Replace(reader.ReadLine(), @"\t|\n|\r", "");
                    if (!string.IsNullOrWhiteSpace(record))
                    {
                        dataString.Add(record);
                    }
                }
            }
            try
            {
                dataString.ForEach(x =>
                {
                    UploadPlanCSV uploadPlanCSV = new UploadPlanCSV();
                    uploadPlanCSV = FromCsv(x);
                    uploadPlanList.Add(uploadPlanCSV);
                });
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred in ReadFileAsync "); }
            }
        }
        return uploadPlanList;
    }
    private static UploadPlanCSV FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(';');

        //// this check will be implemented when we will get the sample file 
        //if (values.Length != 5 || string.IsNullOrEmpty(values[0]) || string.IsNullOrEmpty(values[1]))
        //{
        //    throw new Exception();
        //}

        UploadPlanCSV b = new UploadPlanCSV();
        b.Route = (!string.IsNullOrEmpty(values[0])) ? values[0] : "";
        b.District = (!string.IsNullOrEmpty(values[1])) ? values[1] : "";
        b.Courier = (!string.IsNullOrEmpty(values[2])) ? values[2] : "";
        b.Mail = (!string.IsNullOrEmpty(values[3])) ? values[3] : "";
        b.CityName = (!string.IsNullOrEmpty(values[4])) ? values[4] : "";
        b.RouteType = (!string.IsNullOrEmpty(values[5])) ? values[5] : "";

        b.Transpost = (!string.IsNullOrEmpty(values[24])) ? values[24] : "";
        b.RouteLength = (!string.IsNullOrEmpty(values[25])) ? values[25] : "";
        b.FNetRute = (!string.IsNullOrEmpty(values[26])) ? values[26] : "";
        b.Order = (!string.IsNullOrEmpty(values[27])) ? values[27] : "";
        b.Return = (!string.IsNullOrEmpty(values[28])) ? values[28] : "";
        b.ALBTid = (!string.IsNullOrEmpty(values[29])) ? values[29] : "";
        b.PriceGroup = (!string.IsNullOrEmpty(values[30])) ? values[30] : "";
        b.PaymentAgainstPersonalVeh = (!string.IsNullOrEmpty(values[31])) ? values[31] : "";

        return b;
    }

    public Task<Result<List<BasePlanDTO>>> LoadDailyPlanData(DailyPlanSearchDto request)
    {
        throw new NotImplementedException();
    }
    #endregion
}



////////////////// To do 
///
//public static string GetCsv<T>(this List<T> csvDataObjects)
//{
//    var propertyInfos = typeof(T).GetProperties();
//    var sb = new StringBuilder();
//    sb.AppendLine(GetCsvHeaderSorted(propertyInfos));
//    csvDataObjects.ForEach(d => sb.AppendLine(GetCsvDataRowSorted(d, propertyInfos)));
//    return sb.ToString();
//}
// to do ends 