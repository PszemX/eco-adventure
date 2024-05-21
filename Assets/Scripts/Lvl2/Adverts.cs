using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adverts : MonoBehaviour
{
    private float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isBossKilled)
        {
            counter += 5f * Time.deltaTime;
            if(counter < 16)
            {
                transform.Translate(0.0f, 5f * Time.deltaTime, 0.0f, Space.World);
            }
            else
            {
                GameManager.instance.isBossKilled = false;
            }
        }
    }
}
