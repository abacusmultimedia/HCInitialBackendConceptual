using zero.Shared.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Infrastructure.Persistence.Context;

public class DbContextTransaction : IContextTransaction
{
    public DbContextTransaction(IApplicationDbContext context)
    {
        _Context = context as ApplicationDbContext;
    }

    private ApplicationDbContext _Context { get; }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _Context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _Context.Database.CommitTransactionAsync();
    }

    public Task<int> SaveChangesAsync()
    {
        return _Context.SaveChangesAsync();
    }
}