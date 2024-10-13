using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionM : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animatorController;
    public float timeSet;
    float timeCount;
    public bool call;
    int action;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animatorController = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time > timeCount + timeSet)
        {
            timeCount += Time.time + timeSet;
            call = true;
        }

        if (call)
        {
           action = Action();
           Debug.Log(action);
           call = false;
        }

        Raction(action);

    }

    int Action()
    {
        return UnityEngine.Random.Range(1, 9);
    }

    void Raction(int numact)
    {
        float move = GetComponent<Monster>().moveSpeed;
        Debug.Log("move" + ((move / 100) * 90));
        if (numact >= 1 && numact <= 6)
        {
            animatorController.SetBool("Move", false);
        }
        if (numact == 7)
        {
            animatorController.SetBool("Move", true);
            transform.Translate(new Vector2(-1 * 0.2f, 0) * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(numact == 8)
        {
            animatorController.SetBool("Move", true);
            transform.Translate(new Vector2(1 * 0.2f, 0) * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = true;
        }

                

    }
}
