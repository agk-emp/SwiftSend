using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftSend.data.Entities;

namespace SwiftSend.infrastructure.Context.Configurations
{
    public class DelivryPersonConfigurations : IEntityTypeConfiguration<DeleiveryPerson>
    {
        public void Configure(EntityTypeBuilder<DeleiveryPerson> builder)
        {
            builder.HasMany(dlv => dlv.OrderDetails)
                .WithOne(ordDtl => ordDtl.DeleiveryPerson)
                .HasForeignKey(ordDtl => ordDtl.DeliveryPersonId);
        }
    }
}
