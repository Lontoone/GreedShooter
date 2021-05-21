using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    //manage each level 
    //public int round_counter = 3;

    public GameObject _hint_prefab;

    public static LevelManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public TextMeshProUGUI turn_count_downtext;
    public LevelDesignData[] level_settings;

    public Collider2D levelRange;  //decide the bounds of the level

    int current_lv = 1;

    public float time_gap = 30;

    public Vector2 randomPosInMap
    {
        get
        {
            return new Vector2(
              Random.Range(levelRange.bounds.min.x, levelRange.bounds.max.x),
              Random.Range(levelRange.bounds.min.y, levelRange.bounds.max.y)
              );
        }
    }

    private IEnumerator Start()
    {
        //Turn
        WaitForSeconds waitForSeconds = new WaitForSeconds(time_gap);
        for (int i = 0; i < level_settings.Length; i++)
        {
            for (int j = 0; j < level_settings[i].amount; j++)
            {
                Vector2 _rand_pos = new Vector2(
                    Random.Range(levelRange.bounds.min.x, levelRange.bounds.max.x),
                    Random.Range(levelRange.bounds.min.y, levelRange.bounds.max.y)
                    );

                GameObject hint = GCManager.Instantiate(ParticleEffectManager.RED_CIRCLE_HINT_GC_KEY);
                hint.transform.position = _rand_pos;

                yield return new WaitForSeconds(1);

                int rand_rate = Random.Range(0, level_settings[i].all_rate);
                GameObject _enemy = Instantiate(level_settings[i].set[level_settings[i].GetRateIndex(rand_rate)].enemy_prefab,
                                                _rand_pos,

                                                Quaternion.identity);
                //Instancitate_level_data(level_settings[i], _rand_pos);
            }
            turn_count_downtext.text = "Turn left " + (level_settings.Length - i - 1).ToString(); //temp
            yield return waitForSeconds;

        }
    }

    public void Instancitate_level_data(LevelDesignData _data, Vector2 _pos)
    {
        //randly create enemy obj
        for (int i = 0; i < _data.amount; i++)
        {

            //draw a enemy:
            int rand_rate = Random.Range(0, _data.all_rate);

            GameObject _enemy = Instantiate(_data.set[_data.GetRateIndex(rand_rate)].enemy_prefab, _pos, Quaternion.identity);
            //TODO: birth animation


        }
    }



}
