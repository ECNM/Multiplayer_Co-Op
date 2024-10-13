using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;


public class PlayerControl : MonoBehaviour
{
    //private PlayerControllers playerControl;
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int JumpHigh;
    private Player player;
    private PlayerInput PlayerInput;
    public float JumpRate;
    public float NextJumpPress;
    private Collider2D playerCollider;
    public LayerMask layer;
    Vector2 moveDirection;
    public float scal;
    [SerializeField] private bool ground;
    private bool isPlayingAnimation;
    public GameObject hit;
    public bool isHolding = false;
    public bool isJump;

    private int count;
    private float timeCount;
    AnimatorStateInfo stateInfo;

    private float attackRate;

    private int comboStep;
    private float lastAttackTime;
    public float comboDelay = 0.3f;

    int currentCount;
    public float hp;
    public int numOfhearts;
    public float setTimeHp;
    public int damageP;
    public float reMana;

    public float decreaseRate = 1; 
    public float currentValue = 11;

    private float setTime;

    public float virVelocity;

    private void Awake()
    {
        //playerControl = new PlayerControllers();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        //inputActions = new PlayerControllers();
        player = GetComponentInParent<Player>();
        PlayerInput = GetComponent<PlayerInput>();
        playerCollider = GetComponent<Collider2D>();
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    private void Update()
    {
        //Debug.Log(ground);
        if (player.player == Player.PlayerType.Player1)
        {
            Player_One();
            
        }
        else
        {
            Player_Two();

        }

        if (hp <= 0)
        {
            hp = 0;
            animator.Play("Die");
        }
        if (currentValue < 0)
        {
            currentValue = 0;
            isHolding = false;
        }

        SetDefense();
        SetHealth();
        



    }

    private void Player_One()
    {
        float Move = PlayerInput.actions["Move"].ReadValue<Vector2>().x;
        bool Jump = PlayerInput.actions["Jump"].WasPerformedThisFrame();
        bool checkAttack = PlayerInput.actions["Attack2"].WasPerformedThisFrame();
        virVelocity = PlayerInput.actions["Move"].ReadValue<Vector2>().x * 6;

        if (Move != 0)
        {
            //rigidbody2d.velocity = new Vector2(Move * moveSpeed, rigidbody2d.velocity.y);
            transform.Translate(PlayerInput.actions["Move"].ReadValue<Vector2>() * moveSpeed * Time.deltaTime);

            if (Move < 0)
            {
                //this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                this.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                //this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                this.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            //rigidbody2d.velocity = new Vector2(Move * moveSpeed, rigidbody2d.velocity.y);
        }

        if (Jump & Time.time > NextJumpPress & ground == true)
        {
            rigidbody2d.velocity = Vector2.up * JumpHigh;
            animator.SetBool("Jump", true);

            NextJumpPress = Time.time + JumpRate;

            moveSpeed = 2.8f;
        }

        animator.SetFloat("Run", Mathf.Abs(Move));

        if (checkAttack && count <= 2)
        {
            count += 1;
            //Debug.Log("Checked");
            timeCount = Time.time + 0.3f;
            Debug.Log(count);
        }
        if (count > 2)
        {
            count = 0;
        }
        if (Time.time > timeCount)
        {
            count = 0;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            moveSpeed = 4;
        }

        if (count.Equals(1))
        {
            if (Time.time > attackRate)
            {
                moveSpeed = 0.2f;
                attackRate = Time.time + 0.5f;
                animator.Play("Attack1");
                //AudioManager.Instance.PlaySFX("attack");
                //Instantiate(hit, transform.position, transform.rotation);
                //Debug.Log("animation1");
            }
            else
            {
                return;
            }
        }



        if (count.Equals(2))
        {
            if (Time.time > attackRate)
            {
                moveSpeed = 0.2f;
                moveSpeed = 1;
                //attackRate = Time.time + 0.6f;
                animator.Play("Attack2");
                //Instantiate(hit, transform.position, transform.rotation);
                //Instantiate(hit, transform.position, transform.rotation);
                //animator.SetBool("Attack2", true);
                //Debug.Log("animation2");
            }
            else
            {
                animator.SetBool("Attack2", false);
            }
        }
    }

    private void Player_Two()
    {
        float Move = PlayerInput.actions["Move2"].ReadValue<Vector2>().x;
        bool Jump = PlayerInput.actions["Jump2"].WasPerformedThisFrame();
        bool checkAttack = PlayerInput.actions["Attack"].WasPerformedThisFrame();
        virVelocity = PlayerInput.actions["Move2"].ReadValue<Vector2>().x * 6;
        if (Move != 0)
        {
            //rigidbody2d.velocity = new Vector2(Move * moveSpeed, rigidbody2d.velocity.y);
            

            if (Move < 0)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                //this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                this.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                //this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                this.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            //rigidbody2d.velocity = new Vector2(Move * moveSpeed, rigidbody2d.velocity.y);
        }

        if (Jump & Time.time > NextJumpPress & ground == true)
        {
            rigidbody2d.velocity = Vector2.up * JumpHigh;
            animator.SetBool("Jump", true);

            NextJumpPress = Time.time + JumpRate;

            moveSpeed = 2.8f;
        }

        animator.SetFloat("Run", Mathf.Abs(Move));

        if (checkAttack && count <= 2)
        {
            count += 1;
            //Debug.Log("Checked");
            timeCount = Time.time + 0.3f;
            Debug.Log(count);
        }
        if (count > 2)
        {
            count = 0;
        }
        if (Time.time > timeCount)
        {
            count = 0;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            moveSpeed = 4;
        }

        if (count.Equals(1))
        {
            if (Time.time > attackRate)
            {
                moveSpeed = 0.2f;
                attackRate = Time.time + 0.5f;
                animator.Play("Attack1");
                //AudioManager.Instance.PlaySFX("attack");
                //Instantiate(hit, transform.position, transform.rotation);
                //Debug.Log("animation1");
            }
            else
            {
                return;
            }
        }

        if (count.Equals(2))
        {
            if (Time.time > attackRate)
            {
                moveSpeed = 0.2f;
                moveSpeed = 1;
                //attackRate = Time.time + 0.6f;
                animator.Play("Attack2");
                //AudioManager.Instance.PlaySFX("attack2");
                //Instantiate(hit, transform.position, transform.rotation);
                //Instantiate(hit, transform.position, transform.rotation);
                //animator.SetBool("Attack2", true);
                //Debug.Log("animation2");
            }
            else
            {
                animator.SetBool("Attack2", false);
            }
        }

        /*if (count.Equals(3))
        {
            if (Time.time > attackRate)
            {
                moveSpeed = 0.2f;
                //attackRate = Time.time + 0.6f;
                animator.Play("Attack3");
                //AudioManager.Instance.PlaySFX("attack");
                //Instantiate(hit, transform.position, transform.rotation);
                //Instantiate(hit, transform.position, transform.rotation);
                //animator.SetBool("Attack2", true);
                //Debug.Log("animation2");
            }
            else
            {
                animator.SetBool("Attack2", false);
            }
        }*/
        #region Comment
        /*switch (count)
        {
            case 1:
                if (Time.time > attackRate)
                {
                    moveSpeed = 0.6f;
                    attackRate = Time.time + 0.5f;
                    animator.Play("Attack1");
                    Instantiate(hit, transform.position,transform.rotation);
                    //Debug.Log("animation1");
                }
                else
                {
                    return;
                }
                break;
            case 2:
                if (Time.time > attackRate)
                {
                    moveSpeed = 1;
                    //attackRate = Time.time + 0.6f;
                   animator.Play("Attack2");
                    //Instantiate(hit, transform.position, transform.rotation);
                    //animator.SetBool("Attack2", true);
                    //Debug.Log("animation2");
                }
                else
                {
                    animator.SetBool("Attack2", false);
                }
                //animator.Play("Attack2");
               // Debug.Log(count);
                break;
            case 3:
               // Debug.Log(count);
                break;
            default: 
               // Debug.Log("Nothing");
                break;
        }*/



        //animator.SetBool("Attack1", false);
        #endregion
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player" || collision.gameObject.tag.Equals("Monster") && ground == false)
        {
            ground = true;
            animator.SetBool("Jump", false);
            moveSpeed = 4;
            isJump = false;
            //Debug.Log(collision.gameObject.tag);
            //Debug.Log(ground);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag == "Player" || collision.gameObject.tag.Equals("Monster") && ground)
        {
            ground = false;
            isJump = true;
            //Debug.Log(ground);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player" || collision.gameObject.tag.Equals("Monster") && ground == false)
        {
            ground = true;
            isJump = false;
            //Debug.Log("Ground");
        }
    }

    private void Hit()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            GameObject hit_n = Instantiate(hit, this.transform);
            hit_n.GetComponent<AttackkcCheck>().damage = hit_n.GetComponent<AttackkcCheck>().damage*2;
        }
        else
        {
            Instantiate(hit, this.transform);
        }
        //Debug.Log("Hit");
        //Debug.Log(transform.localScale.x);
    }

    private void ResetCombo()
    {
        count = 0;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, rb.velocity.y); 
        Debug.Log("Reset");
    }





    /*public void OnMove(InputAction.CallbackContext context)
    {
        if (player.player == Player.PlayerType.Player1)
        {
            if (context.performed)
            {
                animator.SetBool("Run", true);

                rigidbody2d.velocity = context.ReadValue<Vector2>() * moveSpeed;

                if (context.ReadValue<Vector2>().x < 0)
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
                animator.SetBool("Run", false);
                rigidbody2d.velocity = Vector2.zero * moveSpeed;
            }

        }
        else
        {
            Debug.Log("Player_TWO");
        }
        
    }

    public void Move2(InputAction.CallbackContext context)
    {
        if (player.player == Player.PlayerType.Player2)
        {
            if (context.performed)
            {
                animator.SetBool("Run", true);

                rigidbody2d.velocity = context.ReadValue<Vector2>() * moveSpeed;
                Debug.Log(context.ReadValue<Vector2>());

                if (context.ReadValue<Vector2>().x < 0)
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
                animator.SetBool("Run", false);
                rigidbody2d.velocity = Vector2.zero * moveSpeed;
            }

        }
        else
        {
            Debug.Log("Player_ONE");
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (player.player == Player.PlayerType.Player1)
        {
            if (context.performed)
            {
                Debug.Log("test");
                animator.SetBool("Jump", true);

                rigidbody2d.velocity = Vector2.up * JumpHigh;

                /*if (context.ReadValue<Vector2>().x < 0)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
        else
        {
            Debug.Log("Player_TWO");
        }
    }*/

    private void OnEnable()
    {
        if(player.player == Player.PlayerType.Player2)
        {
            PlayerInput.actions["defense"].performed += OnActionPerformed;
            PlayerInput.actions["defense"].canceled += OnActionCanceled;
        }
        else
        {
            PlayerInput.actions["defense2"].performed += OnActionPerformed;
            PlayerInput.actions["defense2"].canceled += OnActionCanceled;
        }
        
    }

    private void OnDisable()
    {
        if(player.player == Player.PlayerType.Player2)
        {
            PlayerInput.actions["defense"].performed -= OnActionPerformed;
            PlayerInput.actions["defense"].canceled -= OnActionCanceled;
        }
        else
        {
            PlayerInput.actions["defense2"].performed -= OnActionPerformed;
            PlayerInput.actions["defense2"].canceled -= OnActionCanceled;
        }
        
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        isHolding = true;
    }

    private void OnActionCanceled(InputAction.CallbackContext context)
    {
        isHolding = false;
    }

    private void SetDash()
    {
        //bool flib = this.gameObject.GetComponent<SpriteRenderer>().flipX;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        //Debug.Log("FlipX value: " + flib);
        rb.velocity = new Vector2(transform.localScale.x * 7, rb.velocity.y);

        /*if (flib)
        {
            rb.velocity = new Vector2(-7, rb.velocity.y); 
            Debug.Log("Dashing Left");
        }
        else
        {
            rb.velocity = new Vector2(-7, rb.velocity.y); 
            Debug.Log("Dashing Right");
        }*/
    }

    private void AnimationCount()
    {
        currentCount += 1;
        if(currentCount > 2)
        {
            currentCount = 0;
        }
        moveSpeed = 0;
        animator.SetInteger("AnimationCount", currentCount);
    }

    private void SetDefense()
    {
        if (isHolding)
        {
            currentValue -= decreaseRate * Time.deltaTime;
            Light2D s_Light = GetComponentInParent<Player>().defense;
            s_Light.transform.position = transform.position;
            s_Light.enabled = true;
            //GetComponentsInParent<Light2D>()[0].enabled = true;
            if (currentValue == 0)
            {
                currentValue = 0;
            }
            Debug.Log("Current Value: " + currentValue);
        }
        else
        {
            Light2D s_Light = GetComponentInParent<Player>().defense;
            s_Light.transform.position = transform.position;
            s_Light.enabled = false;
            //GetComponentsInParent<Light2D>()[0].enabled = false;
            if (Time.time > setTime)
            {
                currentValue += 1;
                setTime = Time.time + reMana;
                if (currentValue >= 11)
                {
                    currentValue = 11;
                }
            }
        }

        if(reMana <= 4f)
        {
            reMana = 4f;
        }
    }

    private void SetHealth()
    {
        if(numOfhearts >= 5)
        {
            numOfhearts = 5;
        }

        if(hp < numOfhearts)
        {
            if(Time.time > setTimeHp)
            {
                hp += 0.5f;
                setTimeHp = Time.time + 15;
                if (hp >= numOfhearts)
                {
                    hp = numOfhearts;
                }
            }
        }
        if (hp > numOfhearts)
        {
            hp = numOfhearts;
        }
        else
        {
            return;
        }

        
    }

    public void Setcollider()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0.63f, 0.22f);
    }

    public void Recoli()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0.54f, 1.15625f);
    }
    public void SoundAttack()
    {
        AudioManager.Instance.PlaySFX("attack");
    }
}

