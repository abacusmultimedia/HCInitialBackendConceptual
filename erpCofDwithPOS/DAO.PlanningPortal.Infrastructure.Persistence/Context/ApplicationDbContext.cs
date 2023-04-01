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

    //public DbSet<Route> Routes { get; set; }
    //public DbSet<WeekDays> WeekDays { get; set; }
    //public DbSet<RouteType> RouteTypes { get; set; }
    //public DbSet<TransportType> TransportTypes { get; set; }
    //public DbSet<BasePlan> BasePlans { get; set; }
    //public DbSet<ServiceWorker> ServiceWorkers { get; set; }
    //public DbSet<DailyPlan> DailyPlans { get; set; }
    //public DbSet<DraftPlan> DraftPlans { get; set; }
    //public DbSet<UserOuMapping> UserTenantMappings { get; set; }
    //public DbSet<OrdeningGroup> OrdeningGroups { get; set; }
    //public DbSet<AlternativeServiceWorkersforOrdeningGroup> AlternativeServiceWorkersforOrdeningGroups { get; set; }


    //Activity Log dbSets
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<ActivityType> ActivityType { get; set; }
    public DbSet<ActivityLogType> ActivityLogType { get; set; }
    public DbSet<ActivityLogDetail> ActivityLogDetail { get; set; }

    //Activity Log dbSets
    #endregion IApplicationDbContext Planinning Portal



 

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