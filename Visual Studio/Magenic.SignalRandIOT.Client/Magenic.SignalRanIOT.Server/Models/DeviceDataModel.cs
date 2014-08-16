using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magenic.SignalRanIOT.Server.Models
{
    public class DeviceDataModel
    {
        public string DataType { get; set; }

        public int DataValue { get; set; }

        public DateTime DateRead { get; set; }
    }
}