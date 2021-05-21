using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPon : Ammo
{
    public LayerMask wall_layer;
    public float turns = 3;
    protected override void OnEnable()
    {
        base.OnEnable();
        turns = 3;
    }
    public override void CollisionDetect()
    {
        int num = collider.OverlapCollider(filter2D, res);
        for (int i = 0; i < num; i++)
        {
            HitableObj.Hit_event_c(res[i].gameObject, ammoData.damage);
            Debug.Log("hit " + res[i].gameObject.name);
            turns--;
        }
        if (turns > 0 && num > 0)
        {
            //Vector2 _normal = vec.x > res[0].transform.position.x ? Vector2.right : Vector2.left;
            Vector2 _normal = (res[0].transform.position - transform.position).magnitude > 0.3f ? vec.rotate(-45 * Mathf.Deg2Rad) : -vec;
            vec = Vector2.Reflect(vec, _normal);
            //Vector3 _in = vec.normalized;
            //Vector3 _normal = GetSimpleNormal(res[0].gameObject);
            //vec = vec.rotate(90 * Vector3.Cross(vec, _normal).z);
            transform.position = (Vector2)transform.position + vec.normalized;

        }
        else if (turns < 0)
        {

            //Destory self:
            DoDestory();
        }
    }

    Vector2 GetSimpleNormal(GameObject _obj)
    {
        Vector2 offset = _obj.transform.position - transform.position;

        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {
            return new Vector2(1, 0) * Mathf.Sign(offset.x);
        }
        else
        {
            return new Vector2(0, 1) * Mathf.Sign(offset.y);
        }
    }
}
