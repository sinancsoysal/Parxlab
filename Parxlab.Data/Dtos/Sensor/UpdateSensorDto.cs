using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parxlab.Data.Dtos.Sensor
{
    /// <summary>
    /// this class can be used for updateing sensor data
    /// </summary>
    public record UpdateSensorDto
    {
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// sensor ID 
        /// </summary>
        [Required]
        public string WPSDId { get; set; }

        /// <summary>
        /// data collector ID
        /// </summary>
        [Required]
        public string WDCId { get; set; }

        /// <summary>
        /// parking spot ID
        /// </summary>
        [Required]
        public int ParkId { get; set; }

        /// <summary>
        /// proximity sensor data
        /// </summary>
        [Required]
        public string RSSI { get; set; }

        /// <summary>
        /// car state
        /// </summary>
        [Required]
        public byte CarState { get; set; }

        /// <summary>
        /// battery voltage
        /// </summary>
        [Required]
        public string Voltage { get; set; }

        /// <summary>
        /// hardware version
        /// </summary>
        [Required]
        public string HardVer { get; set; }

        /// <summary>
        /// software version
        /// </summary>
        [Required]
        public string SoftVer { get; set; }

        /// <summary>
        /// heartbeat period
        /// </summary>
        [Required]
        public string HBPeriod { get; set; }

    }
}
