using CoffeeMaker.Domain;
using CoffeeMaker.Domain.Entities;
using CoffeeMaker.Domain.Repositories;

namespace CoffeeMaker.Application
{
    public interface IUnitOfWork
    {
        ICoffeeMachineRepository CoffeeMachineRepository { get; }

        IRepository<Transaction> TransactionRepository { get; }

        void Commit();
    }
}
