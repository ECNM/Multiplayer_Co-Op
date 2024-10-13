using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GlobleBool glo;
    public Transform refer;
    bool sett;
    public bool set;

    private void Start()
    {
        glo = GameObject.Find("Managers").GetComponent<GlobleBool>();
        refer = GameObject.Find("SetPoint").GetComponent<Transform>();

        InvokeRepeating("Condition", 0f, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        glo = GameObject.Find("Managers").GetComponent<GlobleBool>();
        refer = GameObject.Find("SetPoint").GetComponent<Transform>();
        
    }

    IEnumerator Set()
    {
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Animator>().Rebind();
        gameObject.GetComponent<Animator>().Update(0f);
        gameObject.transform.position = refer.transform.position;
        gameObject.GetComponent<PlayerControl>().hp = gameObject.GetComponent<PlayerControl>().numOfhearts;
        AudioManager.Instance.PlaySFX("restart");
        sett = true;


    }

    void Condition()
    {
        if (gameObject.GetComponent<PlayerControl>().hp <= 0 && glo.life == false && set == false)
        {
            set = true;
            StartCoroutine(Set());
        }
        if (gameObject.GetComponent<PlayerControl>().hp !<= 0)
        {
            sett = false;
        }
        else if(sett)
        {
            set = false;
        }
    }
}
