using System.Data.Entity.ModelConfiguration;
using CoffeeMaker.Domain;

namespace CoffeeMaker.Infrastructure.Data.Configurations
{
    public class RecipeItemConfigration : EntityTypeConfiguration<RecipeIngredient>
    {
        public RecipeItemConfigration()
        {
            HasRequired(recipeItem => recipeItem.Ingredient)
                .WithMany()
                .HasForeignKey(recipeItem => recipeItem.IngredientId);
        }
    }
}
