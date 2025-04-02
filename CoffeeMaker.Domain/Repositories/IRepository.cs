using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoffeeMaker.Domain.Repositories
{
    public interface IRepository<T>
    {
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        List<T> FindAll();

        T Add(T entity);
    }
}
