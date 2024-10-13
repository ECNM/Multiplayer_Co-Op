using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    public GameObject reffer;
    public TMP_Text t_Coin;
    int coin;


    void Update()
    {
        coin = reffer.GetComponentInChildren<CoinAndSoul>().countCoin;
        t_Coin.text = "X"+coin.ToString();
    }
}
