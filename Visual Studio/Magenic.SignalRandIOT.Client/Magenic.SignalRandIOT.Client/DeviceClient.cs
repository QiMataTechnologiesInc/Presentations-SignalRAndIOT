using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Magenic.SignalRandIOT.Client
{
    class DeviceClient : ServiceControl
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _deviceHubProxy;
        private readonly SerialPort _port;

        public DeviceClient()
        {
            //connect to hub
            _hubConnection = new HubConnection(ConfigurationManager.AppSettings["SignalRLocation"]);
            _deviceHubProxy = _hubConnection.CreateHubProxy(ConfigurationManager.AppSettings["DeviceHubName"]);

            //setup and open port
            _port = new SerialPort("COM9", 9600);
            _port.DataReceived += _port_DataReceived;
            
        }

        void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //On data recieved 
            var port = (SerialPort)sender;
            _deviceHubProxy.Invoke<DeviceData>("SendNewData", DeviceData.Parse(port.ReadLine()));
        }

        public bool Start(HostControl hostControl)
        {
            try
            {
                //open the hub first so that the serial port data has a connection to use
                _hubConnection.Start().Wait();
                _port.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                if (_port.IsOpen)
                {
                    _port.Close();
                }

                if (_hubConnection.State != ConnectionState.Disconnected)
                {
                    _hubConnection.Stop();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}
