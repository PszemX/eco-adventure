using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private Transform wallCheck;
    public float speed;
    public float range;
    private float shotTime;

    private CharacterController player;
    private PlayerCombat playerCombat;
    private Rigidbody2D rigidBody;
    Vector3 cursorPos;
    public LayerMask groundLayer;
    private Vector2 startingPosition;
    private bool isFacingRight;
    private int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        playerCombat = GetComponent<PlayerCombat>();
        rigidBody = GetComponent<Rigidbody2D>();
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = cursorPos - transform.position;
        Vector3 rotation = transform.position - cursorPos;
        rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        shotTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, 0.1f, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                DestroyProjectile();
            }
        }
        if (Time.time - shotTime >= 1.5)
        {
            DestroyProjectile();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            DestroyProjectile();
            ShootingEnemy shootingEnemy = other.gameObject.GetComponent<ShootingEnemy>();
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            if(shootingEnemy) shootingEnemy.TakeDamage(damage);
            else if(enemyController) enemyController.TakeDamage(damage);
            DestroyProjectile();
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            DestroyProjectile();
            BossController bossController = other.gameObject.GetComponent<BossController>();
            bossController.TakeDamage(damage);
            DestroyProjectile();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DestroyProjectile();
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            enemyController.TakeDamage(damage);
            ShootingEnemy shootingEnemy = other.gameObject.GetComponent<ShootingEnemy>();
            shootingEnemy.TakeDamage(damage);
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
