using Pathfinding;
using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f, jumpForce = 100f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true, isJumping, isInAir;
    public bool directionLookEnabled = true;

    [SerializeField] Vector3 startOffset;

    private Path path;
    private int currentWaypoint = 0;
    [SerializeField] public RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    private bool isOnCoolDown;
    public Animator animatorController;
    public float attackSpeed;
    public float nextAttack;
    public float distance;
    public float radius;
    public bool setAttack;
    public float damage;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
        isInAir = false;
        isOnCoolDown = false;
        animatorController = gameObject.GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void Update()
    {
        if(Vector2.Distance(target.transform.position, transform.position) <= distance)
        {
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackSpeed;
                animatorController.SetTrigger("Attack");
                setAttack = true;
                //Debug.Log("Collided" + setAttack + animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            }
        }
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
            if (this.gameObject.GetComponent<MonsterBehavior>())
            {
                this.gameObject.GetComponent<MonsterBehavior>().enabled = false;
            }
            else
            {
                return;
            }

        }
        else
        {
            this.gameObject.GetComponent<EnemyAI>().enabled = false;
            if (this.gameObject.GetComponent<MonsterBehavior>())
            {
                this.gameObject.GetComponent<MonsterBehavior>().enabled = true;
            }
            else
            {
                return;
            }
            
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset, transform.position.z);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed;

        // Jump
        if (!transform.name.Equals("Batty") && !transform.name.Equals("Batty(Clone)"))
        {
            if (jumpEnabled && isGrounded && !isInAir && !isOnCoolDown)
            {
                if (direction.y > jumpNodeHeightRequirement)
                {
                    if (isInAir) return;
                    isJumping = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    StartCoroutine(JumpCoolDown());

                }
            }
            if (isGrounded)
            {
                isJumping = false;
                isInAir = false;
            }
            else
            {
                isInAir = true;
            }
        }
        

        // Movement
        if (transform.name.Equals("Batty(Clone)") || transform.name.Equals("Batty"))
        {
            rb.AddForce(force);
        }
        else
        {
            transform.Translate(new Vector2(force.x, rb.velocity.y) * Time.deltaTime);
        }
        //rb.velocity = new Vector2(force.x, rb.velocity.y);
        

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
            animatorController.SetBool("Move", true);
        }
        else
        {
            animatorController.SetBool("Move", false);
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator JumpCoolDown()
    {
        isOnCoolDown = true; 
        yield return new WaitForSeconds(1f);
        isOnCoolDown = false;
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
                        AudioManager.Instance.PlaySFX("Htakehit");
                        animator.Play("Hurt");
                        hitCollider.GetComponent<PlayerControl>().hp -= damage;
                        hitCollider.GetComponent<PlayerControl>().setTimeHp = Time.time + 15f;
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
                        //Debug.Log(player[index].name);
                        setAttack = false;
                    }

                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
