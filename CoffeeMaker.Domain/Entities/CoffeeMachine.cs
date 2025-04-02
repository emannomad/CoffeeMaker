using System.Collections.Generic;
using System.Linq;
using System;

namespace CoffeeMaker.Domain
{
    public class CoffeeMachine
    {
        public int Id { get; set; }

        public List<Tray> Trays { get; set; } = new List<Tray>();

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public List<Beverage> Beverages { get; set; } = new List<Beverage>();

        public void AddTray(string ingredientName, int capacity = 50, int quantity = 0)
        {
            Ingredient ingredient = Ingredients.FirstOrDefault(i => i.Name == ingredientName);
            if (ingredient == null) throw new InvalidOperationException("No such ingredient.");

            Trays.Add(new Tray(ingredient, quantity, capacity));
        }

        public void RefillIngredient(Ingredient ingredient)
        {
            foreach (Tray tray in Trays)
            {
                if (tray.Ingredient == ingredient)
                {
                    tray.Quantity = tray.Capacity;
                }
            }
        }

        public void RefillAllIngredients()
        {
            Trays.ForEach(tray => tray.Quantity = tray.Capacity);
        }

        public void AddIngredient(string name, decimal cost)
        {
            Ingredients.Add(new Ingredient(name, cost));
        }

        // Note: Test driven development would have helped nicely here.
        public Beverage PurchaseBeverage(Beverage beverage)
        {
            // Check stock and then take from tray(s).
            foreach (RecipeIngredient recipeItem in beverage.Recipe)
            {
                // Checking stock...
                Ingredient ingredientInMachine =
                    Ingredients.FirstOrDefault(i => i == recipeItem.Ingredient);

                if (ingredientInMachine == null)
                {
                    throw new InvalidOperationException(
                        "Machine does not contain that ingredient");
                }

                IEnumerable<Tray> traysWithIngredient = Trays
                    .Where(tray => tray.Ingredient == ingredientInMachine);

                int stock = traysWithIngredient.Sum(tray => tray.Quantity);
                if (stock < recipeItem.Quantity)
                {
                    throw new InvalidOperationException(
                        "Not enough ingredients in stock.");
                }

                // Now that we know we have stock, start taking from tray.
                int remainingQuantity = recipeItem.Quantity;
                foreach (Tray tray in traysWithIngredient)
                {
                    if (remainingQuantity <= 0) break;

                    int quantityToTakeFromTray = Math.Min(tray.Quantity, remainingQuantity);
                    tray.Quantity -= quantityToTakeFromTray;
                    remainingQuantity -= quantityToTakeFromTray;
                }
            }

            return beverage;
        }

        public void AddBeverage(string name, IEnumerable<RecipeIngredient> ingredients)
        {
            Beverages.Add(new Beverage(name, ingredients.ToList(), Id));
        }
    }
}
