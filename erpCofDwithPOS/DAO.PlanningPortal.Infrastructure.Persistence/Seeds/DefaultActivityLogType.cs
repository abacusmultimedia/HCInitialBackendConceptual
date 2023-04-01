using DAO.PlanningPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{

    public static class DefaultActivityLogType
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            //Seed Default Tenants
            var Prevdata = await context.ActivityLogType.ToListAsync();

            var NewData = new List<ActivityLogType>
            {
                 new ActivityLogType { SystemKeyword="NewOrderCreated",Name="New Order Created",Enabled=true,Template="Order:{0} has been Created",GroupId=50},
                 new ActivityLogType { SystemKeyword="OrderMovedToInPreparationStatus",Name="Order Moved To In Preparation Status",Enabled=true,Template="Order {0} has been moved from {1} to In Preparation {2}",GroupId=50},
                 new ActivityLogType { SystemKeyword="OrderMovedToDispatchedToBKStatus",Name="Order Moved To Dispatched To BK Status",Enabled=true,Template="Order: {0} has been moved from {1} to Dispatched To BK Status {2}",GroupId=50},
                 new ActivityLogType { SystemKeyword="OrderMovedToReceivedByBKStatus",Name="Order Moved To Received By BK Status",Enabled=true,Template="Order: {0} has been moved from {1}  to Received By BK {2}",GroupId=50},
                 new ActivityLogType { SystemKeyword="OrderMovedToDispatchedToDistributorStatus",Name="Order Moved To Dispatched To Distributor Status",Enabled=true,Template="Order: {0} has been moved from {1} to Dispatched To Distributor {2}",GroupId=50},
                 new ActivityLogType { SystemKeyword="OrderCompleted",Name="Order Completed",Enabled=true,Template="Order: {0} has been Completed{1}",GroupId=50},
                 new ActivityLogType { SystemKeyword="OrderCancelled",Name="Order Cancelled",Enabled=true,Template="Order: {0} has been Cancelled",GroupId=50},
            };

            var result = NewData.Where(p => Prevdata.All(p2 => p2.SystemKeyword != p.SystemKeyword)).ToList();

            if (!result.Any()) return;

            await context.ActivityLogType.AddRangeAsync(result);
            context.SaveChanges();
        }
    }
}
