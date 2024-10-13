using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseController : MonoBehaviour
{
    public float numOfdefense;
    public Image[] defenses;
    public GameObject referancePlayer;

    private void Update()
    {
        numOfdefense = referancePlayer.GetComponentInChildren<PlayerControl>().currentValue;
        UpdateHealth();
    }

    void UpdateHealth()
    {

        for (int i = 0; i < defenses.Length; i++)
        {
            if (i < numOfdefense)
            {
                defenses[i].enabled = true;
            }
            else
            {
                defenses[i].enabled = false;
            }
        }
    }
}
