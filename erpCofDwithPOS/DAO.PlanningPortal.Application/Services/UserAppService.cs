using DAO.PlanningPortal.Application.Interfaces;
using zero.Shared.AppService;
using zero.Shared.Data;
using zero.Shared.Models;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.User;
using zero.Shared.Repositories; 
using zero.Shared.Response;
using DAO.PlanningPortal.Common.Extensions;
using DAO.PlanningPortal.Common.Extensions.QuerableExtensions;
using DAO.PlanningPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zero.Application.Shared.Resources;

namespace DAO.PlanningPortal.Application.Services
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        #region Constructor, Dependencies, and Properties

        public UserAppService(
                              ILogger<UserAppService> logger,
                              IActivityLogService activityLogService,
                              IContextTransaction contextTransaction,
                              IGenericRepositoryAsync<ApplicationUser> repository,
                              IGenericRepositoryAsync<OrganizationUnit> ouRepository,
                              IGenericRepositoryAsync<ApplicationRole> roleRepository,
                              IGenericRepositoryAsync<ApplicationUserRole> applicationUserRole,
                              UserManager<ApplicationUser> userManager,
                              IGenericRepositoryAsync<UserOuMapping> userTenantMapping,
                              IGenericRepositoryAsync<Tenant> tenantRepository)
        {
            _logger = logger;
            _activityLogService = activityLogService;
            _contextTransaction = contextTransaction;
            _repository = repository;
            _roleRepository = roleRepository;
            _applicationUserRole = applicationUserRole;
            _userManager = userManager;
            _ouRepository = ouRepository;
            _userTenantMapping = userTenantMapping;
            _tenantRepository = tenantRepository;
        }

        private UserManager<ApplicationUser> _userManager { get; }
        private IGenericRepositoryAsync<Tenant> _tenantRepository { get; }
        private IGenericRepositoryAsync<OrganizationUnit> _ouRepository { get; }
        private IGenericRepositoryAsync<ApplicationUser> _repository { get; }
        private IGenericRepositoryAsync<ApplicationRole> _roleRepository { get; }
        private IGenericRepositoryAsync<ApplicationUserRole> _applicationUserRole { get; }
        private IGenericRepositoryAsync<UserOuMapping> _userTenantMapping { get; }
        private IContextTransaction _contextTransaction { get; }
        private ILogger<UserAppService> _logger { get; }
        private readonly IActivityLogService _activityLogService;
        #endregion Constructor, Dependencies, and Properties

        #region Public Methods

        /// <summary>
        /// This method returns all the users based on filters and sorting criteria applied by end user
        /// </summary>
        /// <param name="request">request filter parameters</param>
        /// <returns>collection of users based on required filters</returns>
        public async Task<Result<PaginatedList<ApplicationUserDto>>> GetAllUsers(UserSearchDto request)
        {
            request.PageSize = 999999;
            try
            {
                // fetch all users as Queryable
                var queryable = (from user in _repository.GetAllQueryable().Where(x => !x.IsDeleted).Include(x => x.UserOuMapping)
                                 join userRole in _applicationUserRole.GetAllQueryable() on user.Id equals userRole.UserId
                                 join role in _roleRepository.GetAllQueryable() on userRole.RoleId equals role.Id
                                 select new ApplicationUserDto
                                 {
                                     Id = user.Id,
                                     FullName = user.FullName,
                                     UserName = user.UserName,
                                     PhoneNumber = user.PhoneNumber,
                                     Email = user.Email,
                                     Address = user.Address,
                                     IsActive = user.IsActive,
                                     UserRoles = role != null ? new List<ApplicationRoleDto> { new ApplicationRoleDto { Id = role.Id, Name = role.Name } } : null,

                                     OrganizationalUnitsWrtTenant = user.UserOuMapping != null ? user.UserOuMapping.GroupBy(x => x.TenantId).Select(x => new OrganizationalUnitsWrtTenants
                                     {
                                         TenantId = x.FirstOrDefault().Tenant.Id,
                                         TenantName = x.FirstOrDefault().Tenant.DisplayName,
                                         OrganizationalUnit = x.FirstOrDefault().OrganizationUnit != null ? x.Select(x => new OrganizationalUnits
                                         {
                                             Id = x.OrganizationUnit.Id,
                                             DisplayName = x.OrganizationUnit.DisplayName
                                         }).ToList() : null,
                                     }).ToList() : null,
                                 })

                                .WhereIf(!string.IsNullOrEmpty(request.FullName), x => x.FullName.Contains(request.FullName))
                                .WhereIf(!string.IsNullOrEmpty(request.UserName), x => x.UserName.Contains(request.UserName))
                                .WhereIf(!string.IsNullOrEmpty(request.PhoneNo), x => x.PhoneNumber.Contains(request.PhoneNo));
                //.WhereIf(!string.IsNullOrEmpty(request.UserRole), x => x.UserRoles.Any(y=>y.Name.ToLower().Equals(request.UserRole.ToLower())));

                var totalCount = await queryable.CountAsync();

                #region Sorting

                if (!string.IsNullOrEmpty(request.Sorting))
                {
                    queryable = request.Sorting switch
                    {
                        "FullName" => request.SortDirection == 1
                            ? queryable.OrderBy(x => x.FullName)
                            : queryable.OrderByDescending(x => x.FullName),
                        "PhoneNumber" => request.SortDirection == 1
                            ? queryable.OrderBy(x => x.PhoneNumber)
                            : queryable.OrderByDescending(x => x.PhoneNumber),
                        _ => queryable.OrderByDescending(x => x.FullName)
                    };
                }
                else
                {
                    queryable = queryable.OrderByDescending(t => t.FullName);
                }

                #endregion Sorting

                //apply pagination and get the final result
                var result = await PaginatedList<ApplicationUserDto>.CreateAsync(queryable, request.PageIndex, request.PageSize);

                result.Items = result.Items.GroupBy(x => x.Id).Select(user => new ApplicationUserDto
                {
                    Id = user.Key,
                    FullName = user.First().FullName,
                    UserName = user.First().UserName,
                    PhoneNumber = user.First().PhoneNumber,
                    Email = user.First().Email,
                    Address = user.First().Address,
                    IsActive = user.First().IsActive,
                    UserRoles = user.First().UserRoles != null ? user.Select(x => new ApplicationRoleDto { Id = x.UserRoles.First().Id, Name = x.UserRoles.First().Name }).ToList() : null,
                    OrganizationalUnitsWrtTenant = user.First().OrganizationalUnitsWrtTenant,
                }).ToList();

                return Result<PaginatedList<ApplicationUserDto>>.Success(result);

            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(GetAllUsers))); }

                return Result<PaginatedList<ApplicationUserDto>>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }


        public async Task<Result<List<LookupDto>>> GetUserLookUp()
        {
            var response = _repository.GetAll().Select(x =>
             new LookupDto
             {
                 Key = x.Id.ToString(),
                 Title = x.FullName
             }).ToList();
            return Result<List<LookupDto>>.Success(response);
        }

        /// <summary>
        /// This method will get information related to specific user.
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User detail with Roles and Areas info.</returns>
        public Task<Result<ApplicationUserDto>> GetUserById(int id)
        {
            try
            {
                var queryResult = (from user in _repository.GetAllQueryable().Where(x => !x.IsDeleted).Include(x => x.UserOuMapping)
                                   join userRole in _applicationUserRole.GetAllQueryable() on user.Id equals userRole.UserId
                                   join role in _roleRepository.GetAllQueryable() on userRole.RoleId equals role.Id
                                   where user.Id == id
                                   select new ApplicationUserDto
                                   {
                                       Id = user.Id,
                                       FullName = user.FullName,
                                       UserName = user.UserName,
                                       PhoneNumber = user.PhoneNumber,
                                       Email = user.Email,
                                       Address = user.Address,
                                       IsActive = user.IsActive,
                                       UserRoles = role != null ? new List<ApplicationRoleDto> { new ApplicationRoleDto { Id = role.Id, Name = role.Name } } : null,
                                       UserAreas = user.UserOuMapping != null ? user.UserOuMapping.Select(x => new TenantDto { Id = x.Tenant.Id, Name = x.Tenant.Name, DisplayName = x.Tenant.DisplayName }).ToList() : null,
                                   }).ToList();

                var result = queryResult.GroupBy(x => x.Id).Select(user => new ApplicationUserDto
                {
                    Id = user.Key,
                    FullName = user.First().FullName,
                    UserName = user.First().UserName,
                    PhoneNumber = user.First().PhoneNumber,
                    Email = user.First().Email,
                    Address = user.First().Address,
                    IsActive = user.First().IsActive,
                    UserRoles = user.First().UserRoles != null ? user.Select(x => new ApplicationRoleDto { Id = x.UserRoles.First().Id, Name = x.UserRoles.First().Name }).ToList() : null,
                    UserAreas = user.First().UserAreas
                }).FirstOrDefault();

                return Task.FromResult(Result<ApplicationUserDto>.Success(result));
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(GetUserById))); }

                return Task.FromResult(Result<ApplicationUserDto>.Failure(GlobalResource.SystemMalfunctionText));
            }
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="request">All the required parameters of user</param>
        /// <returns>Acknowledge the end user upon successful user creation or in case of failure show the appropriate error message.</returns>
        public async Task<Result<string>> CreateUser(CreateUserRequestDto request)
        {
            try
            {
                var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
                if (userWithSameUserName != null && !userWithSameUserName.IsDeleted)
                    return Result<string>.Failure(string.Format(UsersResource.UsernameAlreadyTaken, request.UserName));

                //if user already exists and soft deleted. reactivate user and reset password
                if (userWithSameUserName != null)
                {
                    userWithSameUserName.IsDeleted = false;
                    userWithSameUserName.IsActive = true;
                    userWithSameUserName.FullName = request.FullName;
                    userWithSameUserName.Email = request.Email;
                    userWithSameUserName.PhoneNumber = request.PhoneNumber;

                    if (request.AreaIds != null && request.CityIds != null)
                    {
                        var deleteEntities = await _userTenantMapping.GetAllQueryable().Where(x => x.UserId == userWithSameUserName.Id).ToListAsync();
                        await _userTenantMapping.DeleteRangeAsync(deleteEntities);
                        var ouData = _ouRepository.GetAllQueryable().Where(x => request.CityIds.Contains(x.Id));

                        List<UserOuMapping> tenantMapping = new List<UserOuMapping>();

                        foreach (var ou in ouData)
                        {
                            tenantMapping.Add(new UserOuMapping
                            {
                                TenantId = ou.TenantId,
                                OrganizationUnitId = ou.Id,
                                UserId = userWithSameUserName.Id
                            });
                        }

                        await _userTenantMapping.AddRangeAsync(tenantMapping);
                    }

                    var updateResult = await _userManager.UpdateAsync(userWithSameUserName);

                    var userRoles = await _userManager.GetRolesAsync(userWithSameUserName);
                    foreach (var role in userRoles)
                    {
                        var removeRoleResult = await _userManager.RemoveFromRoleAsync(userWithSameUserName, role);
                    }
                    var addRoleResult = await _userManager.AddToRolesAsync(userWithSameUserName, request.Roles);

                    //set user password
                    var token = await _userManager.GeneratePasswordResetTokenAsync(userWithSameUserName);

                    await _userManager.ResetPasswordAsync(userWithSameUserName, token, request.Password);

                    return Result<string>.Success(string.Format(UsersResource.UserAlreadyExistedAndUpdated, userWithSameUserName.UserName));
                }

                var user = new ApplicationUser
                {
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    FullName = request.FullName,
                    UserName = request.UserName,
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded) return Result<string>.Failure(GlobalResource.SystemMalfunctionText);

                await _userManager.AddToRolesAsync(user, request.Roles);

                List<UserOuMapping> tenantMappings = new List<UserOuMapping>();

                if (request.AreaIds != null && request.CityIds != null)
                {
                    var ouData = _ouRepository.GetAllQueryable().Where(x => request.CityIds.Contains(x.Id));
                    foreach (var ou in ouData)
                    {
                        tenantMappings.Add(new UserOuMapping
                        {
                            TenantId = ou.TenantId,
                            OrganizationUnitId = ou.Id,
                            UserId = user.Id
                        });
                    }
                    await _userTenantMapping.AddRangeAsync(tenantMappings);
                }
                return Result<string>.Success(string.Format(UsersResource.NewUserCreatedSuccessfully, user.FullName));
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(CreateUser))); }

                return Result<string>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }

        /// <summary>
        /// This method will update the existing user in the database
        /// </summary>
        /// <param name="request">All the parameters to be updated</param>
        /// <returns>Acknowledge the end user upon successful user update or in case of failure show the appropriate error message.</returns>
        public async Task<Result<string>> UpdateUser(UpdateRequestUserDto request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.Id);
                if (user == null) return Result<string>.Failure(string.Format(UsersResource.UserIsNotRegistered, request.FullName));

                user.FullName = request.FullName;
                user.Email = request.Email;
                user.PhoneNumber = request.PhoneNumber;

                //if (userType == Role.Admin) //Only Admin UI have the change password field
                //    user.Email = request.Email;

                if (request.AreaIds != null && request.CityIds != null)
                {
                    var deleteEntities = await _userTenantMapping.GetAllQueryable().Where(x => x.UserId == user.Id).ToListAsync();
                    await _userTenantMapping.DeleteRangeAsync(deleteEntities);
                    var ouData = _ouRepository.GetAllQueryable().Where(x => request.CityIds.Contains(x.Id));

                    List<UserOuMapping> tenantMapping = new List<UserOuMapping>();

                    foreach (var ou in ouData)
                    {
                        tenantMapping.Add(new UserOuMapping
                        {
                            TenantId = ou.TenantId,
                            OrganizationUnitId = ou.Id,
                            UserId = user.Id
                        });
                    }

                    await _userTenantMapping.AddRangeAsync(tenantMapping);
                }

                var result = await _userManager.UpdateAsync(user);

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, role);
                }
                var addRoleResult = await _userManager.AddToRolesAsync(user, request.Roles);

                if (result.Succeeded)
                    return Result<string>.Success(string.Format(UsersResource.UserUpdatedSuccessfully, user.FullName));

                return Result<string>.Failure(GlobalResource.SystemMalfunctionText, $"{result.Errors}");
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(UpdateUser))); }

                return Result<string>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }

        /// <summary>
        /// Delete the existing user
        /// </summary>
        /// <param name="id">Provide Id of the existing user to be deleted</param>
        /// <returns>Acknowledge the end user upon successful user deletion or in case of failure, show the appropriate error message.</returns>
        public async Task<Result<string>> DeleteUser(int id)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(id.ToString());
                if (existingUser == null) return Result<string>.Failure(string.Format(UsersResource.UserDoesNotExists, id.ToString()));

                //if (!(await ValidUserAction(existingUser, userType)))
                //    throw new ApiException("You are not allowed to perform this action.");

                existingUser.IsDeleted = true;
                existingUser.IsActive = false;

                var result = await _userManager.UpdateAsync(existingUser);
                if (result.Succeeded)
                    return Result<string>.Success(string.Format(UsersResource.UserDeletedSuccessfully, existingUser.FullName));

                return Result<string>.Failure(GlobalResource.SystemMalfunctionText, $"{result.Errors}");
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(DeleteUser))); }

                return Result<string>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }

        /// <summary>
        /// Toggle the existing user active status. If its already activated then deactivate it, otherwise activate it.
        /// </summary>
        /// <param name="id">Existing user id</param>
        /// <returns>Acknowledge the end user upon successful user active status toggled or in case of failure, show the appropriate error message.</returns>
        public async Task<Result<string>> ToggleActiveStatus(int id)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(id.ToString());
                if (existingUser == null) return Result<string>.Failure(string.Format(UsersResource.UserDoesNotExists, id.ToString()));

                existingUser.IsActive = !existingUser.IsActive;
                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                    return Result<string>.Success(string.Format(UsersResource.UserUpdatedSuccessfully, existingUser.FullName));

                return Result<string>.Failure(GlobalResource.SystemMalfunctionText, $"{result.Errors}");
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(ToggleActiveStatus))); }

                return Result<string>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }

        /// <summary>
        /// Get all the user roles available in planning portal application
        /// </summary>
        /// <returns>List of roles with Id and Name</returns>
        public async Task<Result<List<ApplicationRoleDto>>> GetAllRoles()
        {
            try
            {
                var result = await _roleRepository.GetAllQueryable().Select(x => new ApplicationRoleDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                return Result<List<ApplicationRoleDto>>.Success(result);
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(GetAllRoles))); }

                return Result<List<ApplicationRoleDto>>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }

        #endregion Public Methods
    }
}