using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CoffeeMaker.Application.Dtos;
using CoffeeMaker.Domain;

namespace CoffeeMaker.Application.Services
{
    public class InventoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddIngredientToCoffeeMachine(int coffeeMachineId, string ingredientName, decimal ingredientCost)
        {
            CoffeeMachine coffeeMachine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(coffeeMachineId);
            coffeeMachine.AddIngredient(ingredientName, ingredientCost);
            _unitOfWork.Commit();
        }

        public void AddTrayToCoffeeMachine(int coffeeMachineId, string name, int capacity)
        {
            CoffeeMachine coffeeMachine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(coffeeMachineId);
            coffeeMachine.AddTray(name, capacity);
            _unitOfWork.Commit();
        }

        public void RefillAllIngredients(int coffeeMachineId = 1)
        {
            CoffeeMachine coffeeMachine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(coffeeMachineId);
            coffeeMachine.RefillAllIngredients();
            _unitOfWork.Commit();
        }

        public void AddBeverage(int coffeeMachineId, string beveragename, List<RecipeIngredientDto> recipeItemDtos)
        {
            CoffeeMachine coffeeMachine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(coffeeMachineId);
            
            List<RecipeIngredient> recipeItems = new List<RecipeIngredient>();
            foreach (RecipeIngredientDto item in recipeItemDtos)
            {
                recipeItems.Add(new RecipeIngredient
                {
                    Ingredient = coffeeMachine.Ingredients.FirstOrDefault(i => i.Name == item.IngredientName),
                    Quantity = item.Quantity
                });
            }

            coffeeMachine.AddBeverage(beveragename, recipeItems);
            _unitOfWork.Commit();
        }
    }
}
