using CoffeeMaker.Domain;

namespace CoffeeMaker.Application.Dtos
{
    public class RecipeIngredientDto
    {
        public string IngredientName { get; set; }

        public int Quantity { get; set; }
    }
}
