using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DBConsoleNF.Services.Interfaces;
using DBConsoleNF.Services.Models;
using DBConsoleNF.Services.OperatorParsers;
using DBConsoleNF.Services.OperatorParsers.Interfaces;
using LinqKit;

namespace DBConsoleNF.Services
{
    public class SearchService : ISearchService
    {
        private readonly IEnumerable<IOperatorParser> _operatorParsers;

        public SearchService(IEnumerable<IOperatorParser> operatorOperatorParsers)
        {
            _operatorParsers = operatorOperatorParsers;
        }

        public SearchService()
        {
            _operatorParsers = new List<IOperatorParser>
            {
                new StringOperatorParser(),
                new BooleanOperatorParser(),
                new IntegerOperatorParser(),
                new DecimalOperatorParser(),
                new DateTimeOperatorParser()
            };
        }

        public Expression<Func<T, bool>> Filter<T>(IEnumerable<Filter> filters) where T : class
        {
            var whereFilter = PredicateBuilder.New<T>();

            if (!filters.Any()) return whereFilter;

            var modelType = typeof(T);
            var properties = modelType.GetProperties();

            foreach (var filter in filters)
            {
                var entityProperty = properties.FirstOrDefault(x => x.Name == filter.Property);
                if (entityProperty == null) throw new ArgumentNullException(nameof(entityProperty), "Filter Property not found");

                var parser = _operatorParsers.FirstOrDefault(x => x.OperatorParserType == entityProperty.PropertyType);
                if (parser == null) throw new ArgumentNullException(nameof(parser), "Parser not found");

                whereFilter = whereFilter.And(parser.Parse<T>(filter));
            }

            return whereFilter;
        }
    }
}