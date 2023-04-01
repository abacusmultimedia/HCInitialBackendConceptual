using DAO.PlanningPortal.Api.Common.Extension;
using DAO.PlanningPortal.API.Common.Middleware;
using DAO.PlanningPortal.Application;
using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace DAO.PlanningPortal.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.RespectBrowserAcceptHeader = true; // false by default
        }).AddNewtonsoftJson(action =>
        {
            action.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            action.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });

        services.AddSwaggerUI("HC Stystems");

        services.AddInfrastructure(Configuration);
        services.AddApplication();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        //app.ConfigureInfrastructre(userManager, roleManager).Wait();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", " HC Stystems v1");
            });
        }

        app.UseRequestInterceptor();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        List<string> origins = GetOrgins();

        app.UseCors(options =>
        {
            options.WithOrigins(origins.ToArray()).AllowAnyMethod().AllowCredentials().AllowAnyHeader().SetIsOriginAllowed((host) => true);
        });

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private List<string> GetOrgins()
    {
        List<string> origins = new();
        var data = Configuration.GetSection("Origns").Value;
        if (data != null)
        {
            foreach (var item in data.Split(',', System.StringSplitOptions.RemoveEmptyEntries))
            {
                if (item.Trim() != "")
                    origins.Add(item.Trim());
            }
        }

        return origins;
    }
}