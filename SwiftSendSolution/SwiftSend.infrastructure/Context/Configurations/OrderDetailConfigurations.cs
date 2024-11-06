using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftSend.data.Entities;

namespace SwiftSend.infrastructure.Context.Configurations
{
    public class OrderDetailConfigurations : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(ordDtl => new { ordDtl.OrderId, ordDtl.RestaurantId });
            builder.HasOne(ordDtl => ordDtl.Meal)
                .WithMany(meal => meal.OrderDetails)
                .HasForeignKey(ordDtl => new { ordDtl.MealName, ordDtl.RestaurantId });
        }
    }
}
