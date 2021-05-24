using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI point_text;
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
    }

    int score = 0;
    public void AddScore(int _add)
    {
        //TODO:screo added Effect
        score += _add;
        point_text.text = score.ToString();
    }

    public bool UsePoint(int _cost)
    {
        if (_cost > score)
        {
            return false;
        }
        else {
            score -= _cost;
            point_text.text = score.ToString();
            return true;
        }
    }




}
