using System;
using Parxlab.Entities.Identity;

namespace Parxlab.Entities
{
    public class ParkUsage:BaseEntity
    {
        public DateTime ArrivalTime { get; set; }
        public DateTime? LeaveTime { get; set; }
        public string PlateNo { get; set; }
        public Guid ClientId { get; set; }
        public Guid ParkId { get; set; }
        public virtual User Client { get; set; }
        public virtual Park Park { get; set; }
    }
}
