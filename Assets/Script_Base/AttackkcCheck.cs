using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackkcCheck : MonoBehaviour
{
    private float starttime;
    public int damage;
    Vector2 frontVector;
    public PlayerControl py;

    private void Awake()
    {
        starttime = Time.time;
        //damage = 10;
    }

    private void Start()
    {
        if (GetComponentsInParent<Transform>()[1].tag.Equals("Player"))
        {
            py = GetComponentInParent<PlayerControl>();
            damage = py.damageP;
        }
        else
        {
            damage = 30;
            py = new PlayerControl();
            py.damageP = 30;
        }
        
    }

    private void Update()
    {
        damage = py.damageP;
        if (Time.time - starttime >= 0.15)
        {
            Destroy(this.gameObject);
        }
        frontVector = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.up;
        Debug.DrawRay(transform.position, frontVector, Color.red);
        Debug.Log("Front Vector: " + frontVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster"))
        {
            
            bool isCriticalHit = Random.Range(0, 100) < 30;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            if (isCriticalHit)
            {
                damage += damage * 1;
            }
            else
            {
                damage = py.damageP;
            }
            DamagePopup.Create(collision.gameObject.transform.position, damage, isCriticalHit);
            if (collision.GetComponent<Monster>())
            {
                collision.GetComponent<Monster>().hp -= damage;
            }
            else
            {
                collision.GetComponent<EnemyAttackAI>().hp -= damage;
            }
            AudioManager.Instance.PlaySFX("Mtakehit");
            collision.GetComponent<Animator>().Play("Hurt");
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponentsInParent<Transform>()[1].transform.localScale.x, 0.5f) * 2;
            //collision.GetComponent<Monster>().player = GetComponentInParent<Transform>();
            Debug.Log("Successful Attack"+ GetComponentInParent<Transform>().transform.localScale.x);
        }
    }
}
