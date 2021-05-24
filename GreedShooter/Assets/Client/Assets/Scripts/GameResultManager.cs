using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultManager : MonoBehaviour
{
    public static GameResultManager instance;

    public GameObject win_panel;
    public GameObject lose_panel;

    public void Awake()
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
    public void SetResult(bool _isWin)
    {
        Cursor.visible = true;
        if (_isWin)
        {
            win_panel.SetActive(true);
        }
        else
        {
            lose_panel.SetActive(true);
        }
    }

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
}
