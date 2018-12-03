using System;
using System.Linq.Expressions;
using DBConsoleNF.Services.Models;
using DBConsoleNF.Services.OperatorParsers.Interfaces;

namespace DBConsoleNF.Services.OperatorParsers
{
    public class IntegerOperatorParser : BaseOperatorParser, IOperatorParser<int>
    {
        public override Type OperatorParserType => typeof(int);

        public IntegerOperatorParser()
        {
            FilterOperatorParsers[FilterOperators.Greater] = Expression.GreaterThan;
            FilterOperatorParsers[FilterOperators.Less] = Expression.LessThan;
            FilterOperatorParsers[FilterOperators.GreaterOrEqual] = Expression.GreaterThanOrEqual;
            FilterOperatorParsers[FilterOperators.LessOrEqual] = Expression.LessThanOrEqual;
        }
    }
}