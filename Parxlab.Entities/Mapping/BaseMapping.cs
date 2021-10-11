using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parxlab.Entities.Mapping
{
    class BaseMapping
    {
        public class BaseMapping : IEntityTypeConfiguration<BaseEntity>
        {
            public void Configure(EntityTypeBuilder<BaseEntity> builder)
            {
                builder.Property(p => p.CreatedDate).HasColumnType("datetime2").HasDefaultValueSql("getdate()");
                builder.Property(p => p.ModifiedDate).HasColumnType("datetime2").HasDefaultValueSql("getdate()");
            }
        }
    }
}
