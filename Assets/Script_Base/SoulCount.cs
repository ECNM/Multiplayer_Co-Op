using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulCount : MonoBehaviour
{
    public GameObject reffer;
    public TMP_Text t_soul;
    int soul;


    void Update()
    {
        soul = reffer.GetComponentInChildren<CoinAndSoul>().soul;
        t_soul.text = "X" + soul.ToString();
    }
}
