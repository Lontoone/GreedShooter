using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject
{
    public float damage_multiplier = 1; // ammo damage * damage_multiplier 
    public float shoot_gap_time = 0.25f;
    public int ammo_row_count = 1;

    public float shoot_sight_angle = 120; //deg
    public Sprite img;

}
