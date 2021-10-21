using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ParkingSystem;
using Serilog;

namespace Parxlab.Service.Contracts.Impl
{
    public class SensorManager : ISensorManager
    {
        private TcpListener tcpListener;
        public SensorManager()
        {
            ParkingOriginalPacket.EvProcessReceivedPacket += sp_ProcessReceivedPacket;
        }

        public void StartListener(string ip, int port)
        {
            var ipAddress = IPAddress.Parse(ip);
            // Create a Socket that will use Tcp protocol      
            var listener = new Socket(ipAddress.AddressFamily, SocketType.Raw, ProtocolType.Tcp);
            // A Socket must be associated with an endpoint using the Bind method  
            listener.Bind(new IPEndPoint(ipAddress, 6000));
            // Specify how many requests a Socket can listen before it gives Server busy response.  
            // We will listen 10 requests at a time  
            listener.Listen(10);
            listener.BeginAccept(AcceptCallback, listener);

        }

        private void AcceptCallback(IAsyncResult ar)
        {
            var res = ar.IsCompleted;
        }

        private void ReceiveAccept()
        {
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                Log.Information("receive thread accepted");
                var wapper = new ParkingRemoteTCP(client);
            }
        }

        private void sp_ProcessReceivedPacket(baseReceivedPacket pk)
        {
            var revType = Convert.ToByte(pk.type_ver >> 8);
            string wpsdid = "";
            var WDCid = "";
            string RSSI;
            byte carState = 0;
            string voltage;
            string hardVer;
            string softVer;
            var deviceName = "";
            var hbPeriod = "";
            switch (pk)
            {
                case SensorHBeat hb:
                {
                    wpsdid = (hb.WPSD_ID).ToString("X2").PadLeft(8, '0');
                    WDCid = (hb.WDC_ID).ToString("X2").PadLeft(8, '0');
                    softVer = "v" + int.Parse(hb.APP_VER.ToString("X2").Substring(0, 1)).ToString() + "." +
                              int.Parse(hb.APP_VER.ToString("X2").Substring(1, 1)).ToString().PadLeft(2, '0');
                    hardVer = (hb.HARD_VER + 10).ToString();
                    hardVer = "v" + hardVer.Substring(0, 1) + "." + hardVer.Substring(1, 1);
                    voltage = (Math.Round((decimal) hb.VOLT / 10, 2)) + "V";
                    RSSI = (hb.RSSI - 30).ToString();
                    hbPeriod = hb.HB_PERIOD.ToString();
                    deviceName = GetDevName(hb.DEV_TYPE);
                    carState = hb.CAR_STATE;
                    break;
                }
                case SensorDetect detect:
                {
                    wpsdid = (detect.WPSD_ID).ToString("X2").PadLeft(8, '0');
                    WDCid = (detect.WDC_ID).ToString("X2").PadLeft(8, '0');
                    hardVer = ((int) (detect.HARD_VER) + 10).ToString();
                    hardVer = "v" + hardVer.Substring(0, 1) + "." + hardVer.Substring(1, 1);
                    deviceName = GetDevName(detect.DEV_TYPE);
                    carState = detect.CAR_STATE;
                    break;
                }
            }

            Log.Information(wpsdid + " " + WDCid + " " + carState);
        }

        public string GetDevName(byte byteName)
        {
            var devname = byteName switch
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

            return devname;
        }
    }
}