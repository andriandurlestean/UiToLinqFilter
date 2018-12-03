using System;
using System.Linq.Expressions;
using DBConsoleNF.Services.Models;
using DBConsoleNF.Services.OperatorParsers.Interfaces;

namespace DBConsoleNF.Services.OperatorParsers
{
    public class StringOperatorParser : BaseOperatorParser, IOperatorParser<string>
    {
        private const string Contains = "Contains";

        public override Type OperatorParserType => typeof(string);

        public StringOperatorParser()
        {
            FilterOperatorParsers[FilterOperators.Contains] = ContainsStringFilterOperatorParser;
            FilterOperatorParsers[FilterOperators.NotContains] = NotContainsStringFilterOperatorParser;
        }

        private Expression ContainsStringFilterOperatorParser(Expression left, Expression right)
        {
            var containsMethod = Expression.Call(left, Contains, null, right);
            return containsMethod;
        }

        private Expression NotContainsStringFilterOperatorParser(Expression left, Expression right)
        {
            var containsMethod = Expression.Call(left, Contains, null, right);
            return Expression.Not(containsMethod);
        }
    }
}