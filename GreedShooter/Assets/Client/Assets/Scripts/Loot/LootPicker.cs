using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPicker : MonoBehaviour
{
    public Collider2D pick_range;
    ContactFilter2D filter2D;
    Collider2D[] res = new Collider2D[5];
    public LayerMask pickable_layer;

    PlayerControl player;


    private void Start()
    {
        filter2D.SetLayerMask(pickable_layer);
        filter2D.useTriggers = true;

        player = FindObjectOfType<PlayerControl>();
    }
    private void Update()
    {
        //press e to pick weapon
        if (Input.GetKeyDown(KeyCode.E))
        {
            //pick loot
            int num = pick_range.OverlapCollider(filter2D, res);
            if (num > 0)
            {
                Debug.Log("pick!" + res[0].gameObject.name);
                Loot _loot = res[0].GetComponent<Loot>();

                if (_loot.type == LootType.weapon)
                {
                    //exchange weapon with this and player's
                    string _old_weaponData_name = player.weapon.weaponData.name;
                    player.EquipWeapon(Resources.Load<WeaponData>("Data/Weapon/" + _loot.data.name));

                    _loot.data = Resources.Load<WeaponData>("Data/Weapon/" + _old_weaponData_name);
                    _loot.spriteRenderer.sprite = (_loot.data as WeaponData).img;
                }
                else
                {
                    string _old_ammo_data = player.ammo.ammoData.name;
                    player.EquipAmmo((AmmoData)_loot.data);
                    _loot.data = Resources.Load<AmmoData>("Data/Ammo/" + _old_ammo_data);
                    Debug.Log("ammo load "+ _loot.data);
                    _loot.spriteRenderer.sprite = (_loot.data as AmmoData).img;
                }

            }
        }
    }
}
