﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parxlab.Entities.Mapping
{
    class SensorMapping : BaseEntityTypeConfiguration<Sensor>
    {
        public override void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.Property(prop => prop.WPSDId).IsRequired();
            builder.Property(prop => prop.WDCId).IsRequired();
            builder.Property(prop => prop.ParkId).IsRequired();
            builder.Property(prop => prop.RSSI).IsRequired();
            builder.Property(prop => prop.CarState).IsRequired();
            builder.Property(prop => prop.Voltage).IsRequired();
            builder.Property(prop => prop.HardVer).IsRequired();
            builder.Property(prop => prop.SoftVer).IsRequired();
            builder.Property(prop => prop.HBPeriod).IsRequired();
            base.Configure(builder);
        }
    }
}
