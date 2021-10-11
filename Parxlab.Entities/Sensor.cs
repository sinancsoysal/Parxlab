using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parxlab.Entities
{
    class Sensor : BaseEntity
    {
        /*
         * byte revType = Convert.ToByte(pk.type_ver >> 8);
                string wpsdid = "";
                string WDCid = "";
                string RSSI = "";
                byte carState = 0;
                string voltage = "";
                string hardVer = "";
                string softVer = "";
                string hbPeriod = "";
         */
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
