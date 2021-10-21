using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Parxlab.Entities.Mapping
{
    public class ParkMapping:BaseEntityTypeConfiguration<Park>
    {
        public override void Configure(EntityTypeBuilder<Park> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Title).IsRequired();
        }
    }
}
