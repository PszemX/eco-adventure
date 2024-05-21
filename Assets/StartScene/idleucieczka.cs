using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleucieczka : MonoBehaviour
{
    public bool flip = false;
    public float speed = 7;
    private float startRun;
    // Start is called before the first frame update
    void Start()
    {
        startRun = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startRun >= 6.5)
        {
            if(flip)gameObject.GetComponent<SpriteRenderer>().flipX = flip;
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
    }
}
