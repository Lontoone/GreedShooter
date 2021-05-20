using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic enemy class
public class BasicEnemy : MonoBehaviour
{
    public ActionController.mAction Idle_act, move_act, shoot_act, hurt_act, die_act;
    HitableObj hitableObj;
    ActionController actionController;

    public LayerMask targetLayer;
    public float damage;
    public float speed;

    public Ammo ammo_prefab;
    public Weapon weapon;

    Transform move_goal;
    SpriteRenderer sp;

    public float random_move_radious = 1.5f;

    //Detect Enemy
    public LayerMask detect_layermask;
    public Collider2D detect_collider;
    ContactFilter2D filter = new ContactFilter2D();
    Collider2D[] detect_result = new Collider2D[5];

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        actionController = GetComponent<ActionController>();
        hitableObj = GetComponent<HitableObj>();

        filter.SetLayerMask(detect_layermask);
        filter.useTriggers = true;

        actionController.eOnActionComplete += SetDefaultAction;
        actionController.eActionQueueCleared += delegate { actionController.AddAction(Idle_act); };

        move_act.action.AddListener(delegate { Move(); });


        //First default action
        actionController.AddAction(Idle_act);

        shoot_act.gap_time = weapon.weaponData.shoot_gap_time;

        //generate move goal
        move_goal = new GameObject().transform;
    }
    private void OnDestroy()
    {
        actionController.eOnActionComplete -= SetDefaultAction;
        actionController.eActionQueueCleared -= delegate { actionController.AddAction(Idle_act); };
    }


    private void FixedUpdate()
    {
        if (weapon == null || ammo_prefab == null)
            return;
        //Shoot
        if (GetTargetInDetectRange())
        {
            actionController.AddAction(shoot_act);
        }
    }

    void SetDefaultAction(ActionController.mAction _previousFinished_action)
    {
        Debug.Log(_previousFinished_action);
        //if target in rnage => chase it
        if (GetTargetInDetectRange())
        {
            move_goal.position = detect_result[0].gameObject.transform.position;

            actionController.AddAction(move_act);
        }
        else
        {
            //walk randomly (set move goal to random point) or Idle
            int rand = Random.Range(0, 100);
            if (rand < 50)
            {
                actionController.AddAction(Idle_act);
                return;
            }

            //TODO: 改用方向?
            Vector2 _goal = new Vector2(
                Random.Range(-random_move_radious, random_move_radious),
                Random.Range(-random_move_radious, random_move_radious));
            move_goal.position = _goal + (Vector2)transform.position;

            actionController.AddAction(move_act);
        }

    }
    bool GetTargetInDetectRange()
    {
        int _len = detect_collider.OverlapCollider(filter, detect_result);
        if (_len > 0)
            return true;
        else
            return false;
    }
    public void Move()
    {
        Debug.Log("enemy moving");
        Vector2 _dir = (move_goal.position - transform.position).normalized;
        transform.position = (Vector2)transform.position + _dir * speed * Time.deltaTime;

        //turn
        if (_dir.x > 0)
        {
            //sp.flipX =true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            //sp.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    public void Shoot()
    {
        Debug.Log("enemy shoot");
        //weapon.Shoot(move_goal.position,
        weapon.Shoot((move_goal.position - transform.position),
                    ammo_prefab.ammoData,
                    targetLayer);
        //Ammo ammo = GCManager.Instantiate(ammo_prefab.ammoData.GC_key).GetComponent<Ammo>();
        //
        //ammo.transform.position = transform.position; //TODO: add shoot point
        //ammo.dir = (move_goal.position - transform.position).normalized;
        //ammo.targetLayer = targetLayer;
    }

    //TODO: dash
    //TODO: Hurt/Die

    void Die()
    {

    }
}
