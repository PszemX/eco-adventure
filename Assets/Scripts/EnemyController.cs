using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Public Variables
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform groundCheck;
    public LayerMask raycastMask;
    public LayerMask groundMask;
    public HealthBar healthBar;
    public GameObject target;
    public float maxDistanceBetween = 50.0f;
    public float walledRadius = 0.1f;
    public float groundedRadius = 0.2f;
    public float attackRange = 0.8f;
    public float moveRange = 1.0f;
    public float deadTime = 1.0f;
    public float attackDistance;
    public float rayCastLength;
    public int attackDamage = 50;
    public int maxHealth = 100;
    #endregion

    #region Private Variables
    [Range(0.01f, 20)] [SerializeField] private float moveSpeed = 1.3f;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isMovingRight = true;
    private bool cooling;
    private bool isWalled;
    private bool attackMode;
    private bool isGrounded;
    private float startPositionX;
    private float distance;
    private int currentHealth;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Awake()
    {
        startPositionX = this.transform.position.x;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * 9.81f);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= maxDistanceBetween)
        {
            if (distanceToTarget < attackRange && cooling == false)
            {
                Attack();
            }
            else
            {
                isGrounded = true;
                isWalled = false;

                // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
                // This can be done using layers instead but Sample Assets will not overwrite your project settings.

                if (!isGrounded || isWalled)
                {
                    // Jeœli nie ma pod³o¿a pod przeciwnikiem, zatrzymaj jego ruch
                    animator.SetBool("canWalk", false);
                    StopAttack();
                }
                else
                {
                    StopAttack();
                    animator.SetBool("canWalk", true);
                    // IdŸ w kierunku gracza
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        if (target.transform.position.x > transform.position.x)
                        {
                            moveRight();
                        }
                        else
                        {
                            moveLeft();
                        }
                    }
                }
            }

            if (cooling)
            {
                animator.SetBool("Attack", false);
            }
        }
        else 
        { 
            animator.SetBool("canWalk", false);
            StopAttack();
        }

    }

    private void Attack()
    {
        attackMode = true;

        animator.SetBool("canWalk", false);
        animator.SetBool("Attack", true);
    }

    private void StopAttack() 
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void moveRight() {
        if (!isFacingRight) Flip();
        isMovingRight = true;
        Vector2 groundCheckPosition = groundCheck.position;
        Vector2 wallCheckPosition = wallCheck.position;
        isGrounded = Physics2D.Linecast(groundCheckPosition, groundCheckPosition + Vector2.down * groundedRadius, groundMask);
        isWalled = Physics2D.Linecast(wallCheckPosition, wallCheckPosition + Vector2.right * walledRadius, groundMask);
        if (isGrounded && !isWalled) transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        else if (!isGrounded || isWalled)
        {
            // Jeœli nie ma pod³o¿a pod przeciwnikiem, zatrzymaj jego ruch
            animator.SetBool("canWalk", false);
            StopAttack();
        }
    }

    private void moveLeft() {
        if (isFacingRight) Flip();
        isMovingRight = false;
        Vector2 groundCheckPosition = groundCheck.position;
        Vector2 wallCheckPosition = wallCheck.position;
        isGrounded = Physics2D.Linecast(groundCheckPosition, groundCheckPosition + Vector2.down * groundedRadius, groundMask);
        isWalled = Physics2D.Linecast(wallCheckPosition, wallCheckPosition + Vector2.left * walledRadius, groundMask);
        if (isGrounded && !isWalled) transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        else if (!isGrounded || isWalled)
        {
            // Jeœli nie ma pod³o¿a pod przeciwnikiem, zatrzymaj jego ruch
            animator.SetBool("canWalk", false);
            StopAttack();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        StartCoroutine(KillOnAnimationEnd());
    }

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(deadTime);
        GameManager.instance.KillEnemy();
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FallLevel"))
        {
            Die();
        }
    }

}
