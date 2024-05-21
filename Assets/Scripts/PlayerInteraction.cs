using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject chat;
    public CharacterController player;
    public PlayerCombat playercombat;
    public AudioController audioController;
    public Animator animator;
    public GameObject arm;
    public bool interaction = false;
    public bool potion = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        audioController = GetComponent<AudioController>();
        playercombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))    // Interakcja.
        {
            interaction = true;
        } 
        else if (Input.GetKeyUp(KeyCode.E))
        {
            interaction = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && GameManager.instance.cocktails > 0 && player.lives < 4)
        {
            potion = true;
            GameManager.instance.useCocktail();
            player.lives++;
            audioController.playHealSound();
            arm.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            potion = false;
            arm.SetActive(true);
        }
        animator.SetBool("Drink", potion);
        animator.SetBool("Interaction", interaction);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint!");
            player.startPosition = other.transform.position;
        }
        if (other.CompareTag("Card"))
        {
            GameManager.instance.AddPoints();
            other.gameObject.SetActive(false);
            audioController.playBonusSound();
        }
        if (other.CompareTag("Cocktail"))
        {
            player.cocktails++;
            GameManager.instance.addCocktail();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Key"))
        {
            player.keys++;
            GameManager.instance.AddKeys();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Gun"))
        {
            playercombat.hasGun = 1.0f;
            playercombat.gunImg.color = Color.gray;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("MachineGun"))
        {
            playercombat.hasMachineGun = 1.0f;
            playercombat.machineGunImg.color = Color.gray;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("FallLevel"))
        {
            player.Death();
        }
        if(other.CompareTag("MovingPlatform"))
        {
            transform.SetParent(other.transform);
        }
        if (other.CompareTag("FallBox"))
        {
            other.GetComponent<FallBox>().startFalling = true;
        }
        if (other.CompareTag("NextLevel"))
        {
            GameManager.instance.NextLevel();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("NPC") || other.CompareTag("Chest"))
        {
            chat.SetActive(true);
        }
        if (other.CompareTag("NPC") && interaction && player.keys == 2)
        {
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            other.gameObject.transform.Find("chat").gameObject.SetActive(false);
            other.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            other.gameObject.transform.Find("Car").gameObject.GetComponent<Animator>().SetBool("Open", true);
            audioController.playCarDoorOpenSound();
            audioController.playCarHornSound();
            audioController.playCarDriveSound();
            chat.SetActive(false);
        }
        if (other.CompareTag("Car") && interaction)
        {
            if(other.gameObject.GetComponent<Car>().goRide == false)
            {
                other.gameObject.GetComponent<Animator>().SetBool("Ride", true);
                other.transform.Translate(0.0f, 0.0f, -2.0f, Space.World);
                transform.SetParent(other.transform);
                other.gameObject.GetComponent<Car>().goRide = true;
                other.gameObject.GetComponent<Car>().TakeCarDriver();
                other.gameObject.tag = "Untagged";
                chat.SetActive(false);
            }
        }
        if (other.CompareTag("Chest") && player.keys > 0 && interaction)
        {
            Animator chestAnimator = other.GetComponent<Animator>();
            GameManager.instance.useKey();
            chestAnimator.SetTrigger("Opened");  // ustaw trigger "Opened" w animatorze skrzynki
            foreach (Collider2D collider in other.gameObject.GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }
            chat.SetActive(false);
        }
        if (other.CompareTag("MovingPlatform"))
        {
            transform.Translate(0.7f * Time.deltaTime, 0.0f, 0.0f, Space.World);
            chat.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        transform.SetParent(null);
        chat.SetActive(false);
    }
}
