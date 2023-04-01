using DAO.PlanningPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{ 
    public static class DefaultActivityTypes
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            //Seed Default Tenants
            var Prevdata = await context.ActivityType.ToListAsync();
            var NewData = new List<ActivityType>
            {
                new ActivityType { Title = "Base Plan" },
                new ActivityType { Title = "Daily Base Plan" },
                new ActivityType { Title = "Daily Customimzed Plan" }
            };
            var result = NewData.Where(p => Prevdata.All(p2 => p2.Title != p.Title)).ToList();
            if (!result.Any()) return;
            await context.ActivityType.AddRangeAsync(result);
            context.SaveChanges();
        }
    }

}
