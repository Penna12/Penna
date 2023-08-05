using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class UserPositionAuthorityConfiguration : IEntityTypeConfiguration<UserPositionAuthority>
    {
        public void Configure(EntityTypeBuilder<UserPositionAuthority> builder)
        {
            builder.ToTable("UserPositionAuthorities");


            builder.HasKey(e => new { e.UserPositionId, e.AuthorityId })
                    .HasName("PK_UserPositionAuthorities");

            builder.HasIndex(e => e.AuthorityId)
                .HasName("IX_AuthorityId");

            builder.HasIndex(e => e.UserPositionId)
                .HasName("IX_UserPositionId");

            builder.Property(e => e.UserPositionId).HasMaxLength(128);

            builder.Property(e => e.AuthorityId).HasMaxLength(128);

            builder.HasOne(d => d.Authority)
                .WithMany(p => p.UserPositionAuthorities)
                .HasForeignKey(d => d.AuthorityId)
                .HasConstraintName("FK_UserPositionAuthorities_Authority_AuthorityId");

            builder.HasOne(d => d.UserPosition)
                .WithMany(p => p.UserPositionAuthorities)
                .HasForeignKey(d => d.UserPositionId)
                .HasConstraintName("FK_UserPositionAuthorities_UserPosition_UserPositionId");

        }
    }
}
