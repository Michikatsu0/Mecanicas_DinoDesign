using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject CompleteLevelUI;
    private void OnTriggerEnter2D()
    {
        CompleteLevelUI.SetActive(true);
    }
}
