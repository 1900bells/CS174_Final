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
    // Is this object a normal pick up, bomb, or treasure?
    public enum CollectableType
    {
        PickUp,
        Bomb,
        Treasure
    }

    private CollectableType type;

    // Is this object already collected
    public bool Collected;

    public AudioSource audioSource;
    public AudioClip audioClip;

    //sound of the ticking bomb
    public AudioSource TickingSource;
    public AudioClip TickingSound;
    public float MaxTickingDistance;

    // Object specific references
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Object has not been collected yet
        Collected = false;

        // Get reference to player
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // For bombs, update how loud the ticking is
        if (type == CollectableType.Bomb)
        {
            // Distance between player and bomb
            float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            // Adjust volume based on how close player is
            TickingSource.volume = (MaxTickingDistance - Mathf.Clamp(distance, 0.0f, MaxTickingDistance)) / MaxTickingDistance;
        }
    }

    //play the explosion sound effect
    public void Explode()
    {
        // Mark this object as "Collected"
        Collected = true;

        // Stop ticking sound effect
        TickingSource.Stop();
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


    public CollectableType Type()
    {
        return type;
    }

    public void Type(CollectableType _type)
    {
        type = _type;

        //if the object is a bomb, initiate the approaching ticking sound
        if (type == CollectableType.Bomb)
        {
            TickingSource = GetComponent<AudioSource>();
            TickingSource.loop = true;
            TickingSource.clip = TickingSound;
            TickingSource.Play();
        }
    }
}
