using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    public GameObject[] player;
    int index;
    public int hp;
    private int setHp;
    public GameObject coin;
    public GameObject fire;
    public GameObject heart;
    bool isCreated = false;
    [SerializeField] private int luckSoul;
    [SerializeField] private int luckHeart;

    public Animator animatorController;

    private void Start()
    {
        animatorController = gameObject.GetComponent<Animator>();
        setHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        index = FindIndex(player);
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, (GetComponentsInChildren<BoxCollider2D>()[1].size * transform.localScale), 0f);
        if (hitColliders.Length > 0)
        {
            foreach (Collider2D hitCollider in hitColliders)
            {
                //Debug.Log("Collision with: " + hitCollider.tag);
                //Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
                Debug.Log("Monster" + hitCollider.tag);
                if (hitCollider.tag.Equals("Player"))
                {
                    GetComponent<EnemyAI>().enabled = true;
                    GetComponent<EnemyAI>().target = player[index].transform;
                }

            }
        }

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
    }
                
                

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, (GetComponentsInChildren<BoxCollider2D>()[1].size * transform.localScale));
    }
}
