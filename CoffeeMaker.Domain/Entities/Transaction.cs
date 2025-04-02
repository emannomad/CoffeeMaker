using System.Collections.Generic;
using System.Linq;

namespace CoffeeMaker.Domain.Entities
{
    public class Transaction
    {
        public Transaction(Beverage beverage)
        {
            BeverageId = beverage.Id;
            LineItems = beverage.Recipe.Select(
                recipeIngredient => new TransactionLineItem
                {
                    Ingredient = recipeIngredient.Ingredient,
                    PricePerItem = recipeIngredient.Ingredient.Price,
                    Quantity = recipeIngredient.Quantity,
                    Transaction = this,
                })
                .ToList();
            TotalPrice = LineItems.Sum(item => item.PricePerItem * item.Quantity);
        }

        public int Id { get; set; }

        public Beverage Beverage { get; set; }

        public int BeverageId { get; set; }

        public List<TransactionLineItem> LineItems { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
