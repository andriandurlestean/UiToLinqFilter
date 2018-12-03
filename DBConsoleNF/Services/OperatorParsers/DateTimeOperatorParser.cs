using System;
using System.Linq.Expressions;
using DBConsoleNF.Services.Models;
using DBConsoleNF.Services.OperatorParsers.Interfaces;

namespace DBConsoleNF.Services.OperatorParsers
{
    public class DateTimeOperatorParser : BaseOperatorParser, IOperatorParser<DateTime>
    {
        public override Type OperatorParserType => typeof(DateTime);

        public DateTimeOperatorParser()
        {
            FilterOperatorParsers[FilterOperators.Greater] = Expression.GreaterThan;
            FilterOperatorParsers[FilterOperators.Less] = Expression.LessThan;
            FilterOperatorParsers[FilterOperators.GreaterOrEqual] = Expression.GreaterThanOrEqual;
            FilterOperatorParsers[FilterOperators.LessOrEqual] = Expression.LessThanOrEqual;
        }
    }
}