using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.XR;
using NetMQ;

public class VRPlayerController : MonoBehaviour
{
    public InputDevice LeftController;
    public InputDevice RightController;

    int left_w = 0;
    int left_s = 0;
    int left_a = 0;
    int left_d = 0;

    int right_w = 0;
    int right_s = 0;
    int right_a = 0;
    int right_d = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        //AsyncIO.ForceDotNet.Force();
        GameManager.Oculus_Input.InputDeviceAction -= manual_stick_data;
        GameManager.Oculus_Input.InputDeviceAction += manual_stick_data;

    }

    // Update is called once per frame
    void manual_stick_data()
    {

        if (LeftController.isValid)
        {
            Vector2 leftStick;
            if (LeftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftStick))
            {

                if (leftStick[1] > 0.981)
                {
                    left_s += 1;

                    if (left_s == 9)
                    {
                        Debug.Log("Transfer Data :  s\n Data 傈价 蜡公 : 傈价");
                        //GameManager.Communication.publisher.SendFrame("s");
                        //client.SendFrame("s");
                        left_s = 0;
                    }

                }

                if (leftStick[1] < -0.981)
                {
                    left_w += 1;

                    if (left_w == 9)
                    {
                        Debug.Log("Transfer Data :  w\n Data 傈价 蜡公 : 傈价");
                        left_w = 0;
                    }
                }

                if (leftStick[0] > 0.981)
                {
                    left_d += 1;

                    if (left_d == 7)
                    {
                        Debug.Log("Transfer Data :  d\n Data 傈价 蜡公 : 傈价");
                        left_d = 0;
                    }
                }

                if (leftStick[0] < -0.981)
                {
                    left_a += 1;
                    if (left_a == 7)
                    {
                        Debug.Log("Transfer Data :  a\n Data 傈价 蜡公 : 傈价");
                        left_a = 0;
                    }
                }
            }

        }
        ///////////////////////////////////////////////////////////////////////////////////////
 
        if (RightController.isValid)
        {
            Vector2 rightStick;
            if (RightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightStick))
            {

                if (rightStick[1] > 0.981)
                {
                    right_s += 1;

                    if (right_s == 9)
                    {
                        Debug.Log("Transfer Data :  s\n Data 傈价 蜡公 : 傈价");
                        //GameManager.Communication.publisher.SendFrame("a");
                        //client.SendFrame("s");
                        right_s = 0;
                    }

                }

                if (rightStick[1] < -0.981)
                {
                    right_w += 1;

                    if (right_w == 9)
                    {
                        Debug.Log("Transfer Data :  w\n Data 傈价 蜡公 : 傈价");
                        right_w = 0;
                    }
                }

                if (rightStick[0] > 0.981)
                {
                    right_d += 1;

                    if (right_d == 7)
                    {
                        Debug.Log("Transfer Data :  d\n Data 傈价 蜡公 : 傈价");
                        right_d = 0;
                    }
                }

                if (rightStick[0] < -0.981)
                {
                    right_a += 1;
                    if (right_a == 7)
                    {
                        Debug.Log("Transfer Data :  a\n Data 傈价 蜡公 : 傈价");
                        right_a = 0;
                    }
                }
            }

        }

    }
}
