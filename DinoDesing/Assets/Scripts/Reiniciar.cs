using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reiniciar : MonoBehaviour
{
    public void LoadScene2()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
