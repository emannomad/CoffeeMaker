using System.Data.Entity.ModelConfiguration;
using CoffeeMaker.Domain;

namespace CoffeeMaker.Infrastructure.Data.Configurations
{
    internal class BeverageConfiguration : EntityTypeConfiguration<Beverage>
    {
        public BeverageConfiguration()
        {
            HasMany(beverage => beverage.Recipe)
                .WithRequired()
                .Map(recipeItem => recipeItem.MapKey("BeverageId"));
        }
    }
}
