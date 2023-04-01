using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAO.PlanningPortal.Common.Extensions.QuerableExtensions;

/// <summary>
/// Some useful extension methods for <see cref="IQueryable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
    public static IOrderedEnumerable<T> SortEnumerable<T>(this IEnumerable<T> table, string sortBy, byte sortOrder)
    {
        IOrderedEnumerable<T> result = null;

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