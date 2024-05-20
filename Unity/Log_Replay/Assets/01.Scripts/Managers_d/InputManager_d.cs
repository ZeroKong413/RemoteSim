using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_d
{
    public Action KeyAction = null;
    public Action<Define.MouseEvenet> MouseAction = null;

    bool _pressed = false;
    // Update is called once per frame
    public void OnUpdate()
    {

        if (Input.anyKey && KeyAction != null) 
        { 
            KeyAction.Invoke();
        }

        if(MouseAction != null) 
        {
            if(Input.GetMouseButton(0)) 
            {
                MouseAction.Invoke(Define.MouseEvenet.Press);
                _pressed = true;
            }
            else
            {
                if(_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvenet.Click);
                    _pressed = false;
                }
                
            }    
        }
    }
}
