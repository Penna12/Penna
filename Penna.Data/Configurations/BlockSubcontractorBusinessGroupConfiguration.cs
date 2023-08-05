using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class BlockSubcontractorBusinessGroupConfiguration : IEntityTypeConfiguration<BlockSubcontractorBusinessGroup>
    {
        public void Configure(EntityTypeBuilder<BlockSubcontractorBusinessGroup> builder)
        {
            builder.ToTable("BlockSubcontractorBusinessGroups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.BusinessGroupId).IsRequired();

            builder.HasOne(m => m.BlockSubcontractor)
                .WithMany(d => d.BlockSubcontractorBusinessGroups)
                .HasForeignKey(d => d.BlockSubcontractorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BlockSubcontractorBusinessGroups_BlockSubcontractor_BlockSubcontractorId");
        }
    }
}
