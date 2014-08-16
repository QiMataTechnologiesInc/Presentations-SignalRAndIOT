using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.AspNet.SignalR.Client;
using Telerik.Windows.Controls;

namespace Magenic.SignalRandIOT.WinApp.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _deviceHubProxy;

        // Constructor
        public MainPage()
        {
            _hubConnection = new HubConnection("http://localhost:56621");
            _deviceHubProxy = _hubConnection.CreateHubProxy("DeviceDataHub");
            _deviceHubProxy.On<DeviceData>("NewDataRecieved", x =>
            {
                Dispatcher.BeginInvoke(() => 
                {
                    ((ArrowGaugeIndicator)this.Gauge.Indicators.Single(y => y.GetType() == typeof(ArrowGaugeIndicator))).Value = x.DataValue;
                });
                
            });
            InitializeComponent();
            this._hubConnection.Start();
        }
    }
}
