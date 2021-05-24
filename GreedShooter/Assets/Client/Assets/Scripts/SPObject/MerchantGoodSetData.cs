using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Goods")]
public class MerchantGoodSetData : ScriptableObject
{
    //what merchant sells
    //public WeaponData[] weapons;
    //public AmmoData[] ammoDatas;
    public Good[] weaponGoods;
    public Good[] ammoGoods;

    public Good RandomWeapon
    {
        get
        {
            return weaponGoods[Random.Range(0, weaponGoods.Length)];
        }

    }
    public Good RandomAmmo
    {
        get
        {
            return ammoGoods[Random.Range(0, ammoGoods.Length)];
        }
    }


}

[System.Serializable]
public struct Good
{
    public ScriptableObject data;
    public int cost;
}
