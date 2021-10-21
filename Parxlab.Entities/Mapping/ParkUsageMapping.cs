using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Parxlab.Entities.Mapping
{
    public class ParkUsageMapping:BaseEntityTypeConfiguration<ParkUsage>
    {
        public override void Configure(EntityTypeBuilder<ParkUsage> builder)
        {
            base.Configure(builder);
            builder.HasOne(d => d.Client)
                .WithMany(p => p.ParkUsage)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_ParkUsage_Client");
            builder.HasOne(d => d.Park)
                .WithMany(p => p.ParkUsage)
                .HasForeignKey(d => d.ParkId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_ParkUsage_Park");
        }
    }
}
