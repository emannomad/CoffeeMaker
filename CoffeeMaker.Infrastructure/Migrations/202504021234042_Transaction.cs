namespace CoffeeMaker.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionLineItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Transactions", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.IngredientId)
                .Index(t => t.TransactionId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BeverageId = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Beverages", t => t.BeverageId, cascadeDelete: true)
                .Index(t => t.BeverageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionLineItems", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "BeverageId", "dbo.Beverages");
            DropForeignKey("dbo.TransactionLineItems", "IngredientId", "dbo.Ingredients");
            DropIndex("dbo.Transactions", new[] { "BeverageId" });
            DropIndex("dbo.TransactionLineItems", new[] { "TransactionId" });
            DropIndex("dbo.TransactionLineItems", new[] { "IngredientId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.TransactionLineItems");
        }
    }
}
