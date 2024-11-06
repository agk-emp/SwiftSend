using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftSend.data.Entities;

namespace SwiftSend.infrastructure.Context.Configurations
{
    public class RestaurantConfigurations : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(res => res.Name).IsRequired()
                .HasMaxLength(300);

            builder.Property(res => res.Location).IsRequired()
                .HasMaxLength(500);
        }
    }
}
