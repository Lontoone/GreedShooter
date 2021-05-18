using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    //manage each level 
    //public int round_counter = 3;
    public TextMeshProUGUI turn_count_downtext;
    public LevelDesignData[] level_settings;

    public Collider2D levelRange;  //decide the bounds of the level

    int current_lv = 1;

    public float time_gap = 30;

    private IEnumerator Start()
    {
        //Turn
        WaitForSeconds waitForSeconds = new WaitForSeconds(time_gap);
        for (int i = 0; i < level_settings.Length; i++)
        {
            Instancitate_level_data(level_settings[i]);
            turn_count_downtext.text = "Turn left " + (level_settings.Length - i - 1).ToString(); //temp
            yield return waitForSeconds;

        }
    }

    public void Instancitate_level_data(LevelDesignData _data)
    {
        //randly create enemy obj
        for (int i = 0; i < _data.amount; i++)
        {
            Vector2 _rand_pos = new Vector2(
                Random.Range(levelRange.bounds.min.x, levelRange.bounds.max.x),
                Random.Range(levelRange.bounds.min.y, levelRange.bounds.max.y)
                );

            //draw a enemy:
            int rand_rate = Random.Range(0, _data.all_rate);

            GameObject _enemy = Instantiate(_data.set[_data.GetRateIndex(rand_rate)].enemy_prefab, _rand_pos, Quaternion.identity);
            //TODO: birth animation


        }
    }



}
