using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parxlab.Entities
{
    public class Sensor : BaseEntity
    {
        public string WPSDId { get; set; }
        public string WDCId { get; set; }
        public int ParkId { get; set; }
        public string RSSI { get; set; }
        public byte CarState { get; set; }
        public string Voltage { get; set; }
        public string HardVer { get; set; }
        public string SoftVer { get; set; }
        public string HBPeriod { get; set; }
    }
}
