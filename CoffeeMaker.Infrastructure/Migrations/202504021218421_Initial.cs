namespace CoffeeMaker.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beverages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CoffeeMachineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoffeeMachines", t => t.CoffeeMachineId, cascadeDelete: true)
                .Index(t => t.CoffeeMachineId);
            
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        BeverageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Beverages", t => t.BeverageId, cascadeDelete: true)
                .Index(t => t.IngredientId)
                .Index(t => t.BeverageId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CoffeeMachineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoffeeMachines", t => t.CoffeeMachineId)
                .Index(t => t.CoffeeMachineId);
            
            CreateTable(
                "dbo.CoffeeMachines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        CoffeeMachineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.CoffeeMachines", t => t.CoffeeMachineId, cascadeDelete: true)
                .Index(t => t.IngredientId)
                .Index(t => t.CoffeeMachineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trays", "CoffeeMachineId", "dbo.CoffeeMachines");
            DropForeignKey("dbo.Trays", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.Ingredients", "CoffeeMachineId", "dbo.CoffeeMachines");
            DropForeignKey("dbo.Beverages", "CoffeeMachineId", "dbo.CoffeeMachines");
            DropForeignKey("dbo.RecipeIngredients", "BeverageId", "dbo.Beverages");
            DropForeignKey("dbo.RecipeIngredients", "IngredientId", "dbo.Ingredients");
            DropIndex("dbo.Trays", new[] { "CoffeeMachineId" });
            DropIndex("dbo.Trays", new[] { "IngredientId" });
            DropIndex("dbo.Ingredients", new[] { "CoffeeMachineId" });
            DropIndex("dbo.RecipeIngredients", new[] { "BeverageId" });
            DropIndex("dbo.RecipeIngredients", new[] { "IngredientId" });
            DropIndex("dbo.Beverages", new[] { "CoffeeMachineId" });
            DropTable("dbo.Trays");
            DropTable("dbo.CoffeeMachines");
            DropTable("dbo.Ingredients");
            DropTable("dbo.RecipeIngredients");
            DropTable("dbo.Beverages");
        }
    }
}
