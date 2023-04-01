using zero.Shared.Models.Security; 
using zero.Shared.Response;
using zero.Shared.Security;
using DAO.PlanningPortal.Common.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using zero.Application.Shared.Resources;

namespace DAO.PlanningPortal.Application.Services
{
    public class AuthenticateAppService
    {
        private readonly ILogger<AuthenticateAppService> _logger;
        public ITokenProvider _tokenProvider { get; }

        public AuthenticateAppService(ILogger<AuthenticateAppService> logger,
                                    ITokenProvider tokenProvider)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<TokenResponse>> AuthenticateUser(UserLogin login)
        {
            try
            {
                // Check current request headers

                // Authenticat user
                var authenticateResult = await _tokenProvider.Authenticate(login.Username, login.Password, null);
                if (authenticateResult.Succeeded)
                {
                }
                else
                {
                }

                return authenticateResult;
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled())
                { _logger.LogError(ex, $"An error occured in {nameof(AuthenticateUser)}"); }
                return Result<TokenResponse>.Failure(GlobalResource.SystemMalfunctionText);
            }
        }
    }
}