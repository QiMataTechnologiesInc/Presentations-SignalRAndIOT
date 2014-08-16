using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Magenic.SignalRandIOT.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
                {
                    x.Service<DeviceClient>();
                    x.RunAsLocalSystem();

                    x.SetDescription("Takes in device data and uploads it to the hub");
                    x.SetDisplayName("Device Service");
                    x.SetServiceName("DeviceService");
                });
        }
    }
}
