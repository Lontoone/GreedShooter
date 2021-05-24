using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    ActionController actionController;
    HitableObj hitableObj;
    BasicEnemy basicEnemy;
    public ActionController.mAction skill1, skill2, skill3, skill4, dash;
    private void Start()
    {
        actionController = GetComponent<ActionController>();
        hitableObj = gameObject.GetComponent<HitableObj>();
        basicEnemy = GetComponent<BasicEnemy>();

        cDrawSkill = StartCoroutine(DrawASkill());

        hitableObj.Die_event += Die;
    }
    public void OnDestroy()
    {
        hitableObj.Die_event -= Die;
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
                //actionController.AddAction(skill3);


            }
            else if (_rand < 40)
            {
                actionController.AddAction(skill2);
                //actionController.AddAction(skill3);

            }
            else if (_rand < 60)
            {
                actionController.AddAction(skill4);

            }
            else if (_rand < 80)
            {
                actionController.AddAction(skill3);
            }
            else
            {
                //actionController.AddAction(dash);
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

            //transform.Rotate(0, 0, rotateSpeed);
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
            Debug.Log("skill 2 " + _pos);
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

    //Attack 3 : cerate ball circles
    public void Skill3()
    {
        StartCoroutine(Skill3(10, 1));
    }
    IEnumerator Skill3(float _time, int _count)
    {
        Vector2[] _starts = new Vector2[_count];
        //WaitForFixedUpdate _wait = new WaitForFixedUpdate();
        WaitForSeconds _wait = new WaitForSeconds(0.25f);

        //create start points
        float _seg = 360 / _count;
        for (int i = 0; i < _count; i++)
        {
            _starts[i] = new Vector2(Mathf.Cos(_seg * i * Mathf.Deg2Rad), Mathf.Sin(_seg * i * Mathf.Deg2Rad));
        }

        SetWeapon(1);
        SetAmmo(1);
        //SetAmmo(Random.Range(0, ammos.Length));

        float _timeCounter = 0;
        while (_timeCounter < _time)
        {
            //rotate starts and create ammos
            for (int i = 0; i < _count; i++)
            {
                weapons[current_weapon_index].Shoot(
                                                    _starts[i] * _timeCounter * 0.5f,
                                                    ammos[current_ammo_index].ammoData,
                                                    basicEnemy.targetLayer
                                                    );

                _starts[i] += new Vector2(Mathf.Cos(Time.fixedTime), Mathf.Sin(Time.fixedTime));
            }
            //_timeCounter+=Time.fixedDeltaTime;
            _timeCounter += 0.25f;
            yield return _wait;
        }
    }

    //Attack 4 : move along wall and shoot
    public void Skill4()
    {
        StartCoroutine(Skill4(3));
    }
    IEnumerator Skill4(int _turn)
    {

        float shoot_gap_time = 0.05f;
        WaitForFixedUpdate _wait = new WaitForFixedUpdate();

        transform.rotation = Quaternion.Euler(0, 0, 0);

        //verticle
        Vector2[] verticle_endStart = {
            new Vector2(-20.5f, -14f),new Vector2(-20.5f, 18.6f)
        };
        Vector2 _movePoint = verticle_endStart[0];

        float _t = 0;
        int _turnCounter = 0;
        while (_turnCounter <= _turn)
        {
            transform.position = Vector2.Lerp(transform.position, _movePoint, Time.deltaTime);
            _t += Time.deltaTime;
            if (_t > shoot_gap_time)
            {
                _t = 0;
                //shoot
                weapons[current_weapon_index].Shoot(transform.right,
                                                    ammos[current_ammo_index].ammoData,
                                                    basicEnemy.targetLayer);

            }

            //reached end or start point.
            if (Vector2.Distance(_movePoint, transform.position) < 1f)
            {
                //reverse
                _turnCounter++;
                _movePoint = verticle_endStart[_turnCounter % 2];

            }

            yield return _wait;
        }


        //horizontal
        _turnCounter = 0;
        Vector2[] horizontal_endStart = {
            new Vector2(-20.5f, 18.6f),new Vector2(20.5f,18.6f)
        };
        _movePoint = horizontal_endStart[0];

        while (_turnCounter <= _turn)
        {
            transform.position = Vector2.Lerp(transform.position, _movePoint, Time.deltaTime);
            _t += Time.deltaTime;
            if (_t > shoot_gap_time)
            {
                _t = 0;
                //shoot
                weapons[current_weapon_index].Shoot(-transform.up,
                                                    ammos[current_ammo_index].ammoData,
                                                    basicEnemy.targetLayer);

                Debug.Log("skill4 shoot " + _turnCounter);
            }

            //reached end or start point.
            if (Vector2.Distance(_movePoint, transform.position) < 1f)
            {
                //reverse
                _turnCounter++;
                _movePoint = horizontal_endStart[_turnCounter % 2];

            }

            yield return _wait;
        }

        SetAmmo(0);
    }

    public void Dash_random()
    {
        StartCoroutine(DashMove(LevelManager.instance.randomPosInMap));
    }
    IEnumerator DashMove(Vector2 _goal)
    {
        WaitForFixedUpdate _wait = new WaitForFixedUpdate();
        while (Vector2.Distance(transform.position, _goal) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, _goal, Time.deltaTime);
            yield return _wait;
        }
    }

    public void Die() {
        //Player win.
        GameResultManager.instance.SetResult(true);
    }
}

