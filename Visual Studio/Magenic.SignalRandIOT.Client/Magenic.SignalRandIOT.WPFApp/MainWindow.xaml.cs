using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Gauge;

namespace Magenic.SignalRandIOT.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _deviceHubProxy;

        public MainWindow()
        {
            _hubConnection = new HubConnection(ConfigurationManager.AppSettings["SignalRLocation"]);
            _deviceHubProxy = _hubConnection.CreateHubProxy(ConfigurationManager.AppSettings["DeviceHubName"]);
            _deviceHubProxy.On<DeviceData>("NewDataRecieved", x => { this.Dispatcher.Invoke(() => 
                {
                    if(x.DataType == "Temperature")
                    {
                        this.tempature_needle.Value = x.DataValue; 
                    }
                    else if (x.DataType == "Humidity")
                    {
                        this.humidity_needle.Value = x.DataValue; 
                    }
                    else if (x.DataType == "Light")
                    {
                        this.light_needle.Value = x.DataValue; 
                    }
                    else if (x.DataType == "Distance")
                    {
                        this.distance_needle.Value = x.DataValue; 
                    }
                });
            });
            InitializeComponent();
            this._hubConnection.Start().Wait();
        }
    }
}
