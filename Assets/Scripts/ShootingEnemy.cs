using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{

    #region Public Variables
    public LayerMask groundMask;
    public LayerMask playerMask;
    public GameObject target;
    public GameObject projectile;
    public HealthBar healthBar;
    public float groundedRadius = 0.2f;
    public float deadTime = 1.0f;
    public float startTimeBtwShots;
    public float moveSpeed;
    public float attackRange;
    public int maxHealth = 100;
    public bool isStatic = false;
    public bool isFacingRight = true;
    #endregion

    #region Private Variables
    private AudioController audioController;
    private Animator animator;
    private bool isMovingRight = true;
    private bool isGrounded;
    private bool isGroundWaiting;
    private bool attackMode;
    private bool cooling;
    private float lastCoolTime = 0.0f;
    private float timeBtwShots;
    private int currentHealth;
    #endregion



    [SerializeField] private Transform groundCheck;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioController = GetComponent<AudioController>();
        if (isStatic && !isFacingRight) Flip();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGroundWaiting)
        {
            animator.SetBool("canWalk", false);
            if (Time.time - lastCoolTime > 3.0f) {
                isGroundWaiting = false;
                Flip();
                isMovingRight = !isMovingRight;
            }
            
        }
        else {
            isGrounded = true;

            if (attackMode)
            {
                Attack();
            }
            else 
            {
                StopAttack();
                if(!isStatic)
                {
                    animator.SetBool("canWalk", true);
                    // Idü w kierunku gracza
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        if (isMovingRight) moveRight();
                        else moveLeft();
                    }
                }
            }
        }
        

        if (cooling)
        {
            animator.SetBool("Attack", false);
        }

    }

    private void moveRight()
    {
        if (!isFacingRight) Flip();
        isMovingRight = true;
        Vector2 groundCheckPosition = groundCheck.position;
        isGrounded = Physics2D.Linecast(groundCheckPosition, groundCheckPosition + Vector2.down * groundedRadius, groundMask);
        if (isGrounded) transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        else if (!isGrounded && !isGroundWaiting)
        {
            isGroundWaiting = true;
            lastCoolTime = Time.time;
        }
    }

    private void moveLeft()
    {
        if (isFacingRight) Flip();
        isMovingRight = false;
        Vector2 groundCheckPosition = groundCheck.position;
        isGrounded = Physics2D.Linecast(groundCheckPosition, groundCheckPosition + Vector2.down * groundedRadius, groundMask);
        if (isGrounded) transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        else if (!isGrounded && !isGroundWaiting)
        {
            isGroundWaiting = true;
            lastCoolTime = Time.time;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            GameManager.instance.KillEnemy();
            Die();
        }
    }

    private void Attack()
    {
        attackMode = true;
        if (timeBtwShots <= 0)
        {
            float dir = isFacingRight ? 1 : -1;
            Vector3 projectilePosition = new Vector3(transform.position.x + (0.5f * dir), transform.position.y - 0.22f, transform.position.z);
            Instantiate(projectile, projectilePosition, Quaternion.identity);
            audioController.playGunSound();
            timeBtwShots = startTimeBtwShots;
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }

        animator.SetBool("canWalk", false);
        animator.SetBool("Attack", true);
    }

    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an object on the "Player" layer
        if (collision.gameObject == target)
        {
            attackMode = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            attackMode = false;
        }
        if (collision.CompareTag("FallLevel"))
        {
            Die();
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
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
    }

}
