using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Infrastructure.Persistence;
using DAO.PlanningPortal.Infrastructure.Persistence.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                                .Build();

        Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                if (dbContext.Database.IsSqlServer())
                {
                    dbContext.Database.Migrate();
                }

                Log.Information("--- Seeding Started --- ");

                await DefaultTenant.SeedAsync(dbContext);
                await DefaultLanguage.SeedAsync(dbContext);
                await DefaultWeekday.SeedAsync(dbContext);
                await DefaultTransportType.SeedAsync(dbContext);
                await DefaultRouteType.SeedAsync(dbContext);
                await DefaultChartOfAccount.SeedAsync(dbContext);
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
                await DefaultInventory.SeedAsync(dbContext);
                await DefaultCostCenter.SeedAsync(dbContext);
                await DefaultRoles.SeedAsync(roleManager);
                await DefaultBasicUser.SeedAsync(userManager, roleManager);

                Log.Information("--- Seeding Finished --- ");
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "An error occurred seeding the DB");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        try
        {
            Log.Information("Starting web host");
            host.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog();
}