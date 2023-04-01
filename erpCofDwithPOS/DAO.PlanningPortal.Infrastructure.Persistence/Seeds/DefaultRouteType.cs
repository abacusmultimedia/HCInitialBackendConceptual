using DAO.PlanningPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultRouteType
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            //Seed Default RouteTypes
            var routeTypeEntities = await context.RouteTypes.ToListAsync();

            var languages = new List<RouteType>
            {
                new RouteType { Title = "Budrute", IsDeleted = false, CreatedOn = DateTime.Now},
                new RouteType { Title = "Budrute / land", IsDeleted = false, CreatedOn = DateTime.Now},
                new RouteType { Title = "Tilkørselsrute", IsDeleted = false, CreatedOn = DateTime.Now}
            };

            var result = languages.Where(p => routeTypeEntities.All(p2 => p2.Title != p.Title)).ToList();

            if (!result.Any()) return;

            await context.RouteTypes.AddRangeAsync(result);
            context.SaveChanges();
        }
    }
}