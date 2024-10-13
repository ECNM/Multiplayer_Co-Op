using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public float moveSpeed = 0.3f;  
    private float direction = 1f; 
    private bool isWalking = false;
    public Animator animatorController;

    void Start()
    {
        animatorController = gameObject.GetComponent<Animator>();
        StartCoroutine(ChangeBehavior());
    }

    void Update()
    {
        if (isWalking)
        {
            transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator ChangeBehavior()
    {
        while (true)
        {
            float randomTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(randomTime);

            int randomBehavior = Random.Range(0, 3);

            if (randomBehavior == 0)
            {
                isWalking = false;
                    animatorController.SetBool("Move", false);
                Debug.Log("Monster is standing still");
            }
            else if (randomBehavior == 1)
            {
                direction = -1f;
                isWalking = true;
                animatorController.SetBool("Move", true);
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                Debug.Log("Monster is walking left");
            }
            else if (randomBehavior == 2)
            {
                direction = 1f;
                isWalking = true;
                animatorController.SetBool("Move", true);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                Debug.Log("Monster is walking right");
            }
        }
    }
}
