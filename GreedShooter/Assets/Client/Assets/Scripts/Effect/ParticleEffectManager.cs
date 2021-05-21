using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    public static ParticleEffectManager instance;

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

        //register particle
        GCManager.RegisterObject(SMALL_BLAST_GC_KYE, smallBlast);
        GCManager.RegisterObject(Large_BLAST_GC_KYE, largeBlast);
        GCManager.RegisterObject(RED_CIRCLE_HINT_GC_KEY, red_circle);

    }

    public void DOBlast(string _key, Vector2 _pos, float _lifeTime)
    {
        GameObject _blast = GCManager.Instantiate(_key);
        _blast.transform.position = _pos;
        _blast.GetComponent<GC_SelfDestoryItem>()?.Setup(_key);
    }



    public const string SMALL_BLAST_GC_KYE = "SMALL_BLAST_GC_KYE";
    public const string Large_BLAST_GC_KYE = "Large_BLAST_GC_KYE";
    public const string RED_CIRCLE_HINT_GC_KEY = "RED_CIRCLE_HINT_GC_KEY";
    public GameObject smallBlast;
    public GameObject largeBlast;
    public GameObject red_circle;

}
public enum EffectType{ 
    smallBlast
}
