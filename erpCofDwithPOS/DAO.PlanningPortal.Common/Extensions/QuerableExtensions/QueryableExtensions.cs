using System;
using System.Linq;
using System.Linq.Expressions;

namespace DAO.PlanningPortal.Common.Extensions.QuerableExtensions;

/// <summary>
/// Some useful extension methods for <see cref="IQueryable{T}"/>.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
    /// </summary>
    public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
    {
        if (query == null)
        {
            throw new ArgumentNullException("query");
        }

        return query.Skip(skipCount).Take(maxResultCount);
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition
            ? query.Where(predicate)
            : query;
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
    {
        return condition
            ? query.Where(predicate)
            : query;
    }

    public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> table, string sortBy, byte sortOrder)
    {
        IOrderedQueryable<T> result = null;

        var sortId = typeof(T).GetProperties().FirstOrDefault(y => y.Name == "Id");
        result = table.OrderBy(x => sortId.GetValue(x));

        var sortProperty = typeof(T).GetProperties().Where(y => y.Name == sortBy).FirstOrDefault();
        if (sortProperty == null) return result;

        if (sortOrder == (byte)SortOrder.ASC)
        {
            result = table.OrderBy(x => sortProperty.GetValue(x));
        }
        else if (sortOrder == (byte)SortOrder.DESC)
        {
            result = table.OrderByDescending(x => sortProperty.GetValue(x));
        }

        return result;
    }
}