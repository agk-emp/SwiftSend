using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftSend.data.Entities;

namespace SwiftSend.infrastructure.Context.Configurations
{
    public class MealConfigurations : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasKey(meal => new { meal.RestaurantId, meal.MealName });

            builder.HasOne(meal => meal.Restaurant)
                .WithMany(res => res.Meals).
                HasForeignKey(meal => meal.RestaurantId);

            builder.Property(meal => meal.MealName).IsRequired()
                .HasMaxLength(300);
        }
    }
}
