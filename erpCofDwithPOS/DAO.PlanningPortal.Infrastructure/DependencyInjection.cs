using DAO.PlanningPortal.Application.Interfaces;
using DAO.PlanningPortal.Application.Services;
using zero.Shared.Common;
using zero.Shared.Data;
using zero.Shared.Models;
using zero.Shared.Repositories;
using zero.Shared.Security;
using DAO.PlanningPortal.Common.Sessions;
using DAO.PlanningPortal.Domain.Constants;
using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Infrastructure.Identity;
using DAO.PlanningPortal.Infrastructure.Persistence;
using DAO.PlanningPortal.Infrastructure.Persistence.Context;
using DAO.PlanningPortal.Infrastructure.Persistence.Repository;
using DAO.PlanningPortal.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace DAO.PlanningPortal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("DAO_Planning"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), sqlServerOptions =>
                    {
                        sqlServerOptions.CommandTimeout(300);
                        sqlServerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    }));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
        {
            config.SignIn.RequireConfirmedEmail = false;
            config.Password.RequireDigit = false;
            config.Password.RequireLowercase = false;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequireUppercase = false;
            config.Password.RequiredLength = 4;
            config.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IUserSession, UserSession>();
        services.AddTransient<IContextTransaction, DbContextTransaction>();
        services.AddTransient<ITokenProvider, TokenProvider>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IActivityLogService, ActivityLogService>();
        services.AddTransient<IBasePlanRepository ,BasePlanRepository> ();

        AddJwtAuthentication(services, configuration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicyName.AdminPolicy, policy => policy.RequireRole(IdentityRoleName.Admin));
            options.AddPolicy(AuthPolicyName.UserPolicy, policy => policy.RequireRole(IdentityRoleName.Distributor, IdentityRoleName.Approver));
            options.AddPolicy(AuthPolicyName.AdminUserPolicy, policy => policy.RequireRole(IdentityRoleName.Admin, IdentityRoleName.Distributor, IdentityRoleName.Approver));
        });

        return services;
    }

    private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
        var jwtSettings = new JWTSettings();
        configuration.Bind("JWTSettings", jwtSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("You are not Authorized");
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("You are not authorized to access this resource");
                    },
                };
            });
    }
}