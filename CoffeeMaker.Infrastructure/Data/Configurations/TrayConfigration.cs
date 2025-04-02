using System.Data.Entity.ModelConfiguration;
using CoffeeMaker.Domain;

namespace CoffeeMaker.Infrastructure.Data.Configurations
{
    public class TrayConfigration : EntityTypeConfiguration<Tray>
    {
        public TrayConfigration()
        {
            HasRequired(tray => tray.Ingredient)
                .WithMany()
                .HasForeignKey(tray => tray.IngredientId);
        }
    }
}
