using System.Collections.Generic;

namespace CoffeeMaker.Domain.Repositories
{
    public interface ICoffeeMachineRepository : IRepository<CoffeeMachine>
    {
        CoffeeMachine GetFullCoffeeMachineById(int id);
    }
}
