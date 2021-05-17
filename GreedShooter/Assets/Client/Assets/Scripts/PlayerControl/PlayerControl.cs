using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerControl : MonoBehaviour
{
    public float speed = 5;
    public float shootSpeed = 1;

    public CursorControl cursorControl;
    public Weapon weapon;
    public Ammo ammo;

    ActionController actionController;
    public ActionController.mAction idle_act, move_act, shoot_act, dash_act, hurt_act, die_act;



    private void Start()
    {
        actionController = GetComponent<ActionController>();

        EquipWeapon(weapon.weaponData);
    }



    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //TODO: Shoot evnet
            actionController.AddAction(shoot_act);
        }
    }

    private void FixedUpdate()
    {
        // rotate player 
        if (cursorControl.cursor_world_position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(180, 0, 0);
        }

        //rotate weapon to point at cursor 
        Vector3 look_dir = cursorControl.transform.position - weapon.transform.position;
        float m = look_dir.y / Mathf.Pow(look_dir.x * look_dir.x + look_dir.y * look_dir.y, 0.5f);
        float m_angle = (float)(Math.Atan(m)) * Mathf.Rad2Deg;
        weapon.transform.eulerAngles = new Vector3(0, 0, m_angle) + transform.rotation.eulerAngles;

        actionController.AddAction(move_act);

    }

    public void EquipWeapon(WeaponData _newWeaponData)
    {
        shoot_act.action.RemoveAllListeners();

        //equip weapon
        shoot_act.action.AddListener(delegate
        {
            weapon.Shoot((cursorControl.cursor_world_position - (Vector2)transform.position).normalized,
            ammo.ammoData);
        });

        shoot_act.gap_time = _newWeaponData.shoot_gap_time;


    }

    public void Move()
    {
        //WASD to move
        Vector2 _move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position = (Vector2)transform.position + _move * speed * Time.deltaTime;

    }

}
