using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    ActionController actionController;
    HitableObj hitableObj;
    BasicEnemy basicEnemy;
    public ActionController.mAction skill1, skill2, skill3, skill4;
    private void Start()
    {
        actionController = GetComponent<ActionController>();
        hitableObj = gameObject.GetComponent<HitableObj>();
        basicEnemy = GetComponent<BasicEnemy>();

        cDrawSkill = StartCoroutine(DrawASkill());
    }


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
            if (_rand <= 20)
            {
                actionController.AddAction(skill1);

            }
            else if (_rand < 40)
            {
                actionController.AddAction(skill2);
            }

            yield return wait;
        }
    }


    public void SetWeapon(int _index)
    {
        current_weapon_index = _index;
    }
    public void SetAmmo(int _index)
    {
        current_ammo_index = _index;
    }
    //default attack:  shoot 360 deg

    //Attack 1 : long shoot and spin
    public void Skill1(float rotateSpeed)
    {
        StartCoroutine(Skill_coro(0.05f, rotateSpeed));

    }
    IEnumerator Skill_coro(float _interval, float rotateSpeed)
    {
        float t = 0;
        WaitForSeconds _wait = new WaitForSeconds(_interval);
        while (t < skill1.duration)
        {
            t += _interval;
            Vector2 _dir = new Vector2(
           Mathf.Cos(Time.time) * rotateSpeed,
           Mathf.Sin(Time.time) * rotateSpeed
           );

            //weapons[weapon_index].Shoot();
            weapons[current_weapon_index].Shoot(
                _dir,
                ammos[current_ammo_index].ammoData,
                basicEnemy.targetLayer
                );

            transform.Rotate(0, 0, rotateSpeed);
            yield return _wait;
        }
    }


    //Attack 2 : summon Meteorite / monster
    public void Skill2(int _amount)
    {
        StartCoroutine(Skill2_coro(_amount));
    }
    IEnumerator Skill2_coro(int _amount)
    {
        int c = 0;
        WaitForSeconds _wait = new WaitForSeconds(1f);
        while (c < _amount)
        {
            int random_scale = Random.Range(2, 30);
            //Vector2 _pos = new Vector2(Mathf.Cos(Time.time), Mathf.Sin(Time.time)) * random_scale;
            Vector2 _pos = LevelManager.instance.randomPosInMap;
            weapons[current_weapon_index].Shoot(
               _pos,
               ammos[current_ammo_index].ammoData,
               basicEnemy.targetLayer
               );


            c++;
            yield return _wait;
        }
    }




    // ======= HP below 30%========

    //Attack 3 : long shoot and spin faster and reverse orientation(lower cool down time)

    //Attack 4 : map fire


}
