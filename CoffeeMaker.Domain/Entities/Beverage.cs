using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMaker.Domain
{
    public class Beverage
    {
        /// <summary>
        /// Had to include because of Entity Framework.
        /// </summary>
        public Beverage()
        {
        }

        public Beverage(string name, List<RecipeIngredient> recipe, int coffeeMachineId)
        {
            Name = name;
            Recipe = recipe;
            CoffeeMachineId = coffeeMachineId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Had to include because of Entity Framework.
        /// </summary>
        public int CoffeeMachineId { get; set; }

        public List<RecipeIngredient> Recipe { get; set; }

        public decimal CalculatePrice()
        {
            return Recipe.Sum(recipeItem => recipeItem.Quantity * recipeItem.Ingredient.Price);
        }

        public string GetPriceLabel()
        {
            return $"{Name} ${CalculatePrice():F2}";
        }

        public Beverage CustomizeBeverage(IEnumerable<RecipeIngredient> customIngredients)
        {
            var recipe = new List<RecipeIngredient>(Recipe);

            // Adjust recipe to custom selection.
            foreach (RecipeIngredient ingredient in
                customIngredients ?? Enumerable.Empty<RecipeIngredient>())
            {
                recipe.Remove(ingredient);

                if (ingredient.Quantity < 0) throw new InvalidOperationException("Recipe item quantity cannot be less than 0");

                // No need to add a recipe item if there is no quantity.
                if (ingredient.Quantity > 0) 
                { 
                    recipe.Add(ingredient); 
                } 
            }

            return new Beverage(Name, recipe, CoffeeMachineId) { Id = Id };
        }
    }
}