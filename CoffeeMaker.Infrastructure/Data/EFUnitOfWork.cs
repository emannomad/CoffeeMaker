using CoffeeMaker.Application;
using CoffeeMaker.Domain;
using CoffeeMaker.Domain.Entities;
using CoffeeMaker.Domain.Repositories;
using CoffeeMaker.Infrastructure.Data.Repositories;

namespace CoffeeMaker.Infrastructure.Data
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly CoffeeMakerDbContext _dbContext;

        private ICoffeeMachineRepository _coffeeMachineRepository;

        private IRepository<Transaction> _transactionRepository;

        public EFUnitOfWork(CoffeeMakerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICoffeeMachineRepository CoffeeMachineRepository
        {
            get
            {
                if (_coffeeMachineRepository == null)
                {
                    _coffeeMachineRepository = new EFCoffeeMachineRepository(_dbContext);
                }

                return _coffeeMachineRepository;
            }
        }

        public IRepository<Transaction> TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                {
                    _transactionRepository = new EFGenericRepository<Transaction>(_dbContext);
                }

                return _transactionRepository;
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
