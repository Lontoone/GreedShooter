using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerControl : MonoBehaviour
{
    public float speed = 5;
    public float shootSpeed = 1;
    public float dash_force = 20;

    public CursorControl cursorControl;
    public Weapon weapon;
    public Ammo ammo;

    ActionController actionController;
    public ActionController.mAction idle_act, move_act, shoot_act, dash_act, hurt_act, die_act, stop_act;

    public LayerMask targetLayer;

    Rigidbody2D rigidbody;
    Animator animator;
    HitableObj hitable;

    Vector2 _move;

    private void Start()
    {
        actionController = GetComponent<ActionController>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitable = GetComponent<HitableObj>();

        EquipWeapon(weapon.weaponData);
    }

    Vector2 _shoot_point;

    private void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {
            //TODO: Shoot evnet
            actionController.AddAction(shoot_act);
        }
        */
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            actionController.AddAction(dash_act);
        }

        //Auto Shoot
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            _shoot_point = GetArrowKeyValue();
            actionController.AddAction(shoot_act);

            Vector3 look_dir = _shoot_point;
            float m = look_dir.y / Mathf.Pow(look_dir.x * look_dir.x + look_dir.y * look_dir.y, 0.5f);
            float m_angle = (float)(Math.Atan(m)) * Mathf.Rad2Deg;
            weapon.transform.eulerAngles = new Vector3(0, 0, -m_angle) + transform.rotation.eulerAngles;
        }
        Debug.Log(_shoot_point);
    }

    private void FixedUpdate()
    {
        // rotate player 
        //if (cursorControl.cursor_world_position.x > transform.position.x)
        if (_shoot_point.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //rotate weapon to point at cursor 
        //Vector3 look_dir = cursorControl.transform.position - weapon.transform.position;
        //float m = look_dir.y / Mathf.Pow(look_dir.x * look_dir.x + look_dir.y * look_dir.y, 0.5f);
        //float m_angle = (float)(Math.Atan(m)) * Mathf.Rad2Deg;
        //weapon.transform.eulerAngles = new Vector3(0, 0, -m_angle) + transform.rotation.eulerAngles;


        //move or idle
        /* TODO: can shoot while walk?
        */
        _move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (_move.magnitude < 0.1f)
        {
            //actionController.AddAction(idle_act);
            animator.Play("Idle");
        }
        else
        {
            //actionController.AddAction(move_act);
            Move();
        }




    }

    public void EquipWeapon(WeaponData _newWeaponData)
    {

        shoot_act.action.RemoveAllListeners();

        //equip weapon
        shoot_act.action.AddListener(delegate
        {
            //Debug.Log(cursorControl.cursor_world_position);
            //weapon.Shoot((cursorControl.cursor_world_position - (Vector2)transform.position),
            weapon.Shoot(GetArrowKeyValue(),
                        ammo.ammoData,
                        targetLayer);
        });

        shoot_act.gap_time = _newWeaponData.shoot_gap_time;
        weapon.SetData(_newWeaponData);

    }
    Vector2 GetArrowKeyValue()
    {
        Vector2 _res = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            _res = new Vector2(-0.5f, 0.5f);

        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            _res = new Vector2(-0.5f, -0.5f);

        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            _res = new Vector2(0.5f, 0.5f);

        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            _res = new Vector2(0.5f, -0.5f);

        else if (Input.GetKey(KeyCode.UpArrow))
            _res = Vector2.up;

        else if (Input.GetKey(KeyCode.DownArrow))
            _res = Vector2.down;

        else if (Input.GetKey(KeyCode.LeftArrow))
            _res = Vector2.left;

        else if (Input.GetKey(KeyCode.RightArrow))
            _res = Vector2.right;
        return _res;
    }

    public void EquipAmmo(AmmoData _ammoData)
    {
        //check if the ammo is on scene:
        GameObject _obj = GCManager.Instantiate(_ammoData.GC_key);
        if (_obj == null)
        {
            //if not => create one new;
            _obj = Instantiate(_ammoData.ammo_prefab);
        }
        Ammo _newAmmo = _obj.GetComponent<Ammo>();
        ammo = _newAmmo;

        //TODO drop current ammo?;
    }


    public void Move()
    {
        //WASD to move
        if (rigidbody.velocity.magnitude < 1)
        {
            transform.position = (Vector2)transform.position + _move * speed * Time.deltaTime;
            //rigidbody.velocity = _move * speed;

            animator.Play("Walk");
        }

    }

    //Dash skill
    public void Dash()
    {
        Vector2 _dir = (cursorControl.cursor_world_position - (Vector2)transform.position).normalized;
        //rigidbody.velocity += (cursorControl.cursor_world_position - (Vector2)transform.position).normalized * dash_force;
        rigidbody.velocity += _move.normalized * dash_force;
        Debug.Log("dash " + rigidbody.velocity);

        hitable.isHitable = false;
        Invoke("SetHitable", 0.25f);
    }

    void SetHitable()
    {
        hitable.isHitable = true;
    }


    public static float Angle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

}
