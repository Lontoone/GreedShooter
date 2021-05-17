using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Collider2D))]
public class Ammo : MonoBehaviour
{
    public AmmoData ammoData;
    public Vector2 dir;
    public float _total_damage = 0;  //ammo damage * weapon's damage_multiplier 
    public LayerMask targetLayer;
    private void Start()
    {
        //gegister gc key
        GCManager.RegisterObject(ammoData.GC_key, gameObject);


    }
    private void OnEnable()
    {
        //self destory after life time
        Invoke("DoDestory", ammoData.life_time);



    }
    private void FixedUpdate()
    {
        //move alone dir
        transform.position = (Vector2)transform.position + dir * Time.deltaTime * ammoData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When Hit things
        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            HitableObj.Hit_event_c(collision.gameObject, ammoData.damage);

            Debug.Log(gameObject.name + " hits " + collision.gameObject.name);

            //Destory self:
            GCManager.Destory(ammoData.GC_key, gameObject);
        }
    }

    void DoDestory()
    {
        GCManager.Destory(ammoData.GC_key, gameObject);
    }

    //TODO:Ammo Hit event and effect


}
