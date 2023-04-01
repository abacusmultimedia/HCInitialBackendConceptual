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
             

            var languages = new List<RouteType>
            {
                new RouteType { Title = "Budrute", IsDeleted = false, CreatedOn = DateTime.Now},
                new RouteType { Title = "Budrute / land", IsDeleted = false, CreatedOn = DateTime.Now},
                new RouteType { Title = "Tilkørselsrute", IsDeleted = false, CreatedOn = DateTime.Now}
            };

         
             

             
            context.SaveChanges();
        }
    }
}