using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    //1번 방법 
    //public GameObject prefab;

    //2번 방법
    GameObject prefab;
    //2,3번 공통
    GameObject tank;

    // Start is called before the first frame update
    void Start()
    {
        ////2번과정
        //prefab = Resources.Load<GameObject>("01.Prefab/Tank");

        ////1,2번 공통
        //tank = Instantiate(prefab);

        //3번 : resource Manager를 통한 생성
        //tank = Managers.Resource.Instantiate("Tank");

        //Destroy(tank,3.0f);
        
    }

}
