using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLvl2 : MonoBehaviour
{
    private bool open = false;
    private int move = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            transform.Translate(5f * Time.deltaTime, 0.0f, 0.0f, Space.World);
            move++;
            if (move > 200)
            {
                open = false;
                GetComponent<BoxCollider2D>().isTrigger = true;
                gameObject.SetActive(false);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameManager.instance.keysFound == 1)
            {
                open = true;
            }
        }
    }

}
