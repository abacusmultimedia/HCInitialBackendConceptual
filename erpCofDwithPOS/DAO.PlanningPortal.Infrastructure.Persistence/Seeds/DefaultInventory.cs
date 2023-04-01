using DAO.PlanningPortal.Domain.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultInventory
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var ExistingEntities = await context.ItemGroup.ToListAsync();

            var entityList = new List<ItemGroup>
            {
                new ItemGroup { Title = "Rental Property", ChildGroups =
                new List< ItemGroup>{
                    new ItemGroup { Title ="Name" , ChildItem = new List<Item> {
                        new Item { Title="Cat 5 Flats G-5 " ,
                            Batches=new List<ItemBatch>
                            {
                                new ItemBatch { Title="F#1, Block # 9, Cat 5 PM Staff colony G-5"},
                                new ItemBatch { Title="F#2, Block # 9, Cat 5 PM Staff colony G-5"},
                                new ItemBatch { Title="F#3, Block # 9, Cat 5 PM Staff colony G-5"},
                                new ItemBatch { Title="F#4, Block # 9, Cat 5 PM Staff colony G-5"},
                                new ItemBatch { Title="F#5, Block # 9, Cat 5 PM Staff colony G-5"},
                                new ItemBatch { Title="F#6, Block # 9, Cat 5 PM Staff colony G-5"},
                            }
                        }
                                ,
                         new Item { Title="Cat 2 Flats G-7 " ,
                            Batches=new List<ItemBatch>
                            {
                                new ItemBatch { Title="F#211, Block # 1, Cat 2 PM Staff colony G-5"},
                                new ItemBatch { Title="F#212, Block # 2, Cat 2 PM Staff colony G-5"},
                                new ItemBatch { Title="F#213, Block # 1, Cat 2 PM Staff colony G-5"},
                                new ItemBatch { Title="F#214, Block # 1, Cat 2 PM Staff colony G-5"},
                                new ItemBatch { Title="F#215, Block # 1, Cat 2 PM Staff colony G-5"},
                                new ItemBatch { Title="F#216, Block # 2, Cat 2 PM Staff colony G-5"},
                            }
                         }
                     } 
                    }
                    }
                },
            };

            var result = entityList.Where(p => ExistingEntities.All(p2 => p2.Title != p.Title)).ToList();

            if (!result.Any()) return;
            try
            {
            await context.ItemGroup.AddRangeAsync(result);
            context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
