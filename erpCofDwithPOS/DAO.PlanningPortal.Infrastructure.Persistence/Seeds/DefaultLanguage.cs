using DAO.PlanningPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultLanguage
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            //Seed Default Languages
            var languageEntities = await context.Languages.ToListAsync();

            var languages = new List<Language>
            {
                new Language { Id= 1, Name = "English"},
                new Language { Id= 2, Name = "Danish" }
            };

            var result = languages.Where(p => languageEntities.All(p2 => p2.Name != p.Name)).ToList();

            if (!result.Any()) return;

            await context.Languages.AddRangeAsync(result);
            context.SaveChanges();
        }
    }
}