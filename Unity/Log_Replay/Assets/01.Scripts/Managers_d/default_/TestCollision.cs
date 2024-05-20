using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    //// 1차 버전
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision");
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Trigger");
    //}

    //2 차 버전
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log($"Collision @ {collision.gameObject.name}");
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Trigger");
    //}



    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{

    //    Vector3 look = transform.TransformDirection(Vector3.forward);
    //    Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

    //    RaycastHit[] hits;

    //    hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);

    //    foreach (RaycastHit hit in hits )
    //    {
    //        Debug.Log($"Raycast {hit.collider.gameObject.name}!");
    //    }

    //}

    private void Update()
    {

        //if (Input.GetMouseButtonDown(0))
        //{

        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    Vector3 dir = mousePos - Camera.main.transform.position;
        //    dir = dir.normalized;

        //    Debug.DrawRay(Camera.main.transform.position, dir*100.0f, Color.green, 1.0f);

        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        //    }

        //}


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

           
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.green, 1.0f);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }

        }

    }
}
