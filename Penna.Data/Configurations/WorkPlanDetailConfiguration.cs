using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penna.Entities.Models;

namespace Penna.Data.Configurations
{
    public class WorkPlanDetailConfiguration : IEntityTypeConfiguration<WorkPlanDetail>
    {
        public void Configure(EntityTypeBuilder<WorkPlanDetail> builder)
        {
            builder.ToTable("WorkPlanDetail");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x => x.PlanDate).IsRequired().HasColumnType("datetime");

            builder.HasOne(m => m.Unit)
                .WithMany(d => d.WorkPlanDetails)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkPlanDetails_Unit_UnitId");

            builder.HasOne(m => m.WorkPlan)
                .WithMany(d => d.WorkPlanDetails)
                .HasForeignKey(d => d.WorkPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkPlanDetails_WorkPlan_WorkPlanId");

            builder.HasOne<WorkPlanDetail>()
                .WithOne(d => d.ParentWorkPlanDetail)
                .HasForeignKey<WorkPlanDetail>(c => c.ParentWorkPlanDetailId);
        }
    }
}
