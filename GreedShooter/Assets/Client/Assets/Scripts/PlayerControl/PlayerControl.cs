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

    Vector2 _move;

    private void Start()
    {
        actionController = GetComponent<ActionController>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        EquipWeapon(weapon.weaponData);
    }



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
        actionController.AddAction(shoot_act);
    }

    private void FixedUpdate()
    {
        // rotate player 
        if (cursorControl.cursor_world_position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //rotate weapon to point at cursor 
        Vector3 look_dir = cursorControl.transform.position - weapon.transform.position;
        float m = look_dir.y / Mathf.Pow(look_dir.x * look_dir.x + look_dir.y * look_dir.y, 0.5f);
        float m_angle = (float)(Math.Atan(m)) * Mathf.Rad2Deg;
        weapon.transform.eulerAngles = new Vector3(0, 0, -m_angle) + transform.rotation.eulerAngles;


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
        Debug.Log(" v " + rigidbody.velocity);



    }

    public void EquipWeapon(WeaponData _newWeaponData)
    {

        shoot_act.action.RemoveAllListeners();

        //equip weapon
        shoot_act.action.AddListener(delegate
        {
            //Debug.Log(cursorControl.cursor_world_position);
            //weapon.Shoot(cursorControl.cursor_world_position,
            weapon.Shoot((cursorControl.cursor_world_position - (Vector2)transform.position),
                        ammo.ammoData,
                        targetLayer);
        });

        shoot_act.gap_time = _newWeaponData.shoot_gap_time;
        weapon.SetData(_newWeaponData);

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
        rigidbody.velocity += (cursorControl.cursor_world_position - (Vector2)transform.position).normalized * dash_force;
        //rigidbody.AddForce(_dir * dash_force, ForceMode2D.Force);
        Debug.Log("dash " + rigidbody.velocity);
    }

}
