using System.Data.Entity.ModelConfiguration;
using CoffeeMaker.Domain;

namespace CoffeeMaker.Infrastructure.Data.Configurations
{
    internal class CoffeeMachineConfiguration : EntityTypeConfiguration<CoffeeMachine>
    {
        public CoffeeMachineConfiguration()
        {
            HasMany(machine => machine.Ingredients)
                .WithRequired()
                .Map(ingredient => ingredient.MapKey("CoffeeMachineId"))
                .WillCascadeOnDelete(false);

            HasMany(machine => machine.Trays)
                .WithRequired()
                .Map(tray => tray.MapKey("CoffeeMachineId"));

            //HasMany(machine => machine.Beverages)
            //    .WithRequired()
            //    .Map(beverage => beverage.MapKey("CoffeeMachineId"));
            // Had to refactor due to entity framework.
            HasMany(machine => machine.Beverages)
                .WithRequired()
                .HasForeignKey(beverage => beverage.CoffeeMachineId);

        }
    }
}
