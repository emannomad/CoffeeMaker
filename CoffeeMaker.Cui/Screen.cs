using System.Collections.Generic;
using System;
using CoffeeMaker.Application.Services;
using CoffeeMaker.Domain;
using CoffeeMaker.Application;
using System.Linq;
using CoffeeMaker.Application.Dtos;

namespace CoffeeMaker.Cui
{
    public class Screen
    {
        private readonly InventoryService _inventoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeverageService _beverageService;

        public Screen(InventoryService inventoryService, IUnitOfWork unitOfWork, BeverageService beverageService)
        {
            _inventoryService = inventoryService;
            _unitOfWork = unitOfWork;
            _beverageService = beverageService;
        }


        public void Run()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("1. Add Ingredient");
            Console.WriteLine("2. Add Tray");
            Console.WriteLine("3. Refill Ingredients");
            Console.WriteLine("4. List Ingredients");
            Console.WriteLine("5. Add Beverage");
            Console.WriteLine("6. List Beverages");
            Console.WriteLine("7. Check Inventory");
            Console.WriteLine("8. Purchase beverage");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1": // Add Ingredient
                    Console.WriteLine("Enter name, then cost.");
                    string name = Console.ReadLine();
                    decimal cost = decimal.Parse(Console.ReadLine());
                    _inventoryService.AddIngredientToCoffeeMachine(1, name, cost);
                    break;
                case "2": // Add Tray
                    Console.WriteLine("Enter name, then capacity.");
                    _inventoryService.AddTrayToCoffeeMachine(1, Console.ReadLine(), int.Parse(Console.ReadLine()));
                    break; 
                case "3": // Refill Ingredients
                    _inventoryService.RefillAllIngredients();
                    break;
                case "4": // List Ingredients
                    {
                        CoffeeMachine machine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(1);
                        foreach (var ingredient in machine.Ingredients)
                        {
                            Console.WriteLine(ingredient.GetCostLabel());
                        }
                    }
                    break;
                case "5": // Add Beverage
                    {
                        var items = new List<RecipeIngredientDto>();
                        Console.WriteLine("Beverage name:");
                        string beverageName = Console.ReadLine();
                        Console.WriteLine("Enter ingredient name, then quantity. Once recipe finished, type exit");
                        while (true)
                        {
                            string recipeItem = Console.ReadLine();
                            if (recipeItem == "exit") break;
                            int quantity = int.Parse(Console.ReadLine());
                            items.Add(new RecipeIngredientDto
                            {
                                IngredientName = recipeItem,
                                Quantity = quantity
                            });
                        }

                        _inventoryService.AddBeverage(1, beverageName, items);
                    }

                    break;
                case "6": // List Beverages
                    {
                        CoffeeMachine machine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(1);
                        foreach (Beverage beverage in machine.Beverages)
                        {
                            Console.WriteLine(beverage.GetPriceLabel());
                        }
                        break;
                    }
                case "7": // Check Inventory
                    {
                        CoffeeMachine machine = _unitOfWork.CoffeeMachineRepository.GetFullCoffeeMachineById(1);
                        foreach (Tray tray in machine.Trays)
                        {
                            Console.WriteLine(tray.GetStockLabel());
                        }
                        break;
                    }
                case "8": // Purchase beverage
                    Console.WriteLine("Select beverage by name");
                    string bevName = Console.ReadLine();
                    Beverage selectedBeverage = _beverageService.SelectBeverage(1, bevName);
                    Console.WriteLine("Beverage name: " + selectedBeverage.Name);

                    foreach (RecipeIngredient ingredient in selectedBeverage.Recipe)
                    {
                        ingredient.GetQuantityLabel();
                    }

                    Console.WriteLine("Customize selectedBeverage? (y/n)");

                    while (true)
                    {
                        string customizeInput = Console.ReadLine();
                        if (customizeInput == "y")
                        {
                            var items = new List<RecipeIngredientDto>();
                            Console.WriteLine("Enter ingredient name, then quantity. Once recipe finished, type exit");
                            while (true)
                            {
                                string recipeItem = Console.ReadLine();
                                if (recipeItem == "exit") break;
                                int quantity = int.Parse(Console.ReadLine());
                                items.Add(new RecipeIngredientDto
                                {
                                    IngredientName = recipeItem,
                                    Quantity = quantity
                                });
                            }

                            selectedBeverage = _beverageService.CustomizeBeverage(1, selectedBeverage, items);
                            Console.WriteLine("Would you like to customize more? (y/n)");
                        }
                        else break;
                    }

                    _beverageService.PurchaseBeverage(1, selectedBeverage);
                    Console.WriteLine("Purchase complete.");
                    break;
                default:
                    break;
            }
        }
    }
}
