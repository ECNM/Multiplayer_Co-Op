using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNS : MonoBehaviour
{
    public Timer setTimerSpwan;
    public SpawnSlime sp;

    // Update is called once per frame
    void Update()
    {
        if (setTimerSpwan.night)
        {
            //this.gameObject.SetActive(false);
            if (transform.name.Equals("Spawn_Boss"))
            {
                sp.enabled = true;
            }
            else
            {
                sp.enabled = false;
            }
            
        }
        else
        {
            if (transform.name.Equals("Spawn_Boss"))
            {
                sp.enabled = false;
            }
            else
            {
                sp.enabled = true;
            }
            //this.gameObject.SetActive(true);

        }
    }
}
