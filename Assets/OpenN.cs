using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenN : MonoBehaviour
{
    public Timer check;
    public GameObject set;
   

    // Update is called once per frame
    void Update()
    {
        if (check.night)
        {
            set.SetActive(true);
        }
        else
        {
            set.SetActive(false);
        }
    }
}
