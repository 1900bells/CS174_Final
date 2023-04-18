using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedSoundController : MonoBehaviour
{
    // Reference to player controller
    PlayerController playerController;


    public AudioSource SlowMovementSound;
    public AudioSource FastMovementSound;

    public AudioClip FastMovementClip;
    public AudioClip SlowMovementClip;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to player controller
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        AudioSource[] SourceArray = GetComponents<AudioSource>();
        SlowMovementSound = SourceArray[3];
        FastMovementSound = SourceArray[4];

        SlowMovementSound.clip = SlowMovementClip;
        //SlowMovementSound.Play();

        FastMovementSound.clip = FastMovementClip;
        //FastMovementSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is in air, dont play sound
        if (playerController.NotJumping == false)
        {
            SlowMovementSound.volume = 0;
        }
        // If player is on ground, play movement sound
        else if (playerController.NotJumping == true)
        {
            // Reference to player rigidbody
            Rigidbody rb = GetComponent<Rigidbody>();
            float magnitude = rb.velocity.magnitude;
            
            /*
            if (magnitude > 10)
            {
                SlowMovementSound.clip = FastMovementClip;
                if (SlowMovementSound.isPlaying == false)
                    SlowMovementSound.Play();
            }
            else
            {
                SlowMovementSound.clip = SlowMovementClip;
                if (SlowMovementSound.isPlaying == false)
                    SlowMovementSound.Play();
            }
             */

            //SlowMovementSound.volume = Mathf.Clamp(magnitude, 0.0f, 10.0f) / 10.0f;
            SlowMovementSound.volume = (1.0f - Mathf.Abs(10.0f - magnitude) / 10.0f) * Time.timeScale;
            if (magnitude > 13)
                SlowMovementSound.volume = 0.0f;
            FastMovementSound.volume = (Mathf.Clamp(magnitude, 0.0f, 20.0f) / 20.0f) * Time.timeScale;
        }
    }
    public void StartSounds()
    {
        SlowMovementSound.Play();
        FastMovementSound.Play();
    }
}

