using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpManager : MonoBehaviour
{
    public static DamagePopUpManager instance;
    public GameObject damage_popup_prefab;
    public const string DAMAGE_POPUP_GC_KEY = "DAMAGE_POPUP_GC_KEY";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GCManager.RegisterObject(DAMAGE_POPUP_GC_KEY, damage_popup_prefab);
    }

    public void PopUp(int damage, Vector2 _position)
    {
        PopUpText _text = GCManager.Instantiate(DAMAGE_POPUP_GC_KEY).GetComponentInChildren<PopUpText>();
        _text.SetText(damage.ToString());
        _text.transform.position = _position;

    }


}
