using System;
using System.Collections.Generic;
using Parxlab.Entities.Enums;

namespace Parxlab.Entities
{
    public class Park:BaseEntity
    {
        public string Title { get; set; }
        public ParkStatus Status { get; set; }
        public Guid SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }
        public virtual ICollection<ParkUsage> ParkUsage { get; set; }
    }
}