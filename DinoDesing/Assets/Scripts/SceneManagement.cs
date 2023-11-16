using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeScene(int loadScene)
    {
        SceneManager.LoadScene(loadScene);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
