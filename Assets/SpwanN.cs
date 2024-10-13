using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanN : MonoBehaviour
{
    public GameObject[] monsters;
    public int setSpwan;
    public Timer setTimerSpwan;
    public int numSpwan;
    [SerializeField] private float setTimeSpawn;
    [SerializeField] private float setDuration;
    bool gettime;

    void Update()
    {
        int check = transform.childCount;
        gettime = setTimerSpwan.night;


        if (GetComponentsInParent<Transform>()[1].name.Equals("Crystal") || GetComponentsInParent<Transform>()[1].name.Equals("Swamp"))
        {
            if (Time.time > setTimeSpawn && check < setSpwan)
            {
                int rand = UnityEngine.Random.Range(0, numSpwan);
                GameObject monster = Instantiate(monsters[rand], transform);
                monster.transform.position = transform.position;
                setTimeSpawn = Time.time + setDuration;
            }
        }
        else if(Time.time > setTimeSpawn && check < setSpwan && setTimerSpwan.night)
        {
            int rand = UnityEngine.Random.Range(0, numSpwan);
            GameObject monster = Instantiate(monsters[rand], transform);
            monster.transform.position = transform.position;
            setTimeSpawn = Time.time + setDuration;
        }

    }
}
