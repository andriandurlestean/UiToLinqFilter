using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DBConsoleNF.Services.Models;

namespace DBConsoleNF.Services.OperatorParsers.Interfaces
{
    public abstract class BaseOperatorParser : IOperatorParser
    {
        protected readonly IDictionary<string, Func<Expression, Expression, Expression>> FilterOperatorParsers;

        public abstract Type OperatorParserType { get; }

        protected BaseOperatorParser()
        {
            FilterOperatorParsers = new Dictionary<string, Func<Expression, Expression, Expression>>
            {
                {FilterOperators.Equal, Expression.Equal},
                {FilterOperators.NotEqual, Expression.NotEqual }
            };
        }

        public Expression<Func<T, bool>> Parse<T>(Filter filter) where T : class
        {
            var type = typeof(T);
            var prop = type.GetProperty(filter.Property);
            if (prop == null)
            {
                throw new ArgumentNullException(nameof(prop), $"Property not found from filter {filter.Property}");
            }

            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var left = Expression.Property(parameterExpression, filter.Property);
            var right = ConvertValueToConstantExpression(filter.Value, prop.PropertyType);
            var expressionFilter = ApplyOperator(filter.Operator, left, right);
            Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(expressionFilter, parameterExpression);
            return expression;
        }

        protected virtual Expression ConvertValueToConstantExpression(string value, Type propertyType)
        {
            var val = Convert.ChangeType(value, propertyType);

            return Expression.Constant(val);
        }

        protected virtual Expression ApplyOperator(string filterOperator, MemberExpression left, Expression right)
        {
            var filterExist = FilterOperatorParsers.ContainsKey(filterOperator);
            if (filterExist)
            {
                return FilterOperatorParsers[filterOperator](left, right);
            }

            throw new ArgumentNullException(nameof(filterOperator), $"No case found for such operator {filterOperator}");
        }
    }
}