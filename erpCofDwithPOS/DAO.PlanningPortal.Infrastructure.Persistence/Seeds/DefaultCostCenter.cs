using DAO.PlanningPortal.Domain.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultCostCenter
    {

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context.CostCenter.Any())
            {
                return;
            }
            else
            {
                //Seed Default RouteTypes
                var routeTypeEntities = await context.RouteTypes.ToListAsync();
                var CostCategories = new List<CostCategory> {
                    new CostCategory{
                        Title="Default Cost Category",
                        CostCenter = new List<CostCenter>{
                            new CostCenter{
                                Title = "Default"
                            }
                        }
                    }
                };
                var result = CostCategories.Where(p => routeTypeEntities.All(p2 => p2.Title != p.Title)).ToList();
                if (!result.Any()) return;
                try
                {
                    await context.CostCategory.AddRangeAsync(result);
                    context.SaveChanges();

                }
                catch (Exception ex) { }
            }
        }
    }
}
