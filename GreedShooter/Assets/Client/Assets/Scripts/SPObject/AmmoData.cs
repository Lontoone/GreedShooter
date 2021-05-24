using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Ammo")]
public class AmmoData : ScriptableObject
{
    public float speed=5;
    public float damage=5;
    public float life_time=0.5f;
    public string GC_key="GC_";
    public Sprite img;
    //public Sprite img;
    public GameObject ammo_prefab;

    public AudioClip sfx;
}
