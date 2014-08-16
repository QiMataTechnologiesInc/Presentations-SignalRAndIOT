using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Magenic.SignalRanIOT.Server.Models;

namespace Magenic.SignalRanIOT.Server.Hubs
{
    public class DeviceDataHub : Hub
    {
        public void SendNewData(DeviceDataModel deviceData)
        {
            Clients.All.NewDataRecieved(deviceData);
        }
    }
}