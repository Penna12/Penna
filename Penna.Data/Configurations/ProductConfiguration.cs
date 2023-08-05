using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Core.Utilities.Enums;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(75);
            builder.Property(x => x.Brand).HasMaxLength(50);
            builder.Property(x => x.UnitId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Quantity).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Dimensions).HasMaxLength(50);
            builder.Property(x => x.Thickness).HasMaxLength(50);
            builder.Property(x => x.Species).HasMaxLength(50);
            builder.Property(x => x.BusinessGroupId).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(StatusEnum.Aktif);

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Project)
                .WithMany(d => d.Products)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Project_ProjectId");

            builder.HasOne(m => m.Unit)
                .WithMany(d => d.Products)
                .HasForeignKey(m => m.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Units_UnitId");
        }
    }
}
