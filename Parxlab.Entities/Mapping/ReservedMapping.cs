using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Parxlab.Entities.Mapping
{
    public class ReservedMapping:BaseEntityTypeConfiguration<Reserved>
    {
        public override void Configure(EntityTypeBuilder<Reserved> builder)
        {
            base.Configure(builder);
        }
    }
}
