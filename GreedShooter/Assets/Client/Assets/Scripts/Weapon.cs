using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    public virtual Ammo Shoot(Vector2 _dir, AmmoData ammoData)
    {
        Debug.Log("shoot  " + _dir);

        float _seg = weaponData.shoot_sight_angle / (weaponData.ammo_row_count + 1);

        //generate ammo
        Ammo _ammo = GCManager.Instantiate(ammoData.GC_key).GetComponent<Ammo>();
        _ammo.transform.position = transform.position;
                                                      
        _ammo.dir = _dir;
        //TODO: multi shoot

        _ammo._total_damage = weaponData.damage_multiplier * ammoData.damage;

        return _ammo;

    }


}
