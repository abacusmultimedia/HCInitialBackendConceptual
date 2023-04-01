using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace zero.Shared.Data;

public interface IContextTransaction
{
    Task<int> SaveChangesAsync();

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task CommitAsync();
}