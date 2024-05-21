using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauthersCat : MonoBehaviour
{
    public bool flip = false;
    public float speed = 6;
    private float startRun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startRun >= 5 && Time.time - startRun <= 8)
        {
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
        else if(Time.time - startRun > 8 && Time.time - startRun <= 10)
        {
            gameObject.GetComponent<Animator>().SetBool("run", false);
        }
        else if (Time.time - startRun > 10 && Time.time - startRun <= 14)
        {
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
        else if (Time.time - startRun > 14 && Time.time - startRun <= 16)
        {
            gameObject.GetComponent<Animator>().SetBool("run", false);
        }
        else if (Time.time - startRun > 16)
        {
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
    }
}
