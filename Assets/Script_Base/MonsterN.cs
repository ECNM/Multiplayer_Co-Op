using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterN : MonoBehaviour
{
    public int hp;
    public float damage;
    private int setHp;

    public GameObject[] player;
    public float distance;

    public float moveSpeed = 0.5f;
    public float attackSpeed;
    public float nextAttack;
    private Rigidbody2D rb;
    public Animator animatorController;
    public bool setAttack;
    public float radius;
    public GameObject coin;
    public GameObject fire;
    public GameObject heart;
    bool isCreated = false;
    [SerializeField] private int luckSoul;
    [SerializeField] private int luckHeart;
    int index;



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animatorController = gameObject.GetComponent<Animator>();
        setHp = hp;
        //player = gameObject.AddComponent<Transform>();

    }

    void Update()
    {
        //Debug.Log(animatorController.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        player = GameObject.FindGameObjectsWithTag("Player");
        index = FindIndex(player);
        //Debug.Log(FindIndex(player));
        if (hp <= 0)
        {
            animatorController.Play("Die");
            if (!isCreated)
            {
                GameObject game = Instantiate(coin);
                game.transform.position = this.transform.position;
                int random = UnityEngine.Random.Range(1, 10);
                int random2 = UnityEngine.Random.Range(1, 10);
                //Debug.Log("Random"+random);
                if (random > luckSoul)
                {
                    Instantiate(fire).transform.position = transform.position;
                }
                if (random2 > luckHeart)
                {
                    Instantiate(heart).transform.position = transform.position;
                }
                else
                {
                    isCreated = true;
                    return;
                }

                isCreated = true;
            }

            Destroy(this.gameObject, 0.6f);
        }

        if (hp != setHp && hp != 0)
        {

            if (Vector2.Distance(player[index].transform.position, transform.position) > distance)
            {
                animatorController.SetBool("Move", true);
                if (player[index].transform.position.x < transform.position.x)
                {
                    //rb.AddForce(new Vector2(-1 * moveSpeed , 0), ForceMode2D.Impulse);
                    transform.Translate(new Vector2(-1 * moveSpeed, 0) * Time.deltaTime);
                    GetComponent<SpriteRenderer>().flipX = false;
                    //transform.eulerAngles = new Vector2(0, 0);

                }
                else
                {
                    //rb.AddForce(new Vector2(1 * moveSpeed, 0), ForceMode2D.Impulse);
                    GetComponent<SpriteRenderer>().flipX = true;
                    transform.Translate(new Vector2(1 * moveSpeed, 0) * Time.deltaTime);
                    //transform.eulerAngles = new Vector2(0, 180);
                }
            }
            else
            {
                animatorController.SetBool("Move", false);
                //Debug.Log("No");
                if (Time.time > nextAttack)
                {
                    nextAttack = Time.time + attackSpeed;
                    animatorController.SetTrigger("Attack");
                    setAttack = true;
                    //Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));


                }

            }
        }
    }

    public void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        if (hitColliders.Length > 0)
        {
            foreach (Collider2D hitCollider in hitColliders)
            {
                //Debug.Log("Collision with: " + hitCollider.tag);
                //Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
                if (hitCollider.tag.Equals("Player") && setAttack)
                {


                    Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
                    //Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
                    //animatorController.SetTrigger("Attack");
                    //Debug.Log("mon_at");
                    Rigidbody2D rb = hitCollider.gameObject.GetComponent<Rigidbody2D>();
                    Animator animator = hitCollider.gameObject.GetComponent<Animator>();

                    if (hitCollider.gameObject.GetComponent<PlayerControl>().isHolding)
                    {
                        return;
                    }
                    else
                    {

                        animator.Play("Hurt");
                        hitCollider.GetComponent<PlayerControl>().hp -= damage;
                        Debug.Log("test");


                        if (hitCollider.transform.parent.name.Equals("Player_One"))
                        {
                            Debug.Log("test");
                            StartCoroutine(GameObject.Find("Camera_PlayerOne").GetComponent<CameraShake>().Shake(0.15f, .1f));
                        }
                        else if (hitCollider.transform.parent.name.Equals("Player_Two"))
                        {
                            Debug.Log("test");
                            StartCoroutine(GameObject.Find("Camera_PlayerTwo").GetComponent<CameraShake>().Shake(0.15f, .1f));
                        }
                        //player = GameObject.FindGameObjectsWithTag("Player");
                        //int index = FindIndex(player);
                        //float distance_one = Vector2.Distance(player[0].transform.position, transform.position); 
                        //float distance_two = Vector2.Distance(player[1].transform.position, transform.position);
                        Debug.Log(player[index].name);

                        if (player[index].transform.position.x < transform.position.x)
                        {
                            rb.velocity = new Vector2(-1, 1) * 1.3f;
                        }
                        else
                        {
                            rb.velocity = new Vector2(1, 1) * 1.3f;
                        }
                        setAttack = false;
                    }

                }
            }
        }
    }
    public void SetCollider()
    {
        GetComponent<BoxCollider2D>().size = -(GetComponent<BoxCollider2D>().size);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        //nextAttack = Time.time + attackSpeed;
        if (collision.gameObject.tag.Equals("Player") && setAttack && animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            //animatorController.SetTrigger("Attack");
            //Debug.Log("mon_at");
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Animator animator = collision.gameObject.GetComponent<Animator>();

            animator.Play("Hurt");
            //player = GameObject.FindGameObjectsWithTag("Player");
            int index = FindIndex(player);
            //float distance_one = Vector2.Distance(player[0].transform.position, transform.position); 
            //float distance_two = Vector2.Distance(player[1].transform.position, transform.position);
            Debug.Log(player[index].name);

            if (player[index].transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-1, 1) * 3;
            }
            else
            {
                rb.velocity = new Vector2(1, 1) * 3;
            }
        }
        setAttack = false;
    }*/

    //ScanPlayernearly
    private int FindIndex(GameObject[] player)
    {
        if (player == null || player.Length == 0)
        {
            Debug.LogError("The array is empty or null");
            return int.MaxValue;
        }

        float minValue = Vector2.Distance(player[0].transform.position, transform.position);
        int index = 0;
        for (int i = 1; i < player.Length; i++)
        {
            if (Vector2.Distance(player[i].transform.position, transform.position) < minValue)
            {
                minValue = Vector2.Distance(player[i].transform.position, transform.position);
                index = i;
            }

        }

        return index;
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collided_Stay");
        }
        //nextAttack = Time.time + attackSpeed;
        if (collision.gameObject.tag.Equals("Player") && setAttack && animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //animatorController.SetTrigger("Attack");
            //Debug.Log("mon_at");
            Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Animator animator = collision.gameObject.GetComponent<Animator>();

            animator.Play("Hurt");
            //player = GameObject.FindGameObjectsWithTag("Player");
            int index = FindIndex(player);

            if (player[index].transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-0.5f, 0.5f) * 3;
            }
            else
            {
                rb.velocity = new Vector2(0.5f, 0.5f) * 3;
            }

            setAttack = false;
        }

    }*/

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }






}
