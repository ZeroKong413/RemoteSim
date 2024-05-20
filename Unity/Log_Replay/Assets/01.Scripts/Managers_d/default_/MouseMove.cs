using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float sensitivity = 500f;
    //마우스 민감도
    public float rotationX;
    public float rotationY;

    void Start()
    {

    }

    void Update()
    {
        float MouseMoveX = Input.GetAxis("Mouse X");
        //마우스 x축 움직임값 받아서 저장
        float MouseMoveY = Input.GetAxis("Mouse Y");
        //마우스 y축 움직임값 받아서 저장

        rotationY += MouseMoveX * sensitivity * Time.deltaTime;
        rotationX += MouseMoveY * sensitivity * Time.deltaTime;

        if (rotationX > 35f)
        //고개 너무 안올라가게
        {
            rotationX = 35f;
        }

        if (rotationX < -30f)
        //고개 너무 안숙여지게
        {
            rotationX = -30f;
        }

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
    }

}
