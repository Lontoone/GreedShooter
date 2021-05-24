using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAmmo : Ammo
{
    public float orbitSize = 1.5f;

    [SerializeField]
    Vector2 _start_pos;

    protected override void OnEnable()
    {
        base.OnEnable();
        _start_pos = transform.position;

    }
    public override void Move()
    {

        Vector2 _move = _start_pos + new Vector2(
                Mathf.Cos(Time.fixedTime * ammoData.speed),
                Mathf.Sin(Time.fixedTime * ammoData.speed)
            ) * orbitSize;

        transform.position = _move;

    }
}
