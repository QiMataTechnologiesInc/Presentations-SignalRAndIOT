using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.SignalRandIOT.Client
{
    class DeviceData
    {
        public static DeviceData Parse(string deviceData)
        {
            try
            {
                var pieces = deviceData.Split(',');
                return new DeviceData
                {
                    DataType = pieces[0],
                    DataValue = Int32.Parse(pieces[1])
                };
            }
            catch (Exception)
            {
                return new DeviceData
                {
                    DataType = "Error"
                };
            }
        }

        public DeviceData()
        {
            DateRead = DateTime.Now;
        }

        public string DataType { get; set; }

        public int DataValue { get; set; }

        public DateTime DateRead { get; set; }
    }
}
