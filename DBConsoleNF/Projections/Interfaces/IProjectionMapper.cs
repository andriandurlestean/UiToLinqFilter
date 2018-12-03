using System;
using System.Linq.Expressions;

namespace DBConsoleNF.Projections.Interfaces
{
    public interface IProjectionMapper<TEntity, TModel>
    {
        Expression<Func<TEntity, TModel>> Map();
    }
}