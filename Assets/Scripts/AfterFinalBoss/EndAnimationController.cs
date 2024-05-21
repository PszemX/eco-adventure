using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAnimationController : MonoBehaviour
{
    private float start;
    // Start is called before the first frame update
    void Start()
    {
        start = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - start >= 22)
        {
            SceneManager.LoadSceneAsync("EndGame");
        }
    }
}
