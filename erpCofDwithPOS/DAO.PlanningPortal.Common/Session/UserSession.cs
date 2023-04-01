using DAO.PlanningPortal.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DAO.PlanningPortal.Common.Sessions;

public class UserSession : IUserSession
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsPrincipal _currentUser;

    public UserSession(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        if (httpContextAccessor.HttpContext != null)
            _currentUser = httpContextAccessor.HttpContext.User;
    }

    public int? TenantId
    {
        get
        {
            var tenantId = _httpContextAccessor.HttpContext?.Request.Headers["TenantId"].ToString();
            return !string.IsNullOrEmpty(tenantId) && !tenantId.Contains("null") ? Convert.ToInt32(tenantId) : null;
        }
    }

    public int? OrganizationUnitId
    {
        get
        {
            var organizationUnitId = _httpContextAccessor.HttpContext?.Request.Headers["OrganizationUnitId"].ToString();
            return !string.IsNullOrEmpty(organizationUnitId) && !organizationUnitId.Contains("null") ? Convert.ToInt32(organizationUnitId) : null;
        }
    }

    public int? UserId
    {
        get
        {
            if (_currentUser == null)
                return null;

            if (_currentUser.FindFirst("UserId") != null)
                return Convert.ToInt32(_currentUser.FindFirst("UserId").Value);

            return null;
        }
    }

    public List<UserOuMappingDto> UserOuMappingData
    {
        get
        {
            if (_currentUser == null)
                return null;

            if (_currentUser.FindFirst("UserOuMappingData") != null)
                return JsonConvert.DeserializeObject<List<UserOuMappingDto>>(_currentUser.FindFirst("UserOuMappingData").Value);

            return null;
        }
    }
}