using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftSend.data.Entities;

namespace SwiftSend.infrastructure.Context.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(ord => ord.OrderDetails)
                .WithOne(ordDtl => ordDtl.Order)
                .HasForeignKey(ordDtl => ordDtl.OrderId);
        }
    }
}
