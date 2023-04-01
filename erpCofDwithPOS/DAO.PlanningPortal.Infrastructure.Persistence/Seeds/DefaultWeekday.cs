using DAO.PlanningPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultWeekday
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            //Seed Default Tenants
            var languageEntities = await context.WeekDays.ToListAsync();

            var languages = new List<WeekDays>
            {
                new WeekDays { Title = "Monday", IsDeleted = false, CreatedOn = DateTime.Now},
                new WeekDays { Title = "Tuesday", IsDeleted = false, CreatedOn = DateTime.Now},
                new WeekDays { Title = "Wednesday", IsDeleted = false, CreatedOn = DateTime.Now},
                new WeekDays { Title = "Thursday", IsDeleted = false, CreatedOn = DateTime.Now},
                new WeekDays { Title = "Friday", IsDeleted = false, CreatedOn = DateTime.Now},
                new WeekDays { Title = "Saturday", IsDeleted = false, CreatedOn = DateTime.Now},
                new WeekDays { Title = "Sunday", IsDeleted = false, CreatedOn = DateTime.Now}
            };

            var result = languages.Where(p => languageEntities.All(p2 => p2.Title != p.Title)).ToList();

            if (!result.Any()) return;

            await context.WeekDays.AddRangeAsync(result);
            context.SaveChanges();
        }
    }
}