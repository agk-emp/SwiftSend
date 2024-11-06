using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftSend.data.Entities;

namespace SwiftSend.infrastructure.Context.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasMany(cus => cus.Orders)
            .WithOne(ord => ord.Customer)
            .HasForeignKey(ord => ord.CustomerId);
        }
    }
}
