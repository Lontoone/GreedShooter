using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Ammo : MonoBehaviour
{
    public AmmoData ammoData;
    public Vector2 dir;
    public float _total_damage = 0;  //ammo damage * weapon's damage_multiplier 
    public LayerMask targetLayer;

    public UnityEvent OnDestory;

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

    private void OnTriggerEnter2D(Collider2D collision)  //TODO:de bug why new ammo can't hit?
    {
        //When Hit things
        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            HitableObj.Hit_event_c(collision.gameObject, ammoData.damage);

            Debug.Log(gameObject.name + " hits " + collision.gameObject.name);

            //effect
            CameraFollow.CameraShake_c(0.05f, 0.25f, 2);
            ParticleEffectManager.instance.DOBlast(ParticleEffectManager.SMALL_BLAST_GC_KYE, transform.position, 0.2f);


            //Destory self:
            DoDestory();
        }
    }

    void DoDestory()
    {
        if (OnDestory != null)
        {
            OnDestory.Invoke();
        }
        GCManager.Destory(ammoData.GC_key, gameObject);
    }

    //TODO:Ammo Hit event and effect


}
