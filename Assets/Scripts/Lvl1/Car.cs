using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool goRide = false;
    private AudioController audioController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        audioController = GetComponent<AudioController>();
    }
    // Update is called once per frame
    void Update()
    {
        if(goRide)
        {
            transform.Translate(7f * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
    }

    public void TakeCarDriver()
    {
        transform.Find("Guard").gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }
}
