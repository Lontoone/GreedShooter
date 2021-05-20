using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC_AutoRegister : MonoBehaviour
{
    public string GC_key = "";
    private void Awake()
    {
        GCManager.RegisterObject(GC_key,gameObject);
    }
}
