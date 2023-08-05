using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class WorkPlanConfiguration : IEntityTypeConfiguration<WorkPlan>
    {
        public void Configure(EntityTypeBuilder<WorkPlan> builder)
        {
            builder.ToTable("WorkPlan");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.WorkName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.StartingDate).IsRequired().HasColumnType("datetime");

            builder.HasOne(m => m.CurrentAccount)
                .WithMany(d => d.WorkPlans)
                .HasForeignKey(d => d.ContractorCurrentAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkPlans_CurrentAccount_ContractorCurrentAccountId");

            builder.HasOne(m => m.Unit)
                .WithMany(d => d.WorkPlans)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkPlans_Unit_UnitId");
        }
    }
}
