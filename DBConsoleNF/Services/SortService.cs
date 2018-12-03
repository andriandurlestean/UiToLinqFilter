using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DBConsoleNF.Services.Interfaces;
using DBConsoleNF.Services.Models;

namespace DBConsoleNF.Services
{
    public class SortService : ISortService
    {
        private const string OrderBy = "OrderBy";
        private const string OrderByDescending = "OrderByDescending";
        private const string ThenBy = "ThenBy";
        private const string ThenByDescending = "ThenByDescending";
        private bool _isAlreadySorted;

        public IQueryable<T> Sort<T>(IQueryable<T> query, IEnumerable<Sort> sorts)
        {
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "x");
            foreach (var sort in sorts)
            {
                var entityProperty = type.GetProperty(sort.Property);
                if (entityProperty == null)
                {
                    throw new ArgumentNullException(nameof(entityProperty), "Sort Property not found");
                }
                var propertyAccess = Expression.MakeMemberAccess(parameter, entityProperty);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                var typeArguments = new Type[] { type, entityProperty.PropertyType };
                var methodName = GetSortType(sort.SortOrder);
                var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, query.Expression, Expression.Quote(orderByExp));

                query = query.Provider.CreateQuery<T>(resultExp);
            }

            return query;
        }

        private string GetSortType(string sortOrder)
        {
            if (!_isAlreadySorted)
            {
                _isAlreadySorted = true;
                return sortOrder == SortOperators.Ascendant ? OrderBy : OrderByDescending;
            }

            return sortOrder == SortOperators.Ascendant ? ThenBy : ThenByDescending;
        }
    }
}