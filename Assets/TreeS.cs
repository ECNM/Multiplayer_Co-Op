using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeS : MonoBehaviour
{
    public int hp = 1000;
    public GlobleBool manage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<AttackkcCheck>())
        {
            GetComponentInChildren<ParticleSystem>().Play();
            hp -= collision.gameObject.GetComponent<AttackkcCheck>().damage;
            AudioManager.Instance.PlaySFX("Mtakehit");
        }
    }

    private void Update()
    {
        if (hp <= 0)
        {
            manage.wall = false;
        }
    }
}
