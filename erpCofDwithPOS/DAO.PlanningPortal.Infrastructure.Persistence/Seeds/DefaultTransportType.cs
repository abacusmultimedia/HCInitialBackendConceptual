using DAO.PlanningPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultTransportType
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            //Seed Default TrasportTypes
            var transportTypeEntities = await context.TransportTypes.ToListAsync();

            var languages = new List<TransportType>
            {
                new TransportType { Title = "Bil", IsDeleted = false, CreatedOn = DateTime.Now},
                new TransportType { Title = "Scooter", IsDeleted = false, CreatedOn = DateTime.Now}
            };

            var result = languages.Where(p => transportTypeEntities.All(p2 => p2.Title != p.Title)).ToList();

            if (!result.Any()) return;

            await context.TransportTypes.AddRangeAsync(result);
            context.SaveChanges();
        }
    }
}