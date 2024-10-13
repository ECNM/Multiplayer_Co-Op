using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    private PlayerInput PlayerInput;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        float Move = PlayerInput.actions["Move"].ReadValue<Vector2>().x;
        bool Jump = PlayerInput.actions["Jump"].WasPerformedThisFrame();

        if (Move != 0)
        {
            //rigidbody2d.velocity = new Vector2(Move * moveSpeed, rigidbody2d.velocity.y);
            transform.Translate(PlayerInput.actions["Move"].ReadValue<Vector2>() * 2 * Time.deltaTime);

            if (Move < 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            //rigidbody2d.velocity = new Vector2(Move * moveSpeed, rigidbody2d.velocity.y);
        }
    }
}
