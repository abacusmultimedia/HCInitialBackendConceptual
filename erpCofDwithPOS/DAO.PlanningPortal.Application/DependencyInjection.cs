using DAO.PlanningPortal.Application.Interfaces;
using DAO.PlanningPortal.Application.Interfaces.Finance;
using DAO.PlanningPortal.Application.Interfaces.Inventory;
using DAO.PlanningPortal.Application.Interfaces.POS;
using DAO.PlanningPortal.Application.Interfaces.Transaction;
using DAO.PlanningPortal.Application.Services;
using DAO.PlanningPortal.Application.Services.Finance;
using DAO.PlanningPortal.Application.Services.Inventory; 
using DAO.PlanningPortal.Application.Services.TransactionService;
using DAO.PlanningPortal.Utility.Caching;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DAO.PlanningPortal.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IBasePlanService, BasePlanService>();
            services.AddTransient<ICacheManager, MemoryCacheManager>();
            services.AddTransient<IAccountAppService, AccountAppService>();
            services.AddTransient<IActivityLogService, ActivityLogService>();
            services.AddTransient<IAccessRequestService, AccessRequestService>();

           

            #region finance 
            services.AddTransient<IVenderService, VenderService>();
            services.AddTransient<ILedgerService, LedgerService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ITransactionService, TransactionService>();
          
            #endregion
            #region Inventory 
            services.AddTransient<IInventoryService, Inventory>();
            #endregion

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient<ITenantAppService, TenantAppService>();
            return services;
        }
    }
}