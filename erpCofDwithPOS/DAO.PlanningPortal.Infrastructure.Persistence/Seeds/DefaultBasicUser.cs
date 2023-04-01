using DAO.PlanningPortal.Domain.Constants;
using DAO.PlanningPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var administrator = new ApplicationUser { UserName = "admin", Email = "admin@localhost", FullName = "ERP Admin", IsActive = true };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "1234");
                await userManager.AddToRolesAsync(administrator, new[] { IdentityRoleName.Admin });
            }
            for (int i = 0; i < 4; i++)
            {

                var tempUser = new ApplicationUser { UserName = "user" + (i + 1), Email = "admin@localhost", FullName = "User", IsActive = true };

                if (userManager.Users.All(u => u.UserName != tempUser.UserName))
                {
                    await userManager.CreateAsync(tempUser, "1234");
                    await userManager.AddToRolesAsync(tempUser, new[] { IdentityRoleName.Agent });
                }
            }
        }
    }
}