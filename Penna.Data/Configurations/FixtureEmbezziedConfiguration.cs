using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class FixtureEmbezziedConfiguration : IEntityTypeConfiguration<FixtureEmbezzled>
    {
        public void Configure(EntityTypeBuilder<FixtureEmbezzled> builder)
        {
            builder.ToTable("FixtureEmbezzled");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(100);
            builder.Property(x => x.AppUserId).IsRequired().HasMaxLength(450);
            builder.Property(x => x.EmbezzledDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.ReturnDate).HasColumnType("datetime");

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Fixture)
                .WithMany(d => d.FixtureEmbezzleds)
                .HasForeignKey(m => m.FixtureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FixtureEmbezzleds_Fixture_FixtureId");

            builder.HasOne(m => m.AppUser)
                .WithMany(d => d.FixtureEmbezzleds)
                .HasForeignKey(m => m.AppUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FixtureEmbezzleds_AppUser_AppUserId");
        }
    }
}
