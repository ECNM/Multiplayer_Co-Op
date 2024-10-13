using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAndSoul : MonoBehaviour
{
    public int countCoin;
    public int soul;

    private void Update()
    {
        if(countCoin  >= 100)
        {
            countCoin = 100;
        }
        if(soul >= 100)
        {
            soul = 100;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Coin"))
        {
            AudioManager.Instance.PlaySFX("pick");
            Destroy(collision.gameObject);
            countCoin++;
        }
        else if (collision.gameObject.tag.Equals("Soul"))
        {
            AudioManager.Instance.PlaySFX("pick");
            Destroy(collision.gameObject);
            soul++;
        }
        else if (collision.gameObject.tag.Equals("heart"))
        {
            AudioManager.Instance.PlaySFX("pick");
            Destroy(collision.gameObject);
            GetComponent<PlayerControl>().hp += 0.5f;
        }
    }
}
