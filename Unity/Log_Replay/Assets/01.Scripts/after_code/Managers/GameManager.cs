using NetMQ;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //// Instace  사용 '2번'방법
    static GameManager G_instance; // 유일성 보장
    //public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고옴.
    static GameManager Instance { get { Init(); return G_instance; } } // s_instance를 외부에서 가져가지못함.

    OculusInputManager _oclusinput = new OculusInputManager();
    //WebRTCManager _webrtc = new WebRTCManager();
    CommunicationManager _communication = new CommunicationManager();

    //Keyboard test
    InputManager _input = new InputManager();
    public static OculusInputManager Oculus_Input { get { return Instance._oclusinput; } }
    //public static WebRTCManager WebRTC { get { return Instance._webrtc; } }
    public static CommunicationManager Communication { get { return Instance._communication; } }
    public static InputManager Input { get { return Instance._input; } }

    // Start is called before the first frame update

    private void Awake()
    {
        Init();

        _communication.Checksocket();

        StartCoroutine(_communication.Message_recieve());

    }
    void Start()
    {


        //_communication.Send_data("Good!");

        //_input.Connected_controller();
        //_communication.Checksocket();


        //_communication.InChecksocket();
        //_communication.Message_recieve();
        //_communication._clientCancelled = false;
        //_communication._clientThread = new Thread(_communication.Message_recieve);
        //_communication._clientThread.Start();

    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();

    }

    static void Init()
    {
        if (G_instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(go);
            G_instance = go.GetComponent<GameManager>();
        }

    }
    void OnDestroy()
    {
        //if (_communication._clientThread != null)
        //{
        //    _communication._clientCancelled = true;
        //    _communication._clientThread.Join();
        //}
        _communication.socket.Close();
    }

}
