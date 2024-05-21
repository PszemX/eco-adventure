using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip bonusSound;
    public AudioClip punchSound;
    public AudioClip shootSound;
    public AudioClip machineGunSound;
    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip landingSound;
    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;
    // Car
    public AudioClip carDoorOpening;
    public AudioClip carEngineOn;
    public AudioClip carDrive;
    public AudioClip carHorn;
    //potka
    public AudioClip heal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void playGunSound()
    {
        source.PlayOneShot(shootSound, 10);
    }

    public void playMachineGunSound()
    {
        source.PlayOneShot(machineGunSound, 10);
    }

    public void playPunchSound()
    {
        source.PlayOneShot(punchSound, 10);
    }

    public void playJumpSound()
    {
        source.PlayOneShot(jumpSound, 10);
    }

    public void playBonusSound()
    {
        source.PlayOneShot(bonusSound, 10);
    }

    public void playLandingSound() 
    {
        source.PlayOneShot(landingSound, 10);
    }

    public void playHurtSound()
    {
        source.PlayOneShot(hurtSound, 10);
    }

    public void playCanEngineSound() 
    {
        source.PlayOneShot(carEngineOn, 15);
    }

    public void playCarDoorOpenSound()
    {
        source.PlayOneShot(carDoorOpening, 20);
    }

    public void playCarDriveSound()
    {
        source.PlayOneShot(carDrive, 15);
    }

    public void playCarHornSound()
    {
        source.PlayOneShot(carHorn, 20);
    }

    public void playHealSound()
    {
        source.PlayOneShot(heal, 10);
    }
}
