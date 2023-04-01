using zero.Shared.Models.Security;
using zero.Shared.Repositories; 
using zero.Shared.Response;
using zero.Shared.Security;
using DAO.PlanningPortal.Common.Extensions;
using DAO.PlanningPortal.Common.Models;
using DAO.PlanningPortal.Common.Sessions;
using DAO.PlanningPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using zero.Application.Shared.Resources;

namespace DAO.PlanningPortal.Infrastructure.Identity;

public class TokenProvider : ITokenProvider
{
    #region Constructor, Dependencies, and Properties

    private ILogger<TokenProvider> Logger { get; }
    private UserManager<ApplicationUser> userManager { get; }
    private readonly IUserSession _userSession;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IConfiguration configuration;
    private IGenericRepositoryAsync<ApplicationUser> _repository { get; }


    public TokenProvider(UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        IConfiguration configuration, IUserSession userSession,
                        ILogger<TokenProvider> logger,
                        IGenericRepositoryAsync<ApplicationUser> repository)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.configuration = configuration;
        Logger = logger;
        _userSession = userSession;
        _repository = repository;
    }

    #endregion Constructor, Dependencies, and Properties

    #region Public Methods

    /// <summary>
    /// Authenticates a user with username,password and return the Token Reponse
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    /// <param name="parameters">Extra parameters if required</param>
    /// <returns>Returns token response result</returns>
    public async Task<Result<TokenResponse>> Authenticate(string username, string password, Dictionary<string, object> parameters)
    {
        try
        {
            ApplicationUser user = await _repository.GetAllQueryable().Include(x => x.UserOuMapping).Where(x => x.UserName.ToLower().Equals(username.ToLower())).FirstOrDefaultAsync();

            // Return User or Password is incorrect.
            if (user == null || await userManager.CheckPasswordAsync(user, password) == false)
            {
                if (Logger.IsWarningEnabled())
                { Logger.LogWarning($"Authentication failed for user: {username}"); }

                return Result<TokenResponse>.Failure(UsersResource.UsernamePasswordInvalidText);
            }

            Result<TokenResponse> tokenResponse = await AccessToken(user);

            return tokenResponse;
        }
        catch (Exception ex)
        {
            if (Logger.IsErrorEnabled())
            { Logger.LogError(ex, $"An error occured in {nameof(Authenticate)}"); }

            return Result<TokenResponse>.Failure();
        }
    }

    public async Task<Result<bool>> ChangePassword(ChangePasswordDTO parameter)
    {
        try
        {
            var response = await userManager.ChangePasswordAsync(userManager.Users.FirstOrDefault(x => x.Id == _userSession.UserId)
                 , parameter.CurrentPassword, parameter.NewPassword);
            return response.Succeeded ? Result<bool>.Success(response.Succeeded) : Result<bool>.Failure(response.Errors.First().Description);
        }
        catch
        {
            return Result<bool>.Failure();
        }
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Returns an access token for a user
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>Returns token response result</returns>
    private async Task<Result<TokenResponse>> AccessToken(ApplicationUser user)
    {
        // Return User/Driver is blocked.
        if (!user.IsActive)
        {
            return Result<TokenResponse>.Failure(UsersResource.InactiveDriverText);
        }

        // Get User Roles, Distributor Information, Service Worker Id
        var userRoles = await userManager.GetRolesAsync(user);


        var userOuMappings = user.UserOuMapping?.ToList();

        List<UserOuMappingDto> ouMappingData = new List<UserOuMappingDto>();

        foreach (var ou in userOuMappings)
        {
            ouMappingData.Add(new UserOuMappingDto
            {
                TenantId = ou.TenantId,
                OrganizationUnitId = ou.OrganizationUnitId
            });
        }

        // Adding user claims and Creating token
        string refreshTokenClaim = GenerateRefreshToken();
        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("UserOuMappingData", ouMappingData != null ? JsonConvert.SerializeObject(ouMappingData) : string.Empty),
                    new Claim(ClaimTypes.GivenName, user.FullName??string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, refreshTokenClaim)
                };
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        SecurityTokenDescriptor tokenDescriptor = GenerateToken(authClaims);

        // Generating Token Response
        TokenResponse tokenResponse = GenerateTokenResponse(tokenDescriptor, user, userRoles);
        tokenResponse.RefreshToken = refreshTokenClaim;

        return Result<TokenResponse>.Success(tokenResponse);
    }

    /// <summary>
    /// Generates Security Token Descriptor against the Claims
    /// </summary>
    /// <param name="authClaims">User Claims</param>
    /// <returns>Returns the SecurityTokenDescriptor to generate the token</returns>
    private SecurityTokenDescriptor GenerateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]));

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(authClaims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration["JWTSettings:Issuer"],
            Audience = configuration["JWTSettings:Audience"],
        };

        return tokenDescriptor;
    }

    /// <summary>
    /// Generate Token Response for a user and returns the Token Response object with all the Expiry Dates
    /// </summary>
    /// <param name="tokenDescriptor">Token Descriptor</param>
    /// <param name="user">User</param>
    /// <param name="userRoles">User Roles</param>
    /// <returns>Returns the TokenResponse object</returns>
    private static TokenResponse GenerateTokenResponse(SecurityTokenDescriptor tokenDescriptor, ApplicationUser user, IList<string> userRoles)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        TokenResponse tokenResponse = new()
        {
            Roles = string.Join(',', userRoles),
            UserId = user.Id.ToString(),
            UserName = user.UserName,
            FullName = user.FullName,
            Email = user.Email,
            AccessToken = tokenHandler.WriteToken(token),
            ExpiresIn = (token.ValidTo - token.ValidFrom).TotalSeconds,
            Expires = DateTime.SpecifyKind(token.ValidTo, DateTimeKind.Utc),
            Issued = DateTime.SpecifyKind(token.ValidFrom, DateTimeKind.Utc),
            RefreshTokenExpires = DateTime.SpecifyKind(token.ValidFrom.AddDays(1), DateTimeKind.Utc)
        };
        return tokenResponse;
    }

    /// <summary>
    /// Generates a refresh token random string
    /// </summary>
    /// <returns>Return a random token string</returns>
    private static string GenerateRefreshToken()
    {
        using (var rngCryptoServiceProvider = new RSACryptoServiceProvider())
        {
            var randomBytes = new byte[40];
            randomBytes = rngCryptoServiceProvider.Encrypt(randomBytes, true);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }

    #endregion Private Methods
}