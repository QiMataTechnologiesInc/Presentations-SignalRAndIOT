using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.AspNet.SignalR.Client;

namespace Magenic.SignalRandIOT.Android
{
    [Activity(Label = "Magenic.SignalRandIOT.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private HubConnection _hubConnection;
        private IHubProxy _deviceHubProxy;        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            TextView button = FindViewById<TextView>(Resource.Id.MyButton);

            try
            {
                _hubConnection = new HubConnection("http://signalrandiot.azurewebsites.net");
                _deviceHubProxy = _hubConnection.CreateHubProxy("DeviceDataHub");
                _deviceHubProxy.On<DeviceData>("NewDataRecieved", x =>
                   RunOnUiThread(() =>
                   {
                       button.Text += x.DataType + "= " + x.DataValue + "\n";
                   }));

                this._hubConnection.Start();
            }
            catch (AggregateException aex)
            {
                button.Text = aex.InnerExceptions[0].Message;
            }
            catch (Exception ex)
            {
                button.Text = ex.Message;
            }
        }
    }
}

