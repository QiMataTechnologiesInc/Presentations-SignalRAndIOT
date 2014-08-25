using Assets.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WebSocketSharp;

namespace Services
{
    class UnTypedActionContainer
    {
        public Type ActionType { get; set; }
        public Action<Object> Action { get; set; }
    };

    class SignalRClient
    {
        private WebSocket _ws;
        private string _connectionToken;
        private Dictionary<string,UnTypedActionContainer> _actionMap;

        public SignalRClient()
        {
            _actionMap = new Dictionary<string, UnTypedActionContainer>();
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://signalrandiot.azurewebsites.net/signalr/negotiate?connectionData=%5B%7B%22name%22%3A%22devicedatahub%22%7D%5D&clientProtocol=1.3&_=1408716619953");
            var response = (HttpWebResponse)webRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                _connectionToken = Uri.EscapeDataString(JsonConvert.DeserializeObject<NegotiateResponse>(sr.ReadToEnd()).ConnectionToken);
            }
        }

        public void Open()
        {
            if (_ws == null)
            {
                _ws = new WebSocket("ws://signalrandiot.azurewebsites.net/signalr/connect?transport=webSockets&connectionToken=" + _connectionToken + "&connectionData=%5B%7B%22name%22%3A%22devicedatahub%22%7D%5D&tid=" + UnityEngine.Random.Range(0, 11));
            }
            else
            {
                _ws = new WebSocket("ws://signalrandiot.azurewebsites.net/signalr/reconnect?transport=webSockets&connectionToken=" + _connectionToken + "&connectionData=%5B%7B%22name%22%3A%22devicedatahub%22%7D%5D&tid=" + UnityEngine.Random.Range(0, 11));
            }
            AttachAndConnect();
        }

        private void AttachAndConnect()
        {
            _ws.OnClose += _ws_OnClose;

            _ws.OnError += _ws_OnError;

            _ws.OnMessage += _ws_OnMessage;

            _ws.OnOpen += _ws_OnOpen;

            _ws.Connect();
        }

        void _ws_OnOpen(object sender, EventArgs e)
        {
            UnityEngine.Debug.Log("Opened Connection");
        }

        void _ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Data.Contains("\"H\":\"DeviceDataHub\""))
            {
                var deviceDataWrapper = JsonConvert.DeserializeObject<MessageWrapper>(e.Data).M[0];
                _actionMap[deviceDataWrapper.M].Action(deviceDataWrapper.A[0]);
            }
        }

        void _ws_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            UnityEngine.Debug.Log(e.Message);
        }

        void _ws_OnClose(object sender, CloseEventArgs e)
        {
            UnityEngine.Debug.Log(e.Reason + " Code: " + e.Code + " WasClean: " + e.WasClean);
        }

        public void On<T>(string method, Action<T> callback) where T: class
        {
            _actionMap.Add(method, new UnTypedActionContainer
            {
                Action = new Action<object>(x =>
                {
                    callback(x as T);
                }),
                ActionType = typeof(T)
            });
        }
    }
}
