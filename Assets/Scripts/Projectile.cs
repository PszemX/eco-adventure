using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float range;

    private Transform player;
    private Vector2 target;
    private Vector2 startingPosition;
    private bool isFacingRight;
    private int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = new Vector2(transform.position.x, transform.position.y);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        isFacingRight = target.x > startingPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFacingRight)
        {
            transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            if (transform.position.x <= startingPosition.x - range) DestroyProjectile();
        }
        else if (isFacingRight)
        {
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            if (transform.position.x >= startingPosition.x + range) DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
            DestroyProjectile();
        }
    }

    void DestroyProjectile() 
    {
        Destroy(gameObject);
    }
}
