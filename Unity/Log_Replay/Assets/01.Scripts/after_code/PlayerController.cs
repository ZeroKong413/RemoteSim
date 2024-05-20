using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{


    void Start()
    {
        GameManager.Input.KeyAction -= OnKeyboard; // " - " 의 의미는 다른 곳에서 구독을 진행하고 있으면 먼저 취소하고 신청한다는 의미.
        GameManager.Input.KeyAction += OnKeyboard;

    }

   
    void OnKeyboard()
    {
      

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W");
            GameManager.Communication.Send_data("W");
            StartCoroutine(GameManager.Communication.Message_recieve());
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("S");
            GameManager.Communication.Send_data("S");
            StartCoroutine(GameManager.Communication.Message_recieve());
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A");
            GameManager.Communication.Send_data("A");
            StartCoroutine(GameManager.Communication.Message_recieve());
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D");
            GameManager.Communication.Send_data("D");
            StartCoroutine(GameManager.Communication.Message_recieve());
        }

 
    }

    
}
