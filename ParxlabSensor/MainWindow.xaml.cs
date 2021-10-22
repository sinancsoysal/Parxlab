using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ParkingSystem;
using ParxlabSensor.Cache;
using ParxlabSensor.Helpers;
using Tcp.NET.Client;
using Tcp.NET.Client.Events.Args;
using Tcp.NET.Client.Models;
using WatsonTcp;

namespace ParxlabSensor
{
    public partial class MainWindow : Window
    {
        private bool isListening;

        /// <summary>
        /// <remarks>IP Address</remarks>
        /// </summary>
        private static IPAddress localIP;

        /// <summary>
        /// <remarks>TCP portNum</remarks>
        /// </summary>
        private static ushort portNum;

        /// <summary>
        /// <remarks>Tcp Client</remarks>
        /// </summary>
        private static TcpClient client = new TcpClient();

        /// <summary>
        /// <remarks>Tcp Thread</remarks>
        /// </summary>
        private Thread m_serverThread;

        /// <summary>
        /// <remarks>tcp Listener</remarks>
        /// </summary>
        private static TcpListener tcpListener;
        CancellationTokenSource cts = new();
        public MainWindow()
        {
            InitializeComponent();
            var ip = Barrel.Current.Get<string>("IpAddress");
            if (!string.IsNullOrEmpty(ip))
                IpTextBox.Text = ip;

            var port = Barrel.Current.Get<string>("Port");
            if (!string.IsNullOrEmpty(port))
                PortTextBox.Text = port;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void IpTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(IpTextBox.Text))
                Barrel.Current.Add("IpAddress", IpTextBox.Text, TimeSpan.FromDays(365));
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ParkingOriginalPacket.EvProcessReceivedPacket += ProcessReceivedPacket;
        }


        private void ConnectBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (isListening)
            {
                StopListening();
                return;
            }

