using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public PlayerManager playerMovement;
    public GameObject[] popUps;
    private int popUpIndex;
    //public GameObject spawner;
    //public float waitTime=2f;


    void Start()
    {
        //playerMovement.jumpForce=0;
    }


    void Update() { 

        for (int i = 0; i<popUps.Length; i++)
            if (i== popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);
            }

        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                popUpIndex++;
            }
            else if (popUpIndex == 1)
            {
                //playerMovement.jumpForce = 25;
                if (Input.GetButton("Jump"))
                {
                    popUpIndex++;
                }

            }
            else if (popUpIndex == 2)
            {

            }
            else if (popUpIndex == 3)
            {

            }
            else if (popUpIndex == 4)
            {

            }
            else if (popUpIndex == 5)
            {

            }
            else if (popUpIndex == 6)
            {

            }
            else if (popUpIndex == 7)
            {

            }
    }
    }
}
