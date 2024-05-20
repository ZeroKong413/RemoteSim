using NetMQ;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.XR;

public class OculusInputManager
{
  
    public InputDevice LeftController;
    public InputDevice RightController;
    public Action InputDeviceAction = null;
    //public Action<Define.MouseEvenet> MouseAction = null;

    public void Connected_controller()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        TryInitialize();
    }

    // Update is called once per frame
    public void OnUpdate()
    {

        
       
    }

    void OnDeviceConnected(InputDevice device)
    {
        if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
        {
            Debug.Log("Leftdevice device Connect");
            LeftController = device;
        }
        else if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
        {
            Debug.Log("Rightdevice  device Connect");
            RightController = device;
        }

    }

    void TryInitialize()
    {
        Debug.Log("기기를 찾는 중입니다.");
        if (LeftController.isValid && RightController.isValid)
        {
            return;
        }

        var LeftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, LeftHandDevices);
        if (LeftHandDevices.Count > 0)
        {
            LeftController = LeftHandDevices[0];
            Debug.Log("Left controller connected\nRight controller connected");
        }

        var RightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, RightHandDevices);
        if (RightHandDevices.Count > 0)
        {
            RightController = RightHandDevices[0];
            Debug.Log("Left controller connected\nRight controller connected");
        }
    }

    void OnDestroy()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
    }


}
