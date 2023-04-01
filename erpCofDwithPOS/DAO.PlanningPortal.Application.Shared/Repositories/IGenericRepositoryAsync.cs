using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zero.Shared.Repositories;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task<T> GetByIdAsync(int id);

    Task<IReadOnlyList<T>> GetAllAsync();

    IReadOnlyList<T> GetAll();

    IQueryable<T> GetAllQueryable();

    Task<IReadOnlyList<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

    Task<T> AddAsync(T entity);

    Task<List<T>> AddRangeAsync(List<T> entities);

    Task UpdateAsync(T entity);

    Task UpdateBulkAsync(List<T> entities);

    Task DeleteAsync(T entity);

    Task DeleteRangeAsync(List<T> entities);
}