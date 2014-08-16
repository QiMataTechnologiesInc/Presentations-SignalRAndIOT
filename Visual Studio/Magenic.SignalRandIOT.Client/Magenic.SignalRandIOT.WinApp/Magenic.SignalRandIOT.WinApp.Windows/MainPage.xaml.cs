using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Telerik.UI.Xaml.Controls.DataVisualization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Magenic.SignalRandIOT.WinApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _deviceHubProxy;

        public MainPage()
        {
            _hubConnection = new HubConnection("http://localhost:56621");
            _deviceHubProxy = _hubConnection.CreateHubProxy("DeviceDataHub");
            _deviceHubProxy.On<DeviceData>("NewDataRecieved",async x =>
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, new Windows.UI.Core.DispatchedHandler(() =>
                    {
                        this.Gauge.Indicators[0].Value = x.DataValue;
                    }));
            });
            InitializeComponent();
            this._hubConnection.Start().Wait();
        }
    }
}
