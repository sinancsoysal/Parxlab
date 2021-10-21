using System;
using Parxlab.Entities.Identity;

namespace Parxlab.Entities
{
    public class Reserved:BaseEntity
    {
        public DateTime ArrivalTime { get; set; }  
        public DateTime? LeaveTime { get; set; }  
        public string PlateNo { get; set; }
        public Guid UserId { get; set; }
        public virtual User User{ get; set; }
    }
}
