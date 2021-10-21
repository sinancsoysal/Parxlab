using Parxlab.Entities.Enums;

namespace Parxlab.Data.Dtos.Sensor
{
    public record SensorDto
    {
        public string Ip { get; init; }
        public int Port { get; init; }
        public SensorStatus Status { get; init; }
        public string WPSDId { get; init; }
    }
}
