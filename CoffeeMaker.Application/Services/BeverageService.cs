﻿using System.Collections.Generic;
using System.Linq;
using CoffeeMaker.Application.Dtos;
using CoffeeMaker.Domain;
using CoffeeMaker.Domain.Entities;

namespace CoffeeMaker.Application.Services
{
    public class BeverageService
    {
        public readonly IUnitOfWork _unitOfWork;
        public BeverageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Beverage SelectBeverage(int coffeeMachineId, string beverageName)
        {
            CoffeeMachine coffeeMachine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(coffeeMachineId);
            Beverage selectedBeverage = coffeeMachine.Beverages.FirstOrDefault(b => b.Name == beverageName);
            return selectedBeverage;
        }

        public Beverage CustomizeBeverage(int coffeeMachineId, Beverage beverage, List<RecipeIngredientDto> ingredients)
        {
            CoffeeMachine coffeeMachine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(coffeeMachineId);

            var ingredientDictionary = coffeeMachine.Ingredients.ToDictionary(i => i.Name);

            List<RecipeIngredient> recipeItems = new List<RecipeIngredient>();
            foreach (RecipeIngredientDto item in ingredients)
            {
                if (ingredientDictionary.TryGetValue(item.IngredientName, out Ingredient ingredient))
                {
                    recipeItems.Add(new RecipeIngredient
                    {
                        Ingredient = ingredient,
                        Quantity = item.Quantity
                    });
                }
            }

            return beverage.CustomizeBeverage(recipeItems);
        }

        public void PurchaseBeverage(int coffeeMachineId, Beverage selectedBeverage)
        {
            CoffeeMachine coffeeMachine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(coffeeMachineId);
            coffeeMachine.PurchaseBeverage(selectedBeverage);

            _unitOfWork.TransactionRepository.Add(new Transaction(selectedBeverage));

            _unitOfWork.Commit();
        }
    }
}
