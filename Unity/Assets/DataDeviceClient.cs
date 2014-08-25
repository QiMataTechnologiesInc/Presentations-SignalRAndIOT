using UnityEngine;
using System.Collections;
using WebSocketSharp;
using System.Diagnostics;
using System.Net;
using System.IO;
using Assets.Models;
using Newtonsoft.Json;
using UnityEditor;
using System;
using Services;

public class DataDeviceClient : MonoBehaviour {

    private static SignalRClient _client = new SignalRClient();
    private static NewDeviceDataContainer _dataContainer = new NewDeviceDataContainer();

    private Camera _mainCamera;
    private Light _directionalLight;
    private GameObject _sphere;

	// Use this for initialization
	void Start () {
        _mainCamera = Camera.main;
        _directionalLight = GameObject.Find("Directional light").GetComponent<Light>();
        _sphere = GameObject.Find("Sphere");
        _client.On<DeviceData>("NewDataRecieved", new Action<DeviceData>(x =>
            {
                UnityEngine.Debug.Log("New Data " + x.DataType);
                _dataContainer.Add(x);
            }));
        _client.Open();
	}
	
	// Update is called once per frame
	void Update () {
        foreach(var deviceData in _dataContainer.DeviceData)
        {
            switch(deviceData.DataType)
            {
                case "Temperature":
                    _sphere.GetComponent<ParticleSystem>().enableEmission = deviceData.DataValue > 36;
                    break;
                case "Light":
                    _directionalLight.intensity = deviceData.DataValue - 32;
                    break;
                case "Distance":
                    _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x,
                        _mainCamera.transform.position.y, deviceData.DataValue / 10 * -1);
                    break;
            }
        }
	}
}
