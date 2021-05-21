using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/LevelDesign")]
public class LevelDesignData : ScriptableObject
{

    public EnemySet[] set;
    public int amount = 3;
    public int all_rate
    {
        get
        {
            int res = 0;
            for (int i = 0; i < set.Length; i++)
            {
                res += set[i].rate;
            }
            return res;
        }
    }

    public int GetRateIndex(int rate)
    {
        int _c = 0;
        for (int i = 0; i < set.Length; i++)
        {
            _c += set[i].rate;
            if (rate <= _c)
            {
                return i;
            }
        }

        return set.Length - 1; //default => last one
    }
}

[System.Serializable]
public class EnemySet
{
    public GameObject enemy_prefab;

    public int rate = 10;
}
