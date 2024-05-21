using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBox : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint = 0;
    public bool startFalling = false;
    [SerializeField] private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(startFalling)
        {
            if (Vector2.Distance(this.transform.position, waypoints[currentWaypoint].transform.position) < 0.1f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            }
            transform.position = Vector2.MoveTowards(this.transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);
        }
    }
}
