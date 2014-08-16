using UnityEngine;
using System.Collections;
using WebSocketSharp;

public class DataDeviceClient : MonoBehaviour {
    private WebSocket ws;

    public DataDeviceClient()
    {

    }

	// Use this for initialization
	void Start () {
        ws = new WebSocket("ws://localhost:56621/signalr/devicedatahub");
        ws.OnMessage += ws_OnMessage;
        ws.Connect();
	}

    void ws_OnMessage(object sender, MessageEventArgs e)
    {
        if (e.Data != "{}")
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
