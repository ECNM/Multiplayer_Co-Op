using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUp : MonoBehaviour
{
    public int upValue;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("test"))
        {
            AudioManager.Instance.PlaySFX("Up");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 1f) * upValue;
        }
    }
}
