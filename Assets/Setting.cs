using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject canvasP;
    public GameObject canvasS;
    
    

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvasP.active = !canvasP.active;
            canvasS.active = !canvasS.active;
        }

        if(canvasS.active == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
