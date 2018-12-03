using System.Collections.Generic;
using System.Linq;
using DBConsoleNF.Services.Models;

namespace DBConsoleNF.Services.Interfaces
{
    public interface ISortService
    {
        IQueryable<T> Sort<T>(IQueryable<T> query, IEnumerable<Sort> sorts);
    }
}