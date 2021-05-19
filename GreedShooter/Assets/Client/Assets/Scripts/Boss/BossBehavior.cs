using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    ActionController actionController;
    HitableObj hitableObj;
    public ActionController.mAction skill1, skill2, skill3, skill4;
    private void Start()
    {
        actionController = GetComponent<ActionController>();
        hitableObj = gameObject.GetComponent<HitableObj>();

        cDrawSkill = StartCoroutine(DrawASkill());
    }

    public LayerMask targetLayer;

    int current_weapon_index;
    int current_ammo_index;

    public Weapon[] weapons;
    public Ammo[] ammos;

    WaitForSeconds wait = new WaitForSeconds(3);
    Coroutine cDrawSkill;

    IEnumerator DrawASkill()
    {
        while (true)
        {
            //Draw random skill
            int _rand = Random.Range(0, 100);
            if (_rand <= 80)
            {
                actionController.AddAction(skill1);
            }

            yield return wait;
        }
    }


    public void SetWeapon(int _index)
    {

    }
    //default attack:  shoot 360 deg

    //Attack 1 : long shoot and spin
    public void Skill1(float rotateSpeed)
    {
        Vector2 _dir = new Vector2(
            Mathf.Cos(Time.time),
            Mathf.Sin(Time.time)
            );

        //weapons[weapon_index].Shoot();
        weapons[current_weapon_index].Shoot(
            _dir,
            ammos[current_ammo_index].ammoData,
            targetLayer
            );

        transform.Rotate(0, 0, rotateSpeed);

    }

    //Attack 2 : summon monster


    // ======= HP below 30%========

    //Attack 3 : long shoot and spin faster and reverse orientation(lower cool down time)

    //Attack 4 : map fire


}
