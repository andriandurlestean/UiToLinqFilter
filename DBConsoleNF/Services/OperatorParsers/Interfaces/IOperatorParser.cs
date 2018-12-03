using System;
using System.Linq.Expressions;
using DBConsoleNF.Services.Models;

namespace DBConsoleNF.Services.OperatorParsers.Interfaces
{
    public interface IOperatorParser
    {
        Type OperatorParserType { get; }

        Expression<Func<T, bool>> Parse<T>(Filter filter) where T : class;
    }

    public interface IOperatorParser<T> : IOperatorParser
    {
    }
}