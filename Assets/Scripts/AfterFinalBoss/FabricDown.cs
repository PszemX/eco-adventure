using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricDown : MonoBehaviour
{
    private float fallSpeed = 10.0f;
    private float startFall;
    private float timeToFall = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        startFall = Time.time;
        fallSpeed = Random.Range(6, 9);
        timeToFall = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startFall >= timeToFall)
        {
            transform.Translate(0.0f, -fallSpeed * 0.5f * Time.deltaTime, 0.0f, Space.World);
        }
    }
}
