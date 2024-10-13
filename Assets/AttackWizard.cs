using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWizard : MonoBehaviour
{
    private Animator animator;
    public float attackSpeed;
    public float nextAttack;
    public GameObject hit;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, (GetComponent<BoxCollider2D>().size * transform.localScale), 0f);
        if (hitColliders.Length > 0)
        {
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag.Equals("Monster"))
                {
                    if (Time.time > nextAttack)
                    {
                        if(transform.position.x < hitCollider.gameObject.transform.position.x)
                        {
                            this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        }
                        else if (transform.position.x > hitCollider.gameObject.transform.position.x)
                        {
                            this.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        }
                        nextAttack = Time.time + attackSpeed;
                        AudioManager.Instance.PlaySFX("magic");
                        animator.Play("Wizard-Attack");
                        //Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
                    }

                }
                

            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster"))
        {
            if (Time.time > nextAttack)
            {
                if (transform.position.x < collision.gameObject.transform.position.x)
                {
                    this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                else if (transform.position.x > collision.gameObject.transform.position.x)
                {
                    this.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                nextAttack = Time.time + attackSpeed;
                AudioManager.Instance.PlaySFX("magic");
                animator.Play("Wizard-Attack");
                //Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            }

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster"))
        {
            if (Time.time > nextAttack)
            {
                if (transform.position.x < collision.gameObject.transform.position.x)
                {
                    this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                else if (transform.position.x > collision.gameObject.transform.position.x)
                {
                    this.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                nextAttack = Time.time + attackSpeed;
                AudioManager.Instance.PlaySFX("magic");
                animator.Play("Wizard-Attack");
                //Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            }

        }
    }
    */
    public void Attack()
    {
        GameObject hit_n = Instantiate(hit, this.transform);
        hit_n.GetComponent<BoxCollider2D>().size = hit_n.GetComponent<BoxCollider2D>().size * 5f;
        hit_n.GetComponent<AttackkcCheck>().damage = 30;
    }

   /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, (GetComponentInChildren<BoxCollider2D>().size * transform.localScale));
    }*/
}
