namespace ParxlabSensor.Helpers
{
    public class DeviceHelper
    {
        public static string GetDevName(byte byteName)
        {
            return byteName switch
            {
                0x01 => "WDC-4003",
                0x02 => "WDC-4005",
                0x03 => "WDC-4008",
                0x04 => "WDC-4007",
                0x05 => "WPSD-340S3",
                0x06 => "WPSD-340S5",
                0x07 => "WPSD-340S8",
                0x08 => "WPSD-340S7",
                0x09 => "WPSD-340E3",
                0x0A => "WPSD-340E5",
                0x0B => "WPSD-340E8",
                0x0C => "WPSD-340E7",
                _ => "WDC-400x"
            };
        }
    }
}