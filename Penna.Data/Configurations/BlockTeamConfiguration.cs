using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class BlockTeamConfiguration : IEntityTypeConfiguration<BlockTeam>
    {
        public void Configure(EntityTypeBuilder<BlockTeam> builder)
        {
            builder.ToTable("BlockTeams");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.UserId).IsRequired().HasMaxLength(450);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(30);
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(450);
            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedBy).HasMaxLength(450);
            builder.Property(x => x.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(m => m.Block)
                .WithMany(d => d.BlockTeams)
                .HasForeignKey(d => d.BlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlockTeams_Block_BlockId");

            builder.HasOne(m => m.UserPosition)
                .WithMany(d => d.BlockTeams)
                .HasForeignKey(d => d.UserPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlockTeams_UserPosition_UserPositionId");

            builder.HasOne(m => m.User)
                .WithMany(d => d.BlockTeams)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlockTeams_User_UserId");
        }
    }
}
