using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC_SelfDestoryItem : MonoBehaviour
{
    //public EffectType effectType;
    public float life_time = 1;
    
    
    public string key;
    

    public void Setup(string _key)
    {
        key = _key;
    }

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(key))
            Invoke("DestorySelf", life_time);
    }
    void DestorySelf()
    {
        GCManager.Destory(key, gameObject);
    }
}
