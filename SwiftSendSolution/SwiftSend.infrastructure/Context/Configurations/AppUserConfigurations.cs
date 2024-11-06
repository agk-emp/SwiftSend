using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftSend.data.Entities.Identity;

namespace SwiftSend.infrastructure.Context.Configurations
{
    public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(user => user.FirstName).IsRequired()
                .HasMaxLength(200);

            builder.Property(user => user.LastName).IsRequired()
                .HasMaxLength(200);
        }
    }
}
