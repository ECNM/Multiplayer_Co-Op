using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    public float health;
    public int numOfhearts;
    

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public GameObject referancePlayer;

    private void Update()
    {
        health = referancePlayer.GetComponentInChildren<PlayerControl>().hp;
        numOfhearts = referancePlayer.GetComponentInChildren<PlayerControl>().numOfhearts;
        UpdateHealth();
    }
    void UpdateHealth()
    {

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < numOfhearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

            if (i < (int)health)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i < health)
            {
                hearts[i].sprite = halfHeart;
                hearts[i].transform.localScale = new Vector3(-0.2844611f, 0.2844611f, 0.2844611f);
                
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

}
