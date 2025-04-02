using System.Data.Entity;
using CoffeeMaker.Domain;
using CoffeeMaker.Domain.Entities;
using CoffeeMaker.Infrastructure.Data.Configurations;

namespace CoffeeMaker.Infrastructure.Data
{
    public class CoffeeMakerDbContext : DbContext
    {
        // Your context has been configured to use a 'CoffeeMakerDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CoffeeMaker.Infrastructure.Data.CoffeeMakerDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CoffeeMakerDbContext' 
        // connection string in the application configuration file.
        public CoffeeMakerDbContext()
            : base("name=CoffeeMakerDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<CoffeeMachine> CoffeeMachines { get; set; }

        public virtual DbSet<Tray> Trays { get; set; }

        public virtual DbSet<Beverage> Beverages { get; set; }

        public virtual DbSet<RecipeIngredient> RecipeItems { get; set; }

        public virtual DbSet<Ingredient> Ingredients { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<TransactionLineItem> TransactionLineItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BeverageConfiguration());
            modelBuilder.Configurations.Add(new IngredientConfiguration());
            modelBuilder.Configurations.Add(new RecipeItemConfigration());
            modelBuilder.Configurations.Add(new CoffeeMachineConfiguration());
            modelBuilder.Configurations.Add(new TransactionLineItemConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}