using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
  
    public virtual void Shoot(Vector2 _dir, AmmoData ammoData)
    {
        Debug.Log("shoot  "+_dir);
        //TODO: generate ammo

        Ammo _ammo = GCManager.Instantiate(ammoData.GC_key).GetComponent<Ammo>();
        _ammo.transform.position = transform.position; //TODO: shoot position 
        _ammo.dir = _dir;
        
        _ammo._total_damage = weaponData.damage_multiplier * ammoData.damage;
    }


}
