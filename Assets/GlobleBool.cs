using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobleBool : MonoBehaviour
{
    public GameObject[] gameop;
    public bool set;
    public int count;
    public Image im;
    public Color __alpha;
    public bool wall;
    public GameObject wallH;
    public GameObject trees;
    public bool life;
    public float timend;
    public GameObject text;
    public string textM;

    private void Start()
    {
        wall = true;
        __alpha = Color.black;
        __alpha.a = 0;
        InvokeRepeating("Call", 0f, 0.1f);
    }

    private void Update()
    {
        if (wall == true)
        {
            wallH.SetActive(true);
        }else
        {
            wallH.SetActive(false);
            trees.SetActive(false);
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        //players[1].GetComponent<PlayerControl>();
        if (players[0].GetComponent<PlayerControl>().hp <= 0 && players[1].GetComponent<PlayerControl>().hp <= 0)
        {
            life = true;
        }
        else
        {
            life = false;
        }
    }


    IEnumerator End(string texts)
    {
        __alpha.a += 0.02f;
        im.color = __alpha;
        if (__alpha.a >= 1f)
        {
            text.GetComponent<Animator>().Play("End");
            StartCoroutine(Show());
            
        }
        yield return new WaitForSeconds(0.03f);
    }

    void Call()
    {
        if (count >= 3 || life)
        {
            if(count >= 3)
            {
                GameObject.Find("SFX Source").GetComponent<AudioSource>().volume = 0;
                textM = "Winner";
                text.GetComponent<TMPro.TMP_Text>().text = "THANK YOU";
            }
            else if (life)
            {
                GameObject.Find("SFX Source").GetComponent<AudioSource>().volume = 0;
                textM = "GameOver";
                text.GetComponent<TMPro.TMP_Text>().text = "Game Over";
            }
            StartCoroutine(End(textM));
        }
    }

    IEnumerator Show()
    {
        
        yield return new WaitForSeconds(11f);
        SceneManager.LoadScene("Menu");
    }
}
