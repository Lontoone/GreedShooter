using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    public float life_time = 1;
    //TODO: fade out color

    public TextMeshProUGUI text;

    private void OnEnable()
    {
        Invoke("DoDestory", life_time);
    }

    void DoDestory()
    {
        GCManager.Destory(DamagePopUpManager.DAMAGE_POPUP_GC_KEY, gameObject);
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }
}
