using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Common.Sessions;
using DAO.PlanningPortal.Common.Extensions;
using zero.Shared.Repositories;
using zero.Shared.ViewModels.Plan;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Repository
{


    public class BasePlanRepository : IBasePlanRepository
    {

        ILogger<BasePlanRepository> _logger;
        private readonly IUserSession _userSession;
        #region Constructor, Dependencies, and Properties 
        private readonly ApplicationDbContext _dbContext;
        private IGenericRepositoryAsync<TransportType> _transportTypeRepo { get; }
        private IGenericRepositoryAsync<RouteType> _routeTypeRepo { get; }

        public BasePlanRepository(
            IUserSession userSession,
            ApplicationDbContext dbContext,
            ILogger<BasePlanRepository> logger,
            IGenericRepositoryAsync<TransportType> _trasportTypeRepo,
            IGenericRepositoryAsync<RouteType> _routeTypeRepo
            )
        {
            _logger = logger;
            _dbContext = dbContext;
            _userSession = userSession;
            this._transportTypeRepo = _trasportTypeRepo;
            this._routeTypeRepo = _routeTypeRepo;
        }


        #endregion
        #region Public Methods 
        public void PostData(List<RawDailyPlanDTO> list, int count)
        {
            PostRandAsync(list);
        }

        #endregion
        #region Private Methods

        public void PostRandAsync(List<RawDailyPlanDTO> list)
        {

            try
            {

                Dictionary<string, int> transportIds = new Dictionary<string, int>();
                Dictionary<string, int> routesWIthIDs = new Dictionary<string, int>();
                foreach (var trasnporttype in list.Select(x => x.Transpost).Distinct().ToList())
                {
                    transportIds.Add(trasnporttype, CheckTransportAndPost(trasnporttype));
                }
                list.ForEach(async e =>
                {
                    await CheckandAddRouteType(e);
                    if (!routesWIthIDs.ContainsKey(e.Route))
                        routesWIthIDs.Add(e.Route, CheckandPostRoutes(e));
                });

                var tempDateStamp = DateOnly.FromDateTime(DateTime.Now);///to be removed  
                int count = 0;
                list.ForEach(e =>
              {
                  var entity = new BasePlan()
                  {
                      Return = e.Return,
                      WeekDayId = e.Day,
                      OrdeningGroupId = 2,////// TODO: Get Data from File
                      ServiceWorkerId = e.Courier,
                      CreatedOn = DateTime.UtcNow,
                      CreatedBy = _userSession.UserId,
                      RouteId = routesWIthIDs[e.Route],
                      TransportTypeId = transportIds[e.Transpost],
                      PayforOwnVehicle = e.PaymentAgainstPersonalVeh,
                      //Ordning = e.Order,
                      //OUId = citesWIthIDs[e.CityName],
                  };
                  _dbContext.BasePlans.Add(entity);
                  count++;

                  if (count > 100)
                  {
                      var cd = count;
                      _dbContext.SaveChanges();
                      count = 0;
                  }

              });
                var c = count;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred in PostRandAsync"); }
            }
        }

        /// <summary>
        /// /// Check if Transport Id already exists and return its ID and if not make a new entery and return new Id
        /// </summary> 
        private int CheckTransportAndPost(string name)
        {
            int response = 0;
            try
            {
                var transport = _dbContext.TransportTypes.FirstOrDefault(x => x.Title == name);
                if (transport == null)
                {
                    var entity = new TransportType()
                    {
                        Title = name,
                        IsDeleted = false,
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = _userSession.UserId,
                    };
                    _dbContext.TransportTypes.Add(entity);
                    _dbContext.SaveChanges();
                    response = entity.Id;
                }
                else
                {
                    response = transport.Id;
                }

            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred while CheckTransportAndPost, for city {name}"); }
            }

            return response;
        }

        /// <summary>
        /// /// Check if City Id already exists and return its ID and if not make a new entery and return new Id
        /// </summary> 
        /// 
        private int CheckCityAndPost(string name)
        {
            int response = 0;
            try
            {
                var city = _dbContext.OrganizationUnits.FirstOrDefault(x => x.Name == name);
                if (city == null)
                {
                    var entity = new OrganizationUnit()
                    {
                        Name = name,
                        TenantId = 1,
                        IsActive = true,
                        IsDeleted = false,
                        DisplayName = name,
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = _userSession.UserId,
                    };
                    _dbContext.OrganizationUnits.Add(entity);
                    _dbContext.SaveChanges();
                    response = entity.Id;
                }
                else
                {
                    response = city.Id;
                }

            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred while CheckCityAndPost, for city {name}"); }
            }

            return response;
        }
        /// <summary>
        /// /// Check if Route Id already exists and return its ID and if not make a new entery and return new Id
        /// </summary> 
        private int CheckandPostRoutes(RawDailyPlanDTO element)
        {
            var randomNumbers = new Random();
            var orUnits = _dbContext.OrganizationUnits.Select(x => new { x.Id, x.Name }).ToList();
            var routTypes = _dbContext.RouteTypes.Select(x => new { x.Id, x.Title }).ToList();
            int response = 0;
            try
            {
                var entity = _dbContext.Routes.FirstOrDefault(t => t.RouteName == element.Route);
                if (entity == null)
                {
                    var rout = new Route()
                    {
                        Mail = element.Mail,
                        ALB = element.ALBTid,
                        RouteSpeed = 0, //TODO : Hard Coded will be changed when proper data will be provided 
                        FNR = element.FNetRute,
                        JobId = element.District,
                        RouteName = element.Route,
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = _userSession.UserId,
                        LastModifiedOn = DateTime.UtcNow,
                        RouteLength = element.RouteLength,
                        LastModifiedBy = _userSession.UserId,
                        RouteTypeId = routTypes.FirstOrDefault(x => x.Title == element.RouteType).Id,
                        OrganizationUnitId = orUnits.FirstOrDefault(x => x.Name == element.CityName).Id,
                        TenantId = randomNumbers.Next(1, 16), // TODO : : Hard Coded will be changed when proper data will be provided 



                        //ServiceWorkerId = element.Courier,
                        //RouteTypeId = _dbContext.Transports.FirstOrDefault(j => j.Title == element.Transpost).Id,
                        //TransportId = _dbContext.RouteTypes.FirstOrDefault(j => j.Title == element.RouteType).Id,

                    };
                    _dbContext.Routes.Add(rout);
                    _dbContext.SaveChanges();
                    response = rout.Id;

                }
                else
                {
                    response = entity.Id;

                    entity.Mail = element.Mail;
                    entity.ALB = element.ALBTid;
                    entity.RouteSpeed = 0;  //TODO : Hard Coded will be changed when proper data will be provided 
                    entity.FNR = element.FNetRute;
                    entity.JobId = element.District;
                    entity.RouteName = element.Route;
                    entity.LastModifiedOn = DateTime.UtcNow;
                    entity.RouteLength = element.RouteLength;
                    entity.LastModifiedBy = _userSession.UserId;
                    entity.RouteTypeId = routTypes.FirstOrDefault(x => x.Title == element.RouteType).Id;
                    entity.OrganizationUnitId = orUnits.FirstOrDefault(x => x.Name == element.CityName).Id;
                    entity.TenantId = randomNumbers.Next(1, 16); // TODO : : Hard Coded will be changed when proper data will be provided 


                    //entity.ServiceWorkerId = element.Courier;
                    //entity.TransportId = _dbContext.RouteTypes.FirstOrDefault(j => j.Title == element.RouteType).Id;
                    //entity.RouteTypeId = _dbContext.Transports.FirstOrDefault(j => j.Title == element.Transpost).Id;

                    _dbContext.Entry(entity).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                entity = null;
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred in CheckandPostRoutes, for Route {element} "); }
            }
            return response;
        }
        /// <summary>
        /// /// Check if Rout Type Id already exists and return its ID and if not make a new entery and return new Id
        /// </summary> 
        private async Task CheckandAddRouteType(RawDailyPlanDTO element)
        {
            try
            {
                if (_routeTypeRepo.GetAllQueryable().Where(x => x.Title.ToLower() == element.RouteType.ToLower()).FirstOrDefault() == null)
                {
                    var entity = new RouteType()
                    {
                        Title = element.RouteType
                    };
                    await _routeTypeRepo.AddAsync(entity);
                }

                if (_transportTypeRepo.GetAllQueryable().Where(x => x.Title.ToLower() == element.Transpost.ToLower()).FirstOrDefault() == null)
                {
                    var entity = new TransportType()
                    {
                        Id = 1,//TODO : Hard Coded will be changed when proper data will be provided 
                        Title = element.Transpost,
                    };
                    await _transportTypeRepo.AddAsync(entity);
                }

            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occurred in CheckandAddRouteType, for RouteType {element}  "); }
            }
        }

        #endregion
    }


}
