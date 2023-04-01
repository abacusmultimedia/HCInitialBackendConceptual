using DAO.PlanningPortal.Application.Interfaces;
using zero.Shared.AppService;
using zero.Shared.Data;
using zero.Shared.Models.Security;
using zero.Shared.Models.User; 
using zero.Shared.Response;
using zero.Shared.Security;
using DAO.PlanningPortal.Common.Extensions;
using DAO.PlanningPortal.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using zero.Application.Shared.Resources;

namespace DAO.PlanningPortal.Application.Services
{
    public class AccountAppService : BaseAppService, IAccountAppService
    {
        #region Constructor, Dependencies, and Properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextTransaction"></param>
        /// <param name="logger"></param>
        /// <param name="tokenProvider"></param>
        /// <param name="serviceWorkerService"></param>
        /// <param name="deviceService"></param>
        /// <param name="serviceWorkerAppService"></param>
        /// <param name="userSessionManagementAppService"></param>
        public AccountAppService(IContextTransaction contextTransaction,
                              ILogger<AccountAppService> logger,
                               UserManager<ApplicationUser> userManager,
                              ITokenProvider tokenProvider)
        {
            _contextTransaction = contextTransaction;
            _logger = logger;
            _userManager = userManager;
            _tokenProvider = tokenProvider;
        }

        private IContextTransaction _contextTransaction { get; }
        private ILogger<AccountAppService> _logger { get; }
        private ITokenProvider _tokenProvider { get; }
        private UserManager<ApplicationUser> _userManager { get; }

        /// <summary>
        /// Login User (Service Worker) and returns a token response
        /// </summary>
        /// <param name="Current Password">User Login Model</param>
        /// <param name="NewPassword">Http Request object to access Headers etc</param>
        /// <returns>Returns a token response result</returns>
        ///
        public Task<Result<bool>> ChangePassword(ChangePasswordDTO parameter)
        {
            return _tokenProvider.ChangePassword(parameter);
        }

        #endregion Constructor, Dependencies, and Properties

        #region Public Methods

 

        /// <summary>
        /// Login User (Service Worker) and returns a token response
        /// </summary>
        /// <param name="userLogin">User Login Model</param>
        /// <param name="httpRequest">Http Request object to access Headers etc</param>
        /// <returns>Returns a token response result</returns>
        ///
        public async Task<Result<TokenResponse>> LoginUser(UserLogin userLogin, HttpRequest httpRequest)
        {
            try
            {
                // Get Token Response
                var tokenResponseResult = await _tokenProvider.Authenticate(userLogin.Username, userLogin.Password, null);
                if (tokenResponseResult.Succeeded == false) { return tokenResponseResult; }
                return tokenResponseResult;
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(LoginUser))); }

                return Result<TokenResponse>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }


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

                return Result<string>.Success(string.Format(UsersResource.NewUserCreatedSuccessfully, user.FullName));
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, string.Format(GlobalResource.AnErrorOccuredInMethod, nameof(CreateUser))); }

                return Result<string>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }

        #endregion Public Methods
    }
}