using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationBefore : MonoBehaviour
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
        if(Time.time - start >= 15)
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}
