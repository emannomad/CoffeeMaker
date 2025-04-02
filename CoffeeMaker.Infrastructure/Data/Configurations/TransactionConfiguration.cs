using System.Data.Entity.ModelConfiguration;
using CoffeeMaker.Domain.Entities;

namespace CoffeeMaker.Infrastructure.Data.Configurations
{
    public class TransactionLineItemConfiguration : EntityTypeConfiguration<TransactionLineItem>
    {
        public TransactionLineItemConfiguration()
        {
            HasRequired(t => t.Ingredient)
                .WithMany()
                .HasForeignKey(t => t.IngredientId);
        }
    }
}
