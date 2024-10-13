using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            int ran = UnityEngine.Random.Range(1, 3);
            if(ran == 1)
            {
                collision.gameObject.GetComponent<CoinAndSoul>().soul += 10;
            }else if(ran == 2)
            {
                collision.gameObject.GetComponent<CoinAndSoul>().countCoin += 30;
            }
            AudioManager.Instance.PlaySFX("pick");
            Destroy(gameObject);

        }
    }
}
