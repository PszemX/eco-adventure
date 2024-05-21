using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucieczkaPtakow : MonoBehaviour
{
    private float speed;
    private float walkTime;
    public bool left = true;
    public float min = 6;
    public float max = 8;
    public float endWalk = 14;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(min, max);
        if (left) speed *= -1;
        walkTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        if (Time.time - walkTime >= endWalk) speed = 0.0f;
    }
}
