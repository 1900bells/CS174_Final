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
    public AudioClip CollectClip;
    public AudioClip TreasureCollectClip;
    public AudioClip ExplodeClip;

    //sound of the ticking bomb
    public AudioSource TickingSource;
    public AudioClip TickingSound;

    [SerializeField]
    float MaxTickingDistance;

    // Object specific references
    public GameObject player;

    static Color defaultColor = new Color32(143, 0, 254, 1);

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
        //Play the explode sound effect
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ExplodeClip;
        audioSource.Play();

        //Make object invisible
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //Give object a chance to play sound before disabling
        Invoke("Disable", CollectClip.length);
    }

    public void PickupObject()
    {
        Collected = true;

        //Play the pickup sound effect
        audioSource = GetComponent<AudioSource>();
        if (type == CollectableType.Treasure)
            audioSource.clip = TreasureCollectClip;
        else
            audioSource.clip = CollectClip;
        audioSource.Play();

        //Make object invisible
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //Give object a chance to play sound before disabling
        Invoke("Disable", CollectClip.length);
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
            //TickingSource.Play();
        }
    }

    // Make bomb and treasure objects a different color
    public static void RevealBombAndTreasure()
    {
        CubeController[] pickUps = GameObject.FindObjectsOfType<CubeController>();
        foreach (CubeController pickup in pickUps)
        {
            // Turn bombs red
            if (pickup.type == CollectableType.Bomb)
                pickup.GetComponent<Renderer>().material.color = Color.red;
            // Turn treasures yellow
            else if (pickup.type == CollectableType.Treasure)
                pickup.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    // Return bomb and treasure objects to defualt color
    public static void HideBombAndTreasure()
    {
        CubeController[] pickUps = GameObject.FindObjectsOfType<CubeController>();
        foreach (CubeController pickup in pickUps)
        {
            // Turn bombs red
            if (pickup.type == CollectableType.Bomb)
                pickup.GetComponent<Renderer>().material.color = defaultColor;
            // Turn treasures yellow
            else if (pickup.type == CollectableType.Treasure)
                pickup.GetComponent<Renderer>().material.color = defaultColor;
        }
    }

    public void StartTickingSound()
    {
        TickingSource.Play();
    }
}
