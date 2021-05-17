using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    public virtual void Shoot(Vector2 _dir, AmmoData ammoData, LayerMask ammo_target_layer = default)
    {
        Debug.Log("shoot dir " + _dir);

        float _seg = weaponData.shoot_sight_angle / (weaponData.ammo_row_count + 1);
        //Vector2 _start_vec = Quaternion.AngleAxis(-weaponData.shoot_sight_angle * 0.5f, -transform.right) * _dir;
        Vector2 _start_vec = _dir.rotate(-weaponData.shoot_sight_angle * 0.5f);

        //multi row shoots (多重射擊)
        for (int i = 0; i < weaponData.ammo_row_count; i++)
        {
            Vector2 _divers_dir = _start_vec.rotate(_seg);

            //generate ammo
            Ammo _ammo = GCManager.Instantiate(ammoData.GC_key).GetComponent<Ammo>();
            _ammo.transform.position = transform.position;

            //_ammo.dir = _dir;
            _ammo.dir = _divers_dir;

            _ammo._total_damage = weaponData.damage_multiplier * ammoData.damage;
            _ammo.targetLayer = ammo_target_layer;

            _start_vec = _divers_dir;

        }

        //return _ammo;
    }


}
