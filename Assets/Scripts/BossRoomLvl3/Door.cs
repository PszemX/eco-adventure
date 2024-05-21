using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool openDoor = false;
    public bool closeDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        if (closeDoor)
        {
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isBossKilled)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && openDoor)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && closeDoor)
        {
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            GetComponent<BoxCollider2D>().isTrigger = false;
           
        }
    }

}
