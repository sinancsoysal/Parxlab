using System;

namespace Parxlab.Data.Dtos.Reserved
{
   public class CreateReservedDto
    {
        public DateTime ArrivalTime { get; set; }
        public DateTime? LeaveTime { get; set; }
        public string PlateNo { get; set; }
    }
}
