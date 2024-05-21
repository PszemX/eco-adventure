using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startFalling : MonoBehaviour
{
    private float fallSpeed = 10.0f;
    private float startFall;
    public float timeToFall = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        startFall = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startFall >= timeToFall)
        {
            transform.Translate(-fallSpeed * 0.2f * Time.deltaTime, -fallSpeed * 0.5f * Time.deltaTime, 0.0f, Space.World);
            transform.Rotate(0.0f, 0.0f, fallSpeed * 1.0f * Time.deltaTime);
        }
    }
}
