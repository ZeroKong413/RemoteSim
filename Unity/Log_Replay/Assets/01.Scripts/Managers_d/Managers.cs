//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Managers : MonoBehaviour
//{

//    //// Instace  사용 '2번'방법
//    static Managers s_instance; // 유일성 보장
//    //public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고옴.
//    static Managers Instance { get { Init(); return s_instance; } } // s_instance를 외부에서 가져가지못함.

//    InputManager _input = new InputManager();
//    //ResourceManager _resource = new ResourceManager();
//    public static InputManager Input { get { return Instance._input; } }
//    //public static ResourceManager Resource { get { return Instance._resource; } }
//    // Start is called before the first frame update
//    void Start()
//    {
//        Init();

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        _input.OnUpdate();
//    }

//    static void Init()
//    {
//        if (s_instance == null)
//        {
//            GameObject go = GameObject.Find("@Managers");
//            if (go == null)
//            {
//                go = new GameObject { name = "@Managers" };
//                go.AddComponent<Managers>();
//            }

//            DontDestroyOnLoad(go);
//            s_instance = go.GetComponent<Managers>();
//        }

//    }
//}
