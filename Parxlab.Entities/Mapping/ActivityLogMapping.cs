
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Parxlab.Entities.Mapping
{
   public class ActivityLogMapping:BaseEntityTypeConfiguration<ActivityLog>
    {
        public override void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            base.Configure(builder);

            builder.HasOne(d => d.User)
                .WithMany(p => p.ActivityLog)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_ActivityLog_User");
        }
    }
}
