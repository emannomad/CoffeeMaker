using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CoffeeMaker.Domain.Repositories;

namespace CoffeeMaker.Infrastructure.Data.Repositories
{
    public class EFGenericRepository<T> : IRepository<T>
        where T : class
    {
        private readonly CoffeeMakerDbContext _dbContext;

        public EFGenericRepository(CoffeeMakerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<T> DbSet => _dbContext.Set<T>();

        public T Add(T entity)
        {
            return DbSet.Add(entity);
        }

        public List<T> FindAll()
        {
            return DbSet.ToList();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }
    }
}
