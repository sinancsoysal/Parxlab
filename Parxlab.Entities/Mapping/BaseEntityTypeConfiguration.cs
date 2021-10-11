using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parxlab.Entities.Mapping
{
    class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.Property(p => p.CreatedDate).HasColumnType("datetime2").HasDefaultValueSql("(getDate())");
            builder.Property(p => p.ModifiedDate).HasColumnType("datetime2").HasDefaultValueSql("(getDate())");
        }
    }
}
