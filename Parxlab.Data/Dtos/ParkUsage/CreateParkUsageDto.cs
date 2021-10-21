using System;

namespace Parxlab.Data.Dtos.ParkUsage
{
    public record CreateParkUsageDto
    {
        public DateTime ArrivalTime { get; set; }
        public DateTime? LeaveTime { get; set; }
        public string PlateNo { get; set; }
        public Guid ClientId { get; set; }
        public Guid ParkId { get; set; }
    }
}
