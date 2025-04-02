using System;
using System.Collections.Generic;

namespace CoffeeMaker.Domain
{
    public class Ingredient
    {
        /// <summary>
        /// Had to include because of Entity Framework.
        /// </summary>
        public Ingredient()
        {
        }

        public Ingredient(string name, decimal cost)
        {
            Name = name;
            Cost = cost;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public decimal Price => Cost * 1.25m;

        public override bool Equals(object obj)
        {
            Ingredient otherIngredient = obj as Ingredient;
            if (otherIngredient != null)
            {
                return otherIngredient.Id == Id;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public string GetCostLabel()
        {
            return $"{Name} ${Cost:F2}";
        }
    }
}