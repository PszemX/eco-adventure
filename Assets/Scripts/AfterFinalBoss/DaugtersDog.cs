using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaugtersDog : MonoBehaviour
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
        if (Time.time - startRun >= 5 && Time.time - startRun <= 7)
        {
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
        else if (Time.time - startRun > 7 && Time.time - startRun <= 8)
        {
            gameObject.GetComponent<Animator>().SetBool("run", false);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Time.time - startRun > 8 && Time.time - startRun <= 10)
        {
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
        else if (Time.time - startRun > 10 && Time.time - startRun <= 10.5)
        {
            gameObject.GetComponent<Animator>().SetBool("run", false);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Time.time - startRun > 12)
        {
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
    }
}
