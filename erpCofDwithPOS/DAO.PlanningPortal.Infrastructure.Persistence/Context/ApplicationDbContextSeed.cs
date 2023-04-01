using DAO.PlanningPortal.Domain.Constants;
using DAO.PlanningPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace DAO.PlanningPortal.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async System.Threading.Tasks.Task SeedRoles(RoleManager<ApplicationRole> roleManager)
    {
        await roleManager.CreateAsync(new ApplicationRole(IdentityRoleName.Admin));
        await roleManager.CreateAsync(new ApplicationRole(IdentityRoleName.Distributor));
        await roleManager.CreateAsync(new ApplicationRole(IdentityRoleName.Approver));
        await roleManager.CreateAsync(new ApplicationRole(IdentityRoleName.Agent));
    }

    public static async System.Threading.Tasks.Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        if (roleManager.Roles.All(r => r.Name != IdentityRoleName.Admin))
        {
            await roleManager.CreateAsync(new ApplicationRole(IdentityRoleName.Admin));
        }

        var administrator = new ApplicationUser { UserName = "dao-admin", Email = "dao@localhost", FullName = "DAO Admin", IsActive = true };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "1234");
            await userManager.AddToRolesAsync(administrator, new[] { IdentityRoleName.Admin });
        }
    }
}