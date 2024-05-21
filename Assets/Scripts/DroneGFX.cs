using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DroneGFX : MonoBehaviour
{
    public AIPath aiPath;
    public int damage = 20;

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01) 
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
    }
}
