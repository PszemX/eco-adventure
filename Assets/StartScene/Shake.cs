using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    private float autoStart;
    public float timeToStart = 5.5f;
    public AnimationCurve curve;
    public float duration = 7.0f;
    void Start()
    {
        autoStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - autoStart > timeToStart)
        {
            start = true;
            autoStart += 50;
        }
        if(start)
        {
            start = false;
            StartCoroutine(StartShaking());
        }
    }

    IEnumerator StartShaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0.0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}
