using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class BlockSubcontractorConfiguration : IEntityTypeConfiguration<BlockSubcontractor>
    {
        public void Configure(EntityTypeBuilder<BlockSubcontractor> builder)
        {
            builder.ToTable("BlockSubcontractors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CompanyRepresentative).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(30);
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Block)
                .WithMany(d => d.BlockSubcontractors)
                .HasForeignKey(d => d.BlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlockSubcontractor_Block_BlockId");

            builder.HasOne(m => m.CurrentAccount)
                .WithMany(d => d.BlockSubcontractors)
                .HasForeignKey(d => d.CurrentAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlockSubcontractors_CurrentAccount_CurrentAccountId");
        }
    }
}
