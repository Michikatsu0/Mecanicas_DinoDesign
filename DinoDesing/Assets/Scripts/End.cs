using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject CompleteLevelUI;
    public NubeDeadNuke nubeDeadNuke;
    public PlayerManager playerManager;
    private void OnTriggerEnter2D()
    {
        CompleteLevelUI.SetActive(true);
        nubeDeadNuke.deadScript = true;
        playerManager.deadScript = true;
    }
}
