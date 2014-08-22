using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;

namespace Magenic.SignalRandIOT.Android
{
    [Activity(Label = "Magenic.SignalRandIOT.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        private HubConnection _hubConnection;
        private IHubProxy _deviceHubProxy;
        private SynchronizationContext _context;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _hubConnection = new HubConnection("http://10.0.2.2:56621");
            _deviceHubProxy = _hubConnection.CreateHubProxy("DeviceDataHub");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            _deviceHubProxy.On<DeviceData>("NewDataRecieved", x =>
            {
                _context.Post(delegate
                {
                    button.Text = x.DataValue.ToString();
                }, state: null);
            });
            try
            {
                this._hubConnection.Start().Wait();
            }
            catch (AggregateException ex)
            {
                button.Text = ex.InnerExceptions[0].Message;
            }
        }
    }
}

