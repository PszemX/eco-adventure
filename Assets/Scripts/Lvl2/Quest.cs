using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public Animator animator;
    private bool once = true;
    public bool isSmoke = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isQuestDone && once)
        {
            if(!isSmoke)
            {
                transform.Find("chat").gameObject.SetActive(false);
                transform.Find("chat2").gameObject.SetActive(true);
                animator.SetBool("isRescue", true);
                once = false;
            }
            else
            {
                transform.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.isQuestDone = true;
        }
    }
}
