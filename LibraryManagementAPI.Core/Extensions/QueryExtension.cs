using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LibraryManagementAPI.Core.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string propertyName, bool ascending = true)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, propertyName);
            var exp = Expression.Lambda(prop, param);
            string method = ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { source.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, source.Expression, exp);
            return source.Provider.CreateQuery<T>(mce);
        }

        public static IEnumerable<IGrouping<object?, T>> GroupByDynamic<T>(this IEnumerable<T> source, string propertyName)
        {
            return source.GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
        }
    }
}
