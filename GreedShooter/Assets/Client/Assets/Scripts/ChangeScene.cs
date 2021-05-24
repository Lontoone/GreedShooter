using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void Change(string _scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_scene);

        GCManager.Clear();
    }
}
