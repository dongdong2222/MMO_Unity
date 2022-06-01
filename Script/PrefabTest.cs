using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    GameObject prefab;

    GameObject tank;
    void Start()
    {
        //prefab = Resources.Load<GameObject>("Prefabs/Tank");
        Managers.Resource.Instantiate("Tank");
        //tank = Instantiate(prefab);

        Destroy(tank, 3.0f);
    }
}
