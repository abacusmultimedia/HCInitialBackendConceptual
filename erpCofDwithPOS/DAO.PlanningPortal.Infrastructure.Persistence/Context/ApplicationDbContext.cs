using zero.Shared.Common;
using zero.Shared.Data;
using DAO.PlanningPortal.Common.Extensions.QuerableExtensions;
using DAO.PlanningPortal.Common.Sessions;
using DAO.PlanningPortal.Domain.Common.Entity;
using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Domain.Entities.Finance;
using DAO.PlanningPortal.Domain.Entities.GDPR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using POSERP.Entities;
using POSERP.Entities.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>, IApplicationDbContext
{
    private readonly IDateTime _dateTime;
    private readonly IUserSession _userSession;

    public ApplicationDbContext(
        DbContextOptions options,
        IDateTime dateTime,
        IUserSession userSession) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        _dateTime = dateTime;
        _userSession = userSession;
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Translation> Translations { get; set; }

    #region IApplicationDbContext Planning Portal

    public DbSet<Route> Routes { get; set; }
    public DbSet<WeekDays> WeekDays { get; set; }
    public DbSet<RouteType> RouteTypes { get; set; }
    public DbSet<TransportType> TransportTypes { get; set; }
    public DbSet<BasePlan> BasePlans { get; set; }
    public DbSet<ServiceWorker> ServiceWorkers { get; set; }
    public DbSet<DailyPlan> DailyPlans { get; set; }
    public DbSet<DraftPlan> DraftPlans { get; set; }
    public DbSet<UserOuMapping> UserTenantMappings { get; set; }
    public DbSet<OrdeningGroup> OrdeningGroups { get; set; }
    public DbSet<AlternativeServiceWorkersforOrdeningGroup> AlternativeServiceWorkersforOrdeningGroups { get; set; }


    //Activity Log dbSets
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<ActivityType> ActivityType { get; set; }
    public DbSet<ActivityLogType> ActivityLogType { get; set; }
    public DbSet<ActivityLogDetail> ActivityLogDetail { get; set; }

    //Activity Log dbSets
    #endregion IApplicationDbContext Planinning Portal




    #region GDPR

    public DbSet<DataAccessRequest> DataAccessRequest { get; set; }
    public DbSet<ReasonToAccess> ReasonToAccess { get; set; }
    public DbSet<Systems> Systems { get; set; }

    #endregion GDPR

    #region finance
    public DbSet<Item> Item { get; set; }
    public DbSet<Cities> Cities { get; set; }
    public DbSet<Branch> Branch { get; set; }
    public DbSet<Ledger> Ledgers { get; set; }
    public DbSet<ItemGroup> ItemGroup { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<CostCenter> CostCenter { get; set; }
    public DbSet<LedgerGroup> LedgerGroup { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<PersonalInfo> PersonalInfo { get; set; }
    public DbSet<CostCategory> CostCategory { get; set; }
    public DbSet<BrachBalances> BrachBalances { get; set; }
    public DbSet<WarehouseStock> WarehouseStock { get; set; }
    public DbSet<ParentTransaction> ParentTransaction { get; set; }
    public DbSet<TransactionsAttachments> TransactionsAttachments { get; set; }
    public DbSet<LedgerSMS> LedgerSMS { get; set; }


    #endregion



    /* Define a DbSet for each entity of the application */



    public virtual DbSet<ADM_ACC_Bank_Master> ADM_ACC_Bank_Master { get; set; }
    public virtual DbSet<INV_MSD_Brand> INV_MSD_Brand { get; set; }
    public virtual DbSet<INV_MSD_Color> INV_MSD_Color { get; set; }
    public virtual DbSet<INV_MSD_Item> INV_MSD_Item { get; set; }
    public virtual DbSet<INV_MSD_Model> INV_MSD_Model { get; set; }
    public virtual DbSet<INV_MSD_SubItemCode> INV_MSD_SubItemCode { get; set; }
    public virtual DbSet<SD_POS_Invoice> SD_POS_Invoice { get; set; }
    public virtual DbSet<SD_POS_InvoiceDetail> SD_POS_InvoiceDetail { get; set; }
    public virtual DbSet<INV_MSD_SubItemPrice> INV_MSD_SubItemPrice { get; set; }
    public virtual DbSet<INV_MSD_Category> INV_MSD_Category { get; set; }

    public virtual DbSet<INV_MSD_Department> INV_MSD_Department { get; set; }

    public virtual DbSet<INV_MSD_PackingSize> INV_MSD_PackingSize { get; set; }
    public virtual DbSet<SD_POS_Invoice_Template> SD_POS_Invoice_Template { get; set; }
    public virtual DbSet<SD_POS_InvoiceDetail_Template> SD_POS_InvoiceDetail_Template { get; set; }

//public virtual DbSet<ADM_CON_Approve_Type> ADM_CON_Approve_Type { get; set; }
//public virtual DbSet<ADM_CON_Round_Master> ADM_CON_Round_Master { get; set; }
//public virtual DbSet<ADM_CON_Status_Type> ADM_CON_Status_Type { get; set; }
//public virtual DbSet<ADM_GEN_Area_Master> ADM_GEN_Area_Master { get; set; }
//public virtual DbSet<ADM_GEN_City_Master> ADM_GEN_City_Master { get; set; }
//public virtual DbSet<ADM_GEN_Region_Master> ADM_GEN_Region_Master { get; set; }
//public virtual DbSet<ADM_GEN_State_Master> ADM_GEN_State_Master { get; set; }
//public virtual DbSet<ADM_INV_Bin_Master> ADM_INV_Bin_Master { get; set; }
//public virtual DbSet<ADM_INV_Item> ADM_INV_Item { get; set; }
//public virtual DbSet<ADM_INV_Shelf_Master> ADM_INV_Shelf_Master { get; set; }
//public virtual DbSet<ADM_INV_Unit_Master> ADM_INV_Unit_Master { get; set; }
//public virtual DbSet<ADM_INV_Warehouse_Master> ADM_INV_Warehouse_Master { get; set; }
//public virtual DbSet<ADM_ORG_Branch_Master> ADM_ORG_Branch_Master { get; set; }
//public virtual DbSet<ADM_ORG_Company_Master> ADM_ORG_Company_Master { get; set; }
//public virtual DbSet<ADM_ORG_Division_Master> ADM_ORG_Division_Master { get; set; }
//public virtual DbSet<ADM_ORG_Group_Master> ADM_ORG_Group_Master { get; set; }
//public virtual DbSet<ADM_SAM_Salesman_Master> ADM_SAM_Salesman_Master { get; set; }
//public virtual DbSet<AUD_EXO_Audit_Trial> AUD_EXO_Audit_Trial { get; set; }
//public virtual DbSet<dtproperty> dtproperties { get; set; }
//public virtual DbSet<InvoiceType> InvoiceTypes { get; set; }
//public virtual DbSet<LOG_User_Master> LOG_User_Master { get; set; }
//public virtual DbSet<LOG_User_Type> LOG_User_Type { get; set; }
//public virtual DbSet<SD_SAL_Quotation> SD_SAL_Quotation { get; set; }
//public virtual DbSet<SD_SAL_QuotationDeatil> SD_SAL_QuotationDeatil { get; set; }
//public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
//public virtual DbSet<ADM_ACC_COA_Master> ADM_ACC_COA_Master { get; set; }
//public virtual DbSet<ADM_CON_Default_Type> ADM_CON_Default_Type { get; set; }
//public virtual DbSet<ADM_CON_Due_Terms> ADM_CON_Due_Terms { get; set; }
//public virtual DbSet<Employee> Employees { get; set; }
//public virtual DbSet<FIN_Account_Master> FIN_Account_Master { get; set; }
//public virtual DbSet<INV_MSD_Item_Backup> INV_MSD_Item_Backup { get; set; }
//public virtual DbSet<INV_MSD_ItemType> INV_MSD_ItemType { get; set; }
//public virtual DbSet<INV_MSD_StockMethod> INV_MSD_StockMethod { get; set; }
//public virtual DbSet<INV_MSD_SubItemCode_Backup> INV_MSD_SubItemCode_Backup { get; set; }
//public virtual DbSet<INV_MSD_SubItemCode1> INV_MSD_SubItemCode1 { get; set; }
//public virtual DbSet<INV_MSD_SubItemPrice_backup> INV_MSD_SubItemPrice_backup { get; set; }
//public virtual DbSet<INV_MSD_SubItemPrice1> INV_MSD_SubItemPrice1 { get; set; }
//public virtual DbSet<INV_MSD_Unit> INV_MSD_Unit { get; set; }



public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyDatabaseConcepts();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(ApplicationUser currentUser, CancellationToken cancellationToken = default)
    {
        ApplyDatabaseConcepts();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyDatabaseConcepts()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyCreateConcepts(entry);
                    break;

                case EntityState.Modified:
                    ApplyUpdateConcepts(entry);
                    break;
            }
        }
    }

    private void ApplyCreateConcepts(EntityEntry entry)
    {
        if (entry.Entity is ICreateAuditableEntity)
            ApplyCreateAuditable((ICreateAuditableEntity)entry.Entity);
    }

    private void ApplyUpdateConcepts(EntityEntry entry)
    {
        if (entry.Entity is IUpdateAuditableEntity)
            ApplyUpdateAuditable((IUpdateAuditableEntity)entry.Entity);
    }

    private void ApplyCreateAuditable(ICreateAuditableEntity auditableEntity)
    {
        auditableEntity.CreatedBy = _userSession.UserId;
        auditableEntity.CreatedOn = _dateTime.UtcNow;
    }

    private void ApplyUpdateAuditable(IUpdateAuditableEntity auditableEntity)
    {
        auditableEntity.LastModifiedBy = _userSession.UserId;
        auditableEntity.LastModifiedOn = _dateTime.UtcNow;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //All Decimals will have 18,6 Range
        foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,6)");
        }

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        var assembly = Assembly.GetExecutingAssembly();
        builder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(builder);

        //builder.Entity<Tenant>()
        //.HasRequired(c => c.Draft)
        //.WithMany()
        //.WillCascadeOnDelete(false);

        //builder.Entity<DraftPlan>()
        //    .HasMany<Tenant>("Tenant")
        //    .WithOne()
        //    .OnDelete(DeleteBehavior.NoAction);

        //builder.Entity(typeof(DraftPlan))
        //    .HasOne(typeof(int), "TenantId")
        //    .WithMany()
        //    .HasForeignKey("TenantId")
        //    .OnDelete(DeleteBehavior.NoAction);

        //builder.Entity(typeof(DraftPlan)).
        //        HasOne(typeof(Tenant), "Id")
        //        .WithMany()
        //        .HasForeignKey("TenantId")
        //        .OnDelete(DeleteBehavior.NoAction);

        //builder.Entity<DraftPlan>().HasOne(b => b.Tenant).WithMany(p => p.Dra)
        //.HasForeignKey(p => p.Ten)
        //.OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<ApplicationRole>().ToTable("Roles");
        builder.Entity<ApplicationUserRole>().ToTable("UserRoles");
        builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
        builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
    }

    private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(ApplicationDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

    private void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
        where TEntity : class
    {
        if (entityType.BaseType == null && ShouldFilterEntity<TEntity>(entityType))
        {
            var filterExpression = CreateFilterExpression<TEntity>();
            if (filterExpression != null)
            {
                modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
            }
        }
    }

    protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
    {
        if (typeof(ISoftDeleteAuditableEntity).IsAssignableFrom(typeof(TEntity)))
        {
            return true;
        }

        if (typeof(IMayHaveTenant).IsAssignableFrom(typeof(TEntity)))
        {
            return true;
        }

        if (typeof(IMustHaveTenant).IsAssignableFrom(typeof(TEntity)))
        {
            return true;
        }

        return false;
    }

    protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
    {
        Expression<Func<TEntity, bool>> expression = null;

        if (typeof(ISoftDeleteAuditableEntity).IsAssignableFrom(typeof(TEntity)))
        {
            Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDeleteAuditableEntity)e).IsDeleted;
            expression = expression == null ? softDeleteFilter : CombineExpressions(expression, softDeleteFilter);
        }


        return expression;
    }

    protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
    {
        return ExpressionCombiner.Combine(expression1, expression2);
    }
}