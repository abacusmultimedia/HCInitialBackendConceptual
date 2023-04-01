using DAO.PlanningPortal.Domain.Constants;
using DAO.PlanningPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            await roleManager.CreateAsync(new ApplicationRole(IdentityRoleName.Admin));
            await roleManager.CreateAsync(new ApplicationRole(IdentityRoleName.Distributor));
            await roleManager.CreateAsync(new ApplicationRole(IdentityRoleName.Approver));
            await roleManager.CreateAsync(new ApplicationRole(IdentityRoleName.Agent));
        }
    }
}