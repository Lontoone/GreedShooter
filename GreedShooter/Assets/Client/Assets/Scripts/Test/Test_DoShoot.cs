using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DoShoot : MonoBehaviour
{
    public float time = 0.05f;
    public Ammo _ammo;
    string _key = "keykey";
    public ActionController.mAction shoot;
    public ActionController actionController;


    private IEnumerator Start()
    {
        //GCManager.RegisterObject(_ammo.ammoData.GC_key, _ammo);
        yield return new WaitForFixedUpdate();
        actionController = GetComponent<ActionController>();
        shoot.action.AddListener(delegate { Shoot(); });

        while (true)
        {
            actionController.AddAction(shoot);
            yield return new WaitForSeconds(time);
        }
    }

    void Shoot()
    {
        /*
        Vector2 _pos = new Vector2(
            Random.Range(-20, 20), Random.Range(-20, 20));*/
        GameObject _obj = GCManager.Instantiate(_ammo.ammoData.GC_key);
        _obj.transform.position = transform.position;
        _obj.GetComponent<Ammo>().vec = new Vector2(0.4f, 0);
        //_obj.transform.position = _pos;

    }
}
