using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
    {
        public void Configure(EntityTypeBuilder<Fixture> builder)
        {
            builder.ToTable("Fixture");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(100);

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Project)
                .WithMany(d => d.Fixtures)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fixtures_Project_ProjectId");
        }
    }
}
