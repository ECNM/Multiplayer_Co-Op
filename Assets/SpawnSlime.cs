using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlime : MonoBehaviour
{
    public GameObject monsters;
    public int setSpwan;
    public Timer setTimerSpwan;
    [SerializeField] private float setTimeSpawn;
    [SerializeField] private float setDuration;

    void Update()
    {
        int check = transform.childCount;
        if (setTimerSpwan.night || transform.name.Equals("Spawn_Boss"))
        {
            //this.gameObject.SetActive(false);
            GetComponent<SpawnSlime>().enabled = false;
        }
        else
        {
            //this.gameObject.SetActive(true);
            GetComponent<SpawnSlime>().enabled = true;
        }

        if (Time.time > setTimeSpawn && check < setSpwan)
        {
            GameObject monster = Instantiate(monsters, transform);
            monster.transform.position = transform.position;
            setTimeSpawn = Time.time + setDuration;
        }
        
    }
}
