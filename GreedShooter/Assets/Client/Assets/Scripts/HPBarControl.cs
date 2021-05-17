using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPBarControl : MonoBehaviour
{
    public HitableObj hitable;

    public Image bar;

    float maxHP;

    public const string HPBAR_GC_KEY = "HPBAR_GC_KEY";

    private void Awake()
    {
        GCManager.RegisterObject(HPBAR_GC_KEY, gameObject);
    }
    public void SetHitable(HitableObj _hitable)
    {
        hitable = _hitable;
        maxHP = hitable.HP;
        hitable.gotHit_event += UpdateHPBar;
        hitable.Die_event += DestorySelfOnDie;
    }

    private void OnEnable()
    {
        if (hitable == null) { return; }

        maxHP = hitable.HP;
        hitable.gotHit_event += UpdateHPBar;
        hitable.Die_event += DestorySelfOnDie;
    }
    private void OnDisable()
    {
        if (hitable == null) { return; }

        hitable.gotHit_event -= UpdateHPBar;
        hitable.Die_event -= DestorySelfOnDie;
    }

    void DestorySelfOnDie()
    {
        GCManager.Destory(HPBAR_GC_KEY, gameObject);
    }

    private void FixedUpdate()
    {
        //follow gameobject

        //TODO: get sprite height offset
        Vector2 _pos = new Vector2(hitable.transform.position.x, hitable.transform.position.y + 0.25f);
        transform.position = _pos;

    }

    void UpdateHPBar()
    {
        bar.fillAmount = hitable.HP / (float)maxHP;

    }
}
