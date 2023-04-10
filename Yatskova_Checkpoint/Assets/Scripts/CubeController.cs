using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************************
File: CubeController.cs
Author: Anya Yatskova
DP Email: anna.yatskova@digipen.edu
Date: 4/10/2022
Course: CS174
Section: A
Description: Script that controls the cube objects and many of their sounds

****************************************************************************************************/

public class CubeController : MonoBehaviour
{
    //check if the object is a bomb
    public bool IsBomb;

    // Is this object already collected
    public bool Collected;

    public AudioSource audioSource;
    public AudioClip audioClip;

    //sound of the ticking bomb
    public AudioSource TickingSound;

    // Start is called before the first frame update
    void Start()
    {
        //if the object is a bomb, initiate the approaching ticking sound
        if (IsBomb == true)
        {
            TickingSound = GetComponent<AudioSource>();
            TickingSound.Play();
        }

        // Set object to uncollected
        Collected = false;
    }

    //play the explosion sound effect
    public void Explode()
    {
        // Mark this object as "Collected"
        Collected = true;

        // Stop ticking sound effect
        TickingSound.Stop();
        //Play the pickup sound effect
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();

        //Make object invisible
        gameObject.GetComponent<MeshRenderer>().enabled = false;


        //Give object a chance to play sound before disabling
        Invoke("Disable", audioClip.length);
    }

    public void PickupObject()
    {
        Collected = true;

        //Play the pickup sound effect
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();

        //Make object invisible
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //Give object a chance to play sound before disabling
        Invoke("Disable", audioClip.length);
    }

    // Disable the game object
    void Disable()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}