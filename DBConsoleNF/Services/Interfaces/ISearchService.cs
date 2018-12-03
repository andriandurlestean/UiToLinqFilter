using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DBConsoleNF.Services.Models;

namespace DBConsoleNF.Services.Interfaces
{
    public interface ISearchService
    {
        Expression<Func<T, bool>> Filter<T>(IEnumerable<Filter> filters) where T : class;
    }
}