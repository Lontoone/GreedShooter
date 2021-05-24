using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class LevelManager : MonoBehaviour
{
    //manage each level 
    //public int round_counter = 3;

    public event Action<int> OnLevelCleared;

    public static LevelManager instance;

    List<GameObject> current_enemys = new List<GameObject>();

    BlackScreenEffect blackScreen;

    AudioSource audioSource;
    public GameObject boss_level_effect_prefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public TextMeshProUGUI turn_count_downtext;
    public LevelDesignData[] level_settings;

    public Collider2D levelRange;  //decide the bounds of the level

    [HideInInspector]
    public int current_lv = 0;
    int left_monster = 0;

    public float time_gap = 30;

    public AudioClip alarm_Clip , blast_clip , boss_bgm;

    public AudioSource BGM_as;

    public Vector2 randomPosInMap
    {
        get
        {
            return new Vector2(
              UnityEngine.Random.Range(levelRange.bounds.min.x, levelRange.bounds.max.x),
              UnityEngine.Random.Range(levelRange.bounds.min.y, levelRange.bounds.max.y)
              );
        }
    }
    /*
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

                current_enemys.Add(_enemy.GetComponent<BasicEnemy>());  //TODO: temp
                //Instancitate_level_data(level_settings[i], _rand_pos);
            }
            turn_count_downtext.text = "Turn left " + (level_settings.Length - i - 1).ToString(); //temp
            yield return waitForSeconds;

        }
    }
    */

    public void Start()
    {
        blackScreen = FindObjectOfType<BlackScreenEffect>();

        HitableObj.OnKilled += CheckLevelClear;
        //StartCoroutine(NextLevel());
    }
    private void OnDestroy()
    {
        HitableObj.OnKilled -= CheckLevelClear;
    }

    void CheckLevelClear(GameObject _killed)
    {
        Debug.Log("level check ");

        if (current_enemys.Remove(_killed))
        {
            left_monster--;
            if (left_monster <= 0)
            {

                if (OnLevelCleared != null)
                {
                    OnLevelCleared(current_lv);
                }
                //current_lv++;
                //StartCoroutine(NextLevel());

                //screen get smaller
                blackScreen.SetMagnitude(blackScreen.mask_magnitude * 2);

            }
        }

    }

    public void StartNextLevel()
    {
        current_lv++;
        if (current_lv == level_settings.Length - 1)
        {
            StartCoroutine(BossLevel());
        }
        else
            StartCoroutine(NextLevel());
    }
    IEnumerator NextLevel()
    {
        Debug.Log("current level " + current_lv + " " + level_settings[current_lv].name);
        left_monster = level_settings[current_lv].amount;
        for (int j = 0; j < level_settings[current_lv].amount; j++)
        {
            Vector2 _rand_pos = new Vector2(
                UnityEngine.Random.Range(levelRange.bounds.min.x, levelRange.bounds.max.x),
                UnityEngine.Random.Range(levelRange.bounds.min.y, levelRange.bounds.max.y)
                );

            GameObject hint = GCManager.Instantiate(ParticleEffectManager.RED_CIRCLE_HINT_GC_KEY);
            hint.transform.position = _rand_pos;

            yield return new WaitForSeconds(1);

            int rand_rate = UnityEngine.Random.Range(0, level_settings[current_lv].all_rate);
            GameObject _enemy = Instantiate(level_settings[current_lv].set[level_settings[current_lv].GetRateIndex(rand_rate)].enemy_prefab,
                                            _rand_pos,

                                            Quaternion.identity);

            current_enemys.Add(_enemy);  //TODO: temp
                                         //Instancitate_level_data(level_settings[i], _rand_pos);
        }
        turn_count_downtext.text = "Turn left " + (level_settings.Length - current_lv - 1).ToString(); //temp

    }

    IEnumerator BossLevel()
    {
        BGM_as.Stop();
        WaitForSeconds _wait = new WaitForSeconds(1);
        int t = 0;
        //play alarm Sound
        while (t < 3)
        {
            t++;
            audioSource.PlayOneShot(alarm_Clip);
            yield return _wait;
        }

        audioSource.PlayOneShot(blast_clip);
        BGM_as.clip = boss_bgm;
        BGM_as.Play();

        GameObject _effect = Instantiate(boss_level_effect_prefab, Vector2.zero, Quaternion.identity);
        GameObject _boss = Instantiate(level_settings[current_lv].set[0].enemy_prefab, Vector2.zero, Quaternion.identity);
        turn_count_downtext.text = "BOSS";



    }
}
