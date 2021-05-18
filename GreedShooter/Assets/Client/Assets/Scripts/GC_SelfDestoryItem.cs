using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC_SelfDestoryItem : MonoBehaviour
{
    public float life_time = 1;
    string key;

    public void Setup(string _key)
    {
        key = _key;
    }

    private void OnEnable()
    {
        Invoke("DestorySelf", life_time);
    }
    void DestorySelf()
    {
        GCManager.Destory(key, gameObject);
    }
}
