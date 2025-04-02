namespace CoffeeMaker.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionLineItems", "PricePerItem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TransactionLineItems", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.TransactionLineItems", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionLineItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.TransactionLineItems", "Quantity");
            DropColumn("dbo.TransactionLineItems", "PricePerItem");
        }
    }
}
