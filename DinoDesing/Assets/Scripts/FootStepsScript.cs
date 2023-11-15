using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsScript : MonoBehaviour
{
   
    public GameObject footstep;

    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            footsteps();
        }
       else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) {

            footstepsStop();
        }

    }

    void footsteps()
    {
        footstep.SetActive(true);
    }
    void footstepsStop()
    {
        footstep.SetActive(false);
    }

}