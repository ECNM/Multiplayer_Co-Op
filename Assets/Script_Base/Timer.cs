using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Timer : MonoBehaviour
{
    public Light2D light;
    public bool night;
    public bool loop;
    float timecount = 0;
    float numSave;

    public Light2D[] player;

    private void Start()
    {
        numSave = 0.14f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timecount+1)
        {
            timecount = Time.time;
            if(light.intensity >= numSave && loop)
            {
                light.intensity += 0.006f;
                if (light.intensity >= 1.2f)
                {
                    loop = false;
                }
            }
            else if(!loop)
            {
                light.intensity -= 0.006f;
                if (light.intensity <= 0.14f)
                {
                    numSave = light.intensity;
                    loop = true;
                }
            }
            
        }

        if (light.intensity >= numSave && light.intensity <= 0.50f)
        {
            night = true;
        }
        else
        {
            night = false;
        }
        player[0] = GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<Light2D>();
        player[1] = GameObject.FindGameObjectsWithTag("Player")[1].GetComponentInChildren<Light2D>();
        if (night)
        {
            player[0].enabled = true;
            player[1].enabled = true;
        }
        else
        {
            player[0].enabled = false;
            player[1].enabled = false;
        }
    }
}
