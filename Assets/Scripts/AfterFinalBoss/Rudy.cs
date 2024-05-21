using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudy : MonoBehaviour
{
    public bool flip = false;
    public float speed = 6;
    private float startRun;
    // Start is called before the first frame update
    void Start()
    {
        startRun = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startRun >= 18)
        {
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }

    }
}
