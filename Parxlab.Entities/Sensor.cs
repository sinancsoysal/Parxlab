using Parxlab.Entities.Enums;

namespace Parxlab.Entities
{
    public class Sensor : BaseEntity
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public SensorStatus Status { get; set; }
        public string WPSDId { get; set; }
        public string WDCId { get; set; }
        public int ParkId { get; set; }
        public string RSSI { get; set; }
        public byte CarState { get; set; }
        public string Voltage { get; set; }
        public string HardVer { get; set; }
        public string SoftVer { get; set; }
        public string HBPeriod { get; set; }
        public virtual Park Park { get; set; }
    }
}
