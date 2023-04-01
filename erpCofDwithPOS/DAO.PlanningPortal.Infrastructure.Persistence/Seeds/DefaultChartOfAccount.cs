using DAO.PlanningPortal.Domain.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Seeds
{
    public static class DefaultChartOfAccount
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context.Ledgers.Any())
            {
                return;
            }
            else
            {
                //Seed Default RouteTypes
                var routeTypeEntities = await context.RouteTypes.ToListAsync();
                var ledgerGroups = new List<LedgerGroup>
            {
                new LedgerGroup {  Title="Assets", IsDeleted = false  , ChildLedgerGroups= new List<LedgerGroup>{
                                    new LedgerGroup { Title="Current Assets" , IsDeleted = false  ,
                        ChildLedgerGroups=new List<LedgerGroup>{
                            new LedgerGroup { Title="Cash and Bank Accounts",IsDeleted = false , ChildLedgers = new List<Ledger>{

                                new Ledger { Title = "Cash" , IsDeleted = false,Description ="Cash Account"}
                            } },


                            new LedgerGroup { Title="Account Receivables",IsDeleted = false , ChildLedgerGroups = new List<LedgerGroup>{
                                new LedgerGroup{ Title="Customers" , IsDeleted = false , ChildLedgers =
                                    new List<Ledger>{
                                        new Ledger{Title = "Test Customer" ,PersonalInfo = new PersonalInfo {
                                            Address = "Test Address", FirstName = "Mehmood" , LatName = "ul Hassan",
                                            Cell ="+92 513 889 8874", Email="name@gmai.com", Phone="+92 51-9209078",
                                        } } } } }
                                }
                            }
                        },
                        new LedgerGroup { Title="Fixed Assets" , IsDeleted = false  ,
                                    ChildLedgerGroups=new List<LedgerGroup>{
                                            new LedgerGroup { Title="Equipments",IsDeleted = false , ChildLedgerGroups = new List<LedgerGroup>{ }
                                            }  }  }

                        }
                },

                                new LedgerGroup {  Title="Liabilities", IsDeleted = false  , ChildLedgerGroups= new List<LedgerGroup>{
                                    new LedgerGroup { Title="Short Term Liabilities" , IsDeleted = false  ,
                                         ChildLedgerGroups=new List<LedgerGroup>{
                                     new LedgerGroup { Title="Account payable",IsDeleted = false , ChildLedgerGroups = new List<LedgerGroup>{
                                     new LedgerGroup{ Title="Venders" , IsDeleted = false , ChildLedgers =
                                        new List<Ledger>{
                                            new Ledger{Title = "Test Vender" ,PersonalInfo = new PersonalInfo {
                                            Address = "Test Address", FirstName = "Majid" , LatName = "Ali",
                                            Cell ="+92 513 889 8874", Email="name@gmai.com", Phone="+92 51-9209078",
                                                } } } } }
                                }
                            }
                        },
                        new LedgerGroup { Title="Long Term Liabilities" , IsDeleted = false  ,
                                    ChildLedgerGroups=new List<LedgerGroup>{
                                            new LedgerGroup { Title="Bank Loans",IsDeleted = false , ChildLedgerGroups = new List<LedgerGroup>{ }
                                            }  }  }
                        }
                },


                new LedgerGroup {  Title="Expenses", IsDeleted = false  , ChildLedgerGroups= new List<LedgerGroup>{
                                        new LedgerGroup { Title="Direct Expense" , IsDeleted = false  ,
                                        ChildLedgers = new List<Ledger>{ new Ledger{Title="Purchases"}
                                        }
                                    },
                                        new LedgerGroup { Title="Indirect Expense" , IsDeleted = false  ,
                                        ChildLedgers = new List<Ledger>{ new Ledger{Title="Rent"}, new Ledger{Title="Comission"}
                                        }
                                    }
                } },

                                new LedgerGroup {  Title="Incomes", IsDeleted = false  , ChildLedgerGroups= new List<LedgerGroup>{
                                        new LedgerGroup { Title="Direct Income" , IsDeleted = false  ,
                                        ChildLedgers = new List<Ledger>{ new Ledger{Title="Sales"}
                                        }
                                    }
                } },

            };
                var result = ledgerGroups.Where(p => routeTypeEntities.All(p2 => p2.Title != p.Title)).ToList();
                if (!result.Any()) return;
                try
                {
                    await context.LedgerGroup.AddRangeAsync(result);
                    context.SaveChanges();

                }
                catch (Exception ex) { }
            }
        }
    }
}
