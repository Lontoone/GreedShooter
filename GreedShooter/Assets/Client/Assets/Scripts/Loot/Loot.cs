using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public LootType type;
    public ScriptableObject data;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        SetUP(data);
    }

    public void SetUP(ScriptableObject _data)
    {
        data = _data;
        spriteRenderer = GetComponent<SpriteRenderer>();

        //temp  
        if (type == LootType.ammo)
            spriteRenderer.sprite = (data as AmmoData).img;
        else
            spriteRenderer.sprite = (data as WeaponData).img;
    }
}

public enum LootType
{
    weapon,
    ammo
}
