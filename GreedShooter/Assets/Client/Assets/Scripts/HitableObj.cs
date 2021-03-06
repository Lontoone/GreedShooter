using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
//可攻擊物件
public class HitableObj : MonoBehaviour
{
    public static event Action<GameObject> OnKilled;
    private static event Action<GameObject, float> Hit_event;
    public event Action Die_event;
    public event Action gotHit_event;
    public float HP = 20;
    float maxHP;

    public bool isDead = false;
    public bool isHitable = true;

    HPBarControl hpbar;
    private IEnumerator Start()
    {
        //wait for hp bar to finish regiester:
        yield return new WaitForEndOfFrame();
        //Create HP Bar
        hpbar = GCManager.Instantiate(HPBarControl.HPBAR_GC_KEY).GetComponent<HPBarControl>();
        hpbar.SetHitable(this);

        Hit_event += Hit;

        maxHP = HP;
    }


    private void OnEnable()
    {

    }

    private void OnDestroy()
    {
        //GCManager.Destory(HPBarControl.HPBAR_GC_KEY, hpbar.gameObject);
        Hit_event -= Hit;
    }

    public static void Hit_event_c(GameObject t, float d) //CALL THIS
    {
        if (Hit_event != null)
        {
            Hit_event(t, d);
        }
    }

    void Hit(GameObject target, float damage)
    {
        if (target == gameObject)
        {
            Debug.Log(gameObject.name + " 受到 " + damage + " 傷害");

            if (isHitable)
            {
                HP -= damage;

                //特效:
                DamagePopUpManager.instance.PopUp((int)damage, target.transform.position);

                //TODO [temp]  use layer to check add point:
                if (target.layer == LayerMask.NameToLayer("Monster"))
                {
                    ScoreManager.instance.AddScore((int)damage);
                }


                Hit_effect();
            }
            //判斷死亡
            if (HP <= 0)
            {
                if (OnKilled != null && !isDead)
                {
                    OnKilled(gameObject);
                }
                if (Die_event != null && !isDead)
                {
                    isDead = true;
                    Die_event();
                }

            }
            else
            {
                if (gotHit_event != null)
                {
                    Debug.Log("<color=green>HURT</color>");
                    gotHit_event();
                }
            }

        }
    }

    void Hit_effect()
    {
        //TODO: effect
        //effect
        ParticleEffectManager.instance.DOBlast(ParticleEffectManager.SMALL_BLAST_GC_KYE, transform.position, 0.2f);
        CameraFollow.CameraShake_c(0.01f, 0.1f, 2);
    }

    public void Heal(float amount)
    {
        HP = Mathf.Clamp(HP + amount, 0, maxHP);
        hpbar.UpdateHPBar();
    }
}
