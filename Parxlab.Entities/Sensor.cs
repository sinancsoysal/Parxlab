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
        public string wpsdId { get; set; }
        public string wdcId { get; set; }
        public int parkId { get; set; }
        public string rssi { get; set; }
        public byte carState { get; set; }
        public string voltage { get; set; }
        public string hardVer { get; set; }
        public string softVer { get; set; }
        public string hbPeriod { get; set; }
    }
}
