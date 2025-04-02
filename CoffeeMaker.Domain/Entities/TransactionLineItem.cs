using System.Data;

namespace CoffeeMaker.Domain.Entities
{
    public class TransactionLineItem
    {
        public int Id { get; set; }

        public Ingredient Ingredient { get; set; }

        public int IngredientId { get; set; }

        public int TransactionId { get; set; }

        public Transaction Transaction { get; set; }

        public decimal PricePerItem { get; set; }

        public int Quantity { get; set; }
    }
}