using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fall from sky.
//Don't normalized dir. dir is for des position.
public class Meteorite : Ammo
{
    //public Vector2 des_position;
    public Collider2D attackRange;
    ContactFilter2D filter2D;
    Collider2D[] res = new Collider2D[5];

    public GameObject attack_hint;
    string hint_gc_key;
    protected override void Start()
    {
        //temp: register hint object
        hint_gc_key = attack_hint.GetComponent<GC_SelfDestoryItem>()?.key;
        GCManager.RegisterObject(hint_gc_key, attack_hint);
    }

    protected override void OnEnable()
    {
        //move to the sky:
        transform.position = new Vector2(vec.x, vec.y + 10);
        filter2D.SetLayerMask(targetLayer);
        attackRange.enabled = false;

        //put hint in dest pos
        GameObject _hint = GCManager.Instantiate(hint_gc_key);
        _hint.transform.position = (Vector2)transform.position + vec;
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

            //Attack
            attackRange.enabled = true;
            int _hits = attackRange.OverlapCollider(filter2D, res);
            for (int i = 0; i < _hits; i++)
            {
                HitableObj.Hit_event_c(res[i].gameObject, _total_damage);
            }

            DoDestory();
        }
    }
}
