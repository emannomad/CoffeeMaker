using System.Data.Entity.ModelConfiguration;
using CoffeeMaker.Domain;

namespace CoffeeMaker.Infrastructure.Data.Configurations
{
    internal class IngredientConfiguration : EntityTypeConfiguration<Ingredient>
    {
        public IngredientConfiguration()
        {
        }
    }
}
