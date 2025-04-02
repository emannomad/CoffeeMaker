namespace CoffeeMaker.Domain
{
    public class RecipeIngredient
    {
        /// <summary>
        /// Had to introduce this to give a primary key to entity framework. 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This was introduced to comply with Entity Framework.
        /// </summary>
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            RecipeIngredient otherRecipeItem = obj as RecipeIngredient;
            if (otherRecipeItem != null)
            {
                return otherRecipeItem.Ingredient == Ingredient;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Ingredient.GetHashCode();
        }

        public string GetQuantityLabel()
        {
            return $"{Ingredient.Name} - {Quantity} qty";
        }
    }
}