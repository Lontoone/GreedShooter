using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fall from sky.
//Don't normalized dir. dir is for des position.
public class Meteorite : Ammo
{
    //public Vector2 des_position;

    //ContactFilter2D filter2D;
    //Collider2D[] res = new Collider2D[5];

    public GameObject attack_hint;
    string hint_gc_key;
    protected override void Awake()
    {
        base.Awake();
        //temp: register hint object
        hint_gc_key = attack_hint.GetComponent<GC_SelfDestoryItem>()?.key;
        GCManager.RegisterObject(hint_gc_key, attack_hint);
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {

        //filter2D.SetLayerMask(targetLayer);
        //attackRange.enabled = false;
        collider.enabled = false;


    }
    public override void Move()
    {
        //base.Move();
        Vector2 goal = (Vector2)transform.position +
            (vec - (Vector2)transform.position);

        transform.position = Vector2.Lerp(transform.position, goal, Time.deltaTime * ammoData.speed);

        if (Vector2.Distance(vec, transform.position) < 0.1f)
        {
            //reach goal position

            //Effect
            ParticleEffectManager.instance.DOBlast(ParticleEffectManager.Large_BLAST_GC_KYE,
                transform.position,
                3f);

            //Attack
            collider.enabled = true;
            CollisionDetect();
            DoDestory();

        }
    }

    public override void SetUP(Vector2 _start_pos, Vector2 _vec)
    {

        //put hint in dest pos
        GameObject _hint = GCManager.Instantiate(hint_gc_key);

        //temp
        if (targetLayer == (targetLayer | (1 << LayerMask.NameToLayer("Player"))))
        {
            base.SetUP(_start_pos, _vec);
            _hint.transform.position = vec;
        }
        else
        {
            base.SetUP(_start_pos, CursorControl.instance.cursor_world_position + _vec.normalized *10);
            _hint.transform.position = CursorControl.instance.cursor_world_position;
        }

        //base.SetUP(_start_pos, CursorControl.instance.cursor_world_position);
        //_hint.transform.position = vec;
        //_hint.transform.position =CursorControl.instance.cursor_world_position;

        //move to the sky:
        transform.position = new Vector2(vec.x, vec.y + 50);
    }
}
