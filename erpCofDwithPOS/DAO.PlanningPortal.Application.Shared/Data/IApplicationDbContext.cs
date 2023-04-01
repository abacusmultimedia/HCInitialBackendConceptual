using DAO.PlanningPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace zero.Shared.Data;

public interface IApplicationDbContext
{
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<ApplicationUserRole> UserRoles { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Translation> Translations { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(ApplicationUser currentUser, CancellationToken cancellationToken = default);
}