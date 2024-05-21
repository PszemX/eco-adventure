using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaughterSeed : MonoBehaviour
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
        if (Time.time - startRun >= 5 && Time.time - startRun <= 10)
        {
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
        else if(Time.time - startRun > 10 && Time.time - startRun <= 12)
        {
            gameObject.GetComponent<Animator>().SetBool("run", false);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Time.time - startRun > 12 && Time.time - startRun <= 13)
        {
            gameObject.GetComponent<Animator>().SetBool("plant", true);
        }
        else if (Time.time - startRun > 13 && Time.time - startRun <= 14)
        {
            gameObject.GetComponent<Animator>().SetBool("plant", false);
            transform.Find("Tree1").gameObject.SetActive(true);
            transform.DetachChildren();
        }
        else if (Time.time - startRun > 14)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
    }
}
