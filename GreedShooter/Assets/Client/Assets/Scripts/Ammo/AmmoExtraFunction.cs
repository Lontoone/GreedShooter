using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoExtraFunction : MonoBehaviour
{
    [SerializeField]
    Weapon attack_setting;

    Ammo parent_ammo;
    public void Awake()
    {
        //originSize = transform.localScale;
    }
    private void Start()
    {
        parent_ammo = GetComponent<Ammo>();
    }
    //split to child and shoot
    public void SplitShoot(Ammo _ammo)
    {
        if (attack_setting != null)
        {
            Debug.Log("extra " + (transform == null) + " " + (_ammo == null) + " " + (parent_ammo == null));
            attack_setting.Shoot(transform.up, _ammo.ammoData, parent_ammo.targetLayer);
        }
    }
    public void SplitShoot(AmmoData _ammo)
    {
        if (attack_setting != null)
        {
            Debug.Log("extra " + (transform == null) + " " + (_ammo == null) + " " + (parent_ammo == null));
            attack_setting.Shoot(transform.up, _ammo, parent_ammo.targetLayer);
        }
    }


    private void OnDisable()
    {
        //back to origin size
        transform.localScale = Vector2.one;
    }

    //Vector2 originSize;
    public void GetBigger(float max)
    {
        transform.localScale = Vector2.one;
        StartCoroutine(GetBigger_coro(max));
    }

    IEnumerator GetBigger_coro(float max)
    {
        Vector2 _goalScale = new Vector2(max, max);
        WaitForFixedUpdate _wait = new WaitForFixedUpdate();
        while (transform.lossyScale.x != max)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, _goalScale, Time.deltaTime * 0.2f);

            yield return _wait;
        }
    }

    public void SetDriftedDir(float _angRange)
    {

    

        StartCoroutine(WaitToSetDriftedDir(_angRange));

    }
    IEnumerator WaitToSetDriftedDir(float _ran)
    {
        yield return new WaitForEndOfFrame();

        if (parent_ammo == null)
        {
            parent_ammo = GetComponent<Ammo>();
        }

        float _rand_angle = Random.Range(-_ran, _ran);
        //Vector2 _ang = parent_ammo.vec.rotate(Mathf.Deg2Rad * _rand_angle);
        Vector2 _ang = parent_ammo.vec.rotate(Mathf.Deg2Rad * _rand_angle);
        Debug.Log(gameObject.name + " " + parent_ammo.vec + " set ang " + _ang);


        parent_ammo.vec = _ang;
    }


}
