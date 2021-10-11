﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Parxlab.Entities.Mapping
{
    public class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.Property(p => p.CreatedDate).HasColumnType("datetime2").HasDefaultValueSql("(getDate())");
            builder.Property(p => p.ModifiedDate).HasColumnType("datetime2").HasDefaultValueSql("(getDate())");
        }
    }
}
