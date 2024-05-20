using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController_d : MonoBehaviour
{
    // Start is called before the first frame update
    public float _Speed = 10.0f;

    // [SerializeField]
    // float _Speed = 10.0f;
    bool _moveTodDest = false;
    Vector3 _destPos;

    public Transform cameraTransform;
    public CharacterController characterController;
    public float moveSpeed = 10f;
    //이속
    public float jumpSpeed = 10f;
    //점속
    public float gravity = -20f;
    //중력
    public float yVelocity = 0;

    void Start()
    {
        //Managers.Input.KeyAction -= OnKeyboard; // " - " 의 의미는 다른 곳에서 구독을 진행하고 있으면 먼저 취소하고 신청한다는 의미.
        //Managers.Input.KeyAction += OnKeyboard;

        //Managers.Input.MouseAction -= OnMouseClicked;
        //Managers.Input.MouseAction += OnMouseClicked;
    }

    void Update()
    {
        if (_moveTodDest)
        {
            Animator anim = GetComponent<Animator>();
            anim.Play("RUN");
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.0001f)
            {
                _moveTodDest = false;
            }
            else
            {
                float moveDist = Mathf.Clamp(_Speed * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
                //transform.LookAt(transform.position);
            }
        }
        else
        {
            Animator anim = GetComponent<Animator>();
            anim.Play("WAIT");
        }


    }
    void OnKeyboard()
    {
        //// 키보드 조작 기초  로컬 기준 작동

        //// Local -> World
        //// TransformDirection

        //// World -> Local
        //// InverseTransformDirection

        ////특정 축을 기준으로 회전
        //// transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        //// transform.rotation = Quaternion.Euler(new Vector3(0.0f,Time.deltaTime * 100.0f, 0.0f));
        //// "W" 
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //회전
        //    //1
        //    //transform.rotation = Quaternion.LookRotation(Vector3.forward);
        //    //2
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);

        //    //움직임
        //    //1
        //    //transform.Translate(Vector3.forward * Time.deltaTime * _Speed);
        //    // //2
        //    //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _Speed);

        //    // 정답
        //    transform.position += Vector3.forward * Time.deltaTime * _Speed;
        //}
        //// "S"
        //if (Input.GetKey(KeyCode.S))
        //{
        //    //회전
        //    //1
        //    //transform.rotation = Quaternion.LookRotation(Vector3.back);
        //    //2
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);

        //    //움직임
        //    //1
        //    //transform.Translate(Vector3.forward * Time.deltaTime * _Speed);
        //    // //2(월드좌표)
        //    //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _Speed);

        //    transform.position += Vector3.back * Time.deltaTime * _Speed;
        //}
        //// "A"
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //회전
        //    //1
        //    // transform.rotation = Quaternion.LookRotation(Vector3.left);
        //    //2
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);

        //    //움직임
        //    //1
        //    //transform.Translate(Vector3.forward * Time.deltaTime * _Speed);
        //    // //2
        //    //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _Speed);

        //    transform.position += Vector3.left * Time.deltaTime * _Speed;
        //}
        //// "D"
        //if (Input.GetKey(KeyCode.D))
        //{
        //    //회전
        //    //1
        //    // transform.rotation = Quaternion.LookRotation(Vector3.right);
        //    //2
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);

        //    //움직임
        //    //1
        //    //transform.Translate(Vector3.forward * Time.deltaTime * _Speed);
        //    // //2
        //    //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _Speed);

        //    transform.position += Vector3.right * Time.deltaTime * _Speed;
        //}


        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _Speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _Speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _Speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _Speed;
        }


        _moveTodDest = false;
 
    }

    void OnMouseClicked(Define.MouseEvenet evt)
    {
        //if (evt != Define.MouseEvenet.Click)
        //{
        //    return;
        //}

     
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.green, 1.0f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if(hit.collider.gameObject.name == "Plane")
            {
                _destPos = hit.point;
                _moveTodDest = true;
            }
            
        }
    }
}
