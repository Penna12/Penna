using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //builder.ToTable("AspNetUsers");
            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.FullName).IsRequired().HasMaxLength(75);
            builder.Property(x => x.Address).HasMaxLength(200);
            builder.Property(x => x.CountryDialCode).HasMaxLength(5);
            builder.Property(x => x.PictureUrl).HasMaxLength(200);
            builder.Property(x => x.PictureRealName).HasMaxLength(200);
            builder.Property(x => x.PictureContentType).HasMaxLength(100);
        }
    }
}
