using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Assets.Models
{
    class NewDeviceDataContainer
    {
        private Dictionary<string, int> _valueDictionary;
        private Object _lock;

        public NewDeviceDataContainer()
        {
            _valueDictionary = new Dictionary<string, int>();
            _lock = new object();
        }

        public void Add(DeviceData data)
        {
            lock (_lock)
            {
                _valueDictionary[data.DataType] = data.DataValue;
            }
        }

        public IEnumerable<DeviceData> DeviceData
        {
            get
            {
                lock (_lock)
                {
                    foreach(var item in _valueDictionary)
                    {
                        yield return new DeviceData
                        {
                            DataType = item.Key,
                            DataValue = item.Value
                        };
                    }
                    _valueDictionary.Clear();
                }
            }
        }
    }
}
