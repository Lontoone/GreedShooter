using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAmmo : Ammo
{
    public float orbitSize = 1.5f;

    [SerializeField]
    Vector2 start_pos;

    protected override void OnEnable()
    {
        base.OnEnable();
        //start_pos = transform.position;



    }
    public override void Move()
    {

        Vector2 _move = start_pos + new Vector2(
                Mathf.Cos(Time.fixedTime * ammoData.speed),
                Mathf.Sin(Time.fixedTime * ammoData.speed)
            ) * orbitSize;

        transform.position = _move;

    }
    public override void SetUP(Vector2 _start_pos, Vector2 _vec)
    {
        base.SetUP(_start_pos, _vec);
        start_pos = _start_pos + _vec.normalized * 5;
        orbitSize = Random.Range(1.5f, 5);


    }
}
