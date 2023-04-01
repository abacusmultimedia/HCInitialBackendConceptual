using DAO.PlanningPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultTenant
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            //Seed Default Tenants
            var tenantEntities = await context.Tenants.ToListAsync();

            var tenants = new List<Tenant>
            {
                new Tenant { Name = "10", DisplayName= "Area 10", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "11", DisplayName= "Area 11", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "12", DisplayName= "Area 12", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "13", DisplayName= "Area 13", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "14", DisplayName= "Area 14", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "15", DisplayName= "Area 15", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "16", DisplayName= "Area 16", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "17", DisplayName= "Area 17", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "18", DisplayName= "Area 18", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "19", DisplayName= "Area 19", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "20", DisplayName= "Area 20", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "21", DisplayName= "Area 21", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "22", DisplayName= "Area 22", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "23", DisplayName= "Area 23", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "24", DisplayName= "Area 24", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now },
                new Tenant { Name = "25", DisplayName= "Area 25", IsActive = true, IsDeleted = false, CreatedOn = DateTime.Now }
            };

            var result = tenants.Where(p => tenantEntities.All(p2 => p2.Name != p.Name)).ToList();

            if (!result.Any()) return;

            await context.Tenants.AddRangeAsync(result);
            //await context.SaveChangesAsync();
            context.SaveChanges();
        }
    }
}