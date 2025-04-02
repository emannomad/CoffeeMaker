using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CoffeeMaker.Domain;
using CoffeeMaker.Domain.Repositories;

namespace CoffeeMaker.Infrastructure.Data.Repositories
{
    public class EFCoffeeMachineRepository : EFGenericRepository<CoffeeMachine>, ICoffeeMachineRepository
    {
        public EFCoffeeMachineRepository(CoffeeMakerDbContext dbContext) : base(dbContext)
        {
        }

        public CoffeeMachine GetFullCoffeeMachineById(int id)
        {
            return DbSet.Include(machine => machine.Beverages)
                .Include(machine => machine.Ingredients)
                .Include(machine => machine.Beverages.Select(beverage => beverage.Recipe))
                .Include(machine => machine.Beverages.Select(beverage => beverage.Recipe.Select(recipe => recipe.Ingredient)))
                .Include(machine => machine.Trays)
                .Include(machine => machine.Trays.Select(tray => tray.Ingredient))
                .FirstOrDefault(machine => machine.Id == id);
        }
    }
}