            if (string.IsNullOrEmpty(PortTextBox.Text))
            {
                MessageBox.Show("Please input port number", "Validation Error", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(IpTextBox.Text))
            {
                MessageBox.Show("Please input ip address", "Validation Error", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
                return;
            }

            var ip = new Regex(
                @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");
            if (!ip.IsMatch(IpTextBox.Text))
            {
                MessageBox.Show("IP Address is invalid", "Validation Error", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
                return;
            } 
            portNum = ushort.Parse(PortTextBox.Text);
            if (portNum is > 0 and < 65535)
            {
                StartListening();
            }
            else
            {
                MessageBox.Show("Port Range:1~65535!");
            }
        }

        private async void StartListening()
        {
            try
            {
                ITcpNETClient client = new TcpNETClient(new ParamsTcpClient
                {
                    Uri = IpTextBox.Text,
                    Port = portNum,
                    EndOfLineCharacters = "\r\n",
                    IsSSL = false
                });
                client.ConnectionEvent += ConnectionEventChanged;
                client.MessageEvent += MessageEventChanged;
                client.ErrorEvent += ErrorEventFired;
                await client.ConnectAsync();
                //localIP = IPAddress.Parse(IpTextBox.Text);
                //tcpListener = new TcpListener(localIP, portNum);
                //tcpListener.Start();
                //m_serverThread = new Thread(ReceiveAccept);
                //m_serverThread.Start();
                //m_serverThread.IsBackground = true;
                //  ReceiveAccept();
                isListening = true;
                ConnectBtn.Content = "Stop Listening";
            }
            catch (SocketException ex)
            {
               // tcpListener.Stop();
                eth_Setclose();
                MessageBox.Show("TCP Server Listen Error:" + ex.Message,"Socket Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (Exception err)
            {
                tcpListener.Stop();
                eth_Setclose();
                MessageBox.Show("TCP Server Listen Error:" + err.Message, "Socket Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static Task ErrorEventFired(object sender, TcpErrorClientEventArgs args)
        {
            var res = args.Exception;
            var tt = args.Message;
            return Task.CompletedTask;
        }

        private static Task MessageEventChanged(object sender, TcpMessageClientEventArgs args)
        {
            throw new NotImplementedException();
        }

        private static Task ConnectionEventChanged(object sender, TcpConnectionClientEventArgs args)
        {
            throw new NotImplementedException();
        }

        private async void ReceiveAccept()
        {
            while (true)
            {
                try
                {
                    client = await tcpListener.AcceptTcpClientAsync();

                    //listenstatelabel.ForeColor = Color.Lime;
                    //connectstatelabel.Text = "connect ok";
                    //deviceIPstatelabel.Text = client.Client.RemoteEndPoint.ToString();
                    //ethNetOpenState = true;
                    var wapper = new ParkingRemoteTCP(client);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Faaaaaileeeed");

                    eth_Setclose();
                    // MessageBox.Show(ex.Message, "?", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public async Task<TcpClient> GetTcpClient(TcpListener listener, CancellationToken token)
        {
            await using (cts.Token.Register(listener.Stop))
            {
                try
                {
                    var client = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                    return client;
                }
                catch (ObjectDisposedException)
                {
                    Debug.WriteLine("Expooseeeed");
                    // Token was canceled - swallow the exception and return null
                    if (token.IsCancellationRequested) return null;
                    throw;
                }
            }
        }
        private void ProcessReceivedPacket(baseReceivedPacket pk)
        {
            try
            {
                var revType = Convert.ToByte(pk.type_ver >> 8);
                string wpsdid;
                var WDCid = "";
                string RSSI;
                byte carState;
                string voltage;
                string hardVer;
                string softVer;
                var deviceName = "";
                var hbPeriod = "";

                switch (pk)
                {
                    case SensorHBeat hb:
                    {
                        // reshow(hb.recRawData, true);
                        wpsdid = (hb.WPSD_ID).ToString("X2").PadLeft(8, '0');
                        WDCid = (hb.WDC_ID).ToString("X2").PadLeft(8, '0');
                        softVer = "v" + int.Parse(hb.APP_VER.ToString("X2").Substring(0, 1)).ToString() + "." +
                                  int.Parse(hb.APP_VER.ToString("X2").Substring(1, 1)).ToString().PadLeft(2, '0');
                        hardVer = (hb.HARD_VER + 10).ToString();
                        hardVer = "v" + hardVer.Substring(0, 1) + "." + hardVer.Substring(1, 1);
                        voltage = (Math.Round((decimal) hb.VOLT / 10, 2)) + "V";
                        RSSI = (hb.RSSI - 30).ToString();
                        hbPeriod = hb.HB_PERIOD.ToString();
                        deviceName = DeviceHelper.GetDevName(hb.DEV_TYPE);
                        carState = hb.CAR_STATE;
                        if (carState == 0x01)
                        {
                            LogBox.AppendText("wpsd id:" + wpsdid + "\nsoft Ver:" + softVer + "\nhard Ver:" + hardVer +
                                              "\nvoltage:" + voltage + "\nRSSI:" + RSSI + "\ncar State:Have Car\n");
                        }
                        else
                        {
                            LogBox.AppendText("wpsd id:" + wpsdid + "\nsoft Ver:" + softVer + "\nhard Ver:" + hardVer +
                                              "\nvoltage:" + voltage + "\nRSSI:" + RSSI + "\ncar State:No Car\n");
                        }

                        break;
                    }
                    case SensorDetect detect:
                    {
                        // reshow(detect.recRawData, true);
                        wpsdid = detect.WPSD_ID.ToString("X2").PadLeft(8, '0');
                        WDCid = detect.WDC_ID.ToString("X2").PadLeft(8, '0');
                        hardVer = (detect.HARD_VER + 10).ToString();
                        hardVer = "v" + hardVer[..1] + "." + hardVer.Substring(1, 1);
                        deviceName = DeviceHelper.GetDevName(detect.DEV_TYPE);
                        carState = detect.CAR_STATE;
                        if (carState == 0x01)
                        {
                            LogBox.AppendText("wpsd id:" + wpsdid + "\nhard Ver:" + hardVer + "\ncar State:Have Car\n");
                        }
                        else
                        {
                            LogBox.AppendText("wpsd id:" + wpsdid + "\nhard Ver:" + hardVer + "\ncar State:No Car\n");
                        }

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void eth_Setclose()
        {
            ConnectionStsLbl.Content = "Not Connected";
            ConnectBtn.Content = "Start Listening";
            isListening = false;
        }

        private void StopListening()
        {
            try
            {
                tcpListener.Stop();
#pragma warning disable SYSLIB0006 // Type or member is obsolete
                m_serverThread.Abort();
#pragma warning restore SYSLIB0006 // Type or member is obsolete
                m_serverThread = null;
                client.Close();
                eth_Setclose();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("TCP Server Listen Error!" + ex.Message,"TCP Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void PortTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PortTextBox.Text))
                Barrel.Current.Add("Port", PortTextBox.Text, TimeSpan.FromDays(365));
        }
    }
}