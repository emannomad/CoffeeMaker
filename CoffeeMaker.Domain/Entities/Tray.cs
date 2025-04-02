namespace CoffeeMaker.Domain
{
    public class Tray
    {
        /// <summary>
        /// Had to include because of Entity Framework.
        /// </summary>
        public Tray()
        {
        }
            
        public Tray(Ingredient ingredient, int quantity, int capacity)
        {
            Ingredient = ingredient;
            Quantity = quantity;
            Capacity = capacity;
        }

        /// <summary>
        /// This was introduced to comply with Entity Framework.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This was introduced to comply with Entity Framework.
        /// </summary>
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }

        public int Capacity { get; set; }

        public string GetStockLabel()
        {
            return $"{Ingredient.Name} {Quantity}/{Capacity}";
        }
    }
}