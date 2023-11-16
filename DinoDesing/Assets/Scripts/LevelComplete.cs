using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
   public GameObject CompleteLevelUI;
   public void CompleteLevel()
    {
        CompleteLevelUI.SetActive(true);
    }

}
