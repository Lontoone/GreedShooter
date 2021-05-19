using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoExtraFunction : MonoBehaviour
{
    [SerializeField]
    Weapon attack_setting;

    Ammo parent_ammo;
    private void Start()
    {
        parent_ammo = GetComponent<Ammo>();
    }
    //split to child and shoot
    public void SplitShoot(Ammo _ammo)
    {
        if (attack_setting != null)
        {
            attack_setting.Shoot(transform.up, _ammo.ammoData, parent_ammo.targetLayer);
        }
    }
}
