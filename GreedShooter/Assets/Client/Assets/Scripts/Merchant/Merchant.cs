using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{

    public MerchantGoodSetData goodSets;

    public SpriteRenderer ammo_preview, weapon_preview;

    [HideInInspector]
    public Good selling_weapon;
    [HideInInspector]
    public Good selling_ammo;

    public GameObject merchantObj;

    public void Start()
    {
        LevelManager.instance.OnLevelCleared += SetUp;

        //temp
        SetUp(0);
    }
    public void OnDestroy()
    {
        LevelManager.instance.OnLevelCleared -= SetUp;
    }

    public void SetUp(int _currentLv)
    {
        merchantObj.SetActive(true);

        //randomly draw a weapon and ammo data and set as preview
        selling_weapon = goodSets.RandomWeapon;
        selling_ammo = goodSets.RandomAmmo;

        ammo_preview.sprite = (selling_ammo.data as AmmoData).img;
        weapon_preview.sprite = (selling_weapon.data as WeaponData).img;

        weapon_preview.enabled = true;
        ammo_preview.enabled = true;
    }


    public bool BuyWeapon()
    {

        //check point is enough
        if (ScoreManager.instance.UsePoint(selling_weapon.cost))
        {
            //buy => create Loot item
            Loot loot = CreateLoot(selling_weapon.data, LootType.weapon);

            loot.transform.position = weapon_preview.transform.position;
            weapon_preview.enabled = false;
            return true;
        }
        else {
            return false;
        }
    }
    public bool BuyAmmo()
    {
        //check point is enough
        if (ScoreManager.instance.UsePoint(selling_ammo.cost))
        {
            //buy => create Loot item
            Loot loot = CreateLoot(selling_ammo.data, LootType.ammo);

            loot.transform.position = ammo_preview.transform.position;
            ammo_preview.enabled = false;
            return true;

        }
        else {
            return false;
        }
    }

    //close ans start next level
    public void NextLv()
    {
        LevelManager.instance.StartNextLevel();
        merchantObj.SetActive(false);

        //destory all unpicked loot
        Loot[] unpickedLoot = FindObjectsOfType<Loot>();
        for (int i = 0; i < unpickedLoot.Length; i++)
        {
            Destroy(unpickedLoot[i].gameObject);
        }

        RPGCore.SetDictionaryValue("talk",LevelManager.instance.current_lv.ToString());
        //TODO:effect
    }

    Loot CreateLoot(ScriptableObject _data, LootType _type)
    {
        GameObject loot_obj = new GameObject();
        loot_obj.AddComponent(typeof(SpriteRenderer));
        loot_obj.AddComponent(typeof(CircleCollider2D));
        loot_obj.GetComponent<CircleCollider2D>().isTrigger = true;

        Loot loot = loot_obj.AddComponent(typeof(Loot)).GetComponent<Loot>();
        loot.type = _type;
        loot.gameObject.layer = LayerMask.NameToLayer("Loot");
        loot.transform.localScale *= 2;
        loot.SetUP(_data);
        return loot;
    }


}
