using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtension 
{
    public static Vector2 rotate(this Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
