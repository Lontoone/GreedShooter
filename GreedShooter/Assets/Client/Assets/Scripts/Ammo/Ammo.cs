﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Ammo : MonoBehaviour
{
    public AmmoData ammoData;
    public Vector2 vec;
    //Vector2 _dir;
    public float _total_damage = 0;  //ammo damage * weapon's damage_multiplier 
    public LayerMask targetLayer;

    public UnityEvent OnDestory;
    Collider2D collider;
    ContactFilter2D filter2D;
    static Collider2D[] res = new Collider2D[200];

    protected virtual void Start()
    {
        //gegister gc key
        GCManager.RegisterObject(ammoData.GC_key, gameObject);
        collider = GetComponent<Collider2D>();

    }
    protected virtual void OnEnable()
    {
        //self destory after life time
        Invoke("DoDestory", ammoData.life_time);

        filter2D.SetLayerMask(targetLayer);

    }
    private void OnDisable()
    {
        CancelInvoke("DoDestory");
    }
    private void FixedUpdate()
    {
        Move();

        //detect collision
        int num = collider.OverlapCollider(filter2D, res);
        for (int i = 0; i < num; i++)
        {
            HitableObj.Hit_event_c(res[i].gameObject, ammoData.damage);
            Debug.Log("hit " + res[i].gameObject.name);
        }
        if (num > 0)
        {
            //effect
            ParticleEffectManager.instance.DOBlast(ParticleEffectManager.SMALL_BLAST_GC_KYE, transform.position, 0.2f);
            CameraFollow.CameraShake_c(0.01f, 0.1f, 2);
            //Destory self:
            DoDestory();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)  //TODO:de bug why new ammo can't hit?
    {
        /*
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
        */
    }

    public virtual void Move()
    {
        //move alone dir
        transform.position = (Vector2)transform.position + vec.normalized * Time.deltaTime * ammoData.speed;
    }

    protected void DoDestory()
    {
        if (OnDestory != null)
        {
            OnDestory.Invoke();
        }
        GCManager.Destory(ammoData.GC_key, gameObject);
    }

    //TODO:Ammo Hit event and effect


}
