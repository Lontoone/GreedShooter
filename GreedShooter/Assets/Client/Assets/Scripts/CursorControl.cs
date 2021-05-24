using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//display player's cursor as aim target
public class CursorControl : MonoBehaviour
{
    Camera _camer;

    public static CursorControl instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        Cursor.visible = false;
    }

    public Vector2 cursor_world_position
    {
        get { return _camer.ScreenToWorldPoint(Input.mousePosition); }
    }

    private void Start()
    {
        _camer = Camera.main;
    }
    private void FixedUpdate()
    {
        Vector2 _world_pos = _camer.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _world_pos;
    }
}
