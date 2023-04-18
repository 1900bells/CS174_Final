using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatButton : MonoBehaviour
{
    bool CheatActive;

    [SerializeField]
    AudioSource ButtonSource;
    [SerializeField]
    AudioClip ButtonSound;

    // Start is called before the first frame update
    void Start()
    {
        // Start with cheats off
        CheatActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cheat()
    {
        // If we are already cheating
        if (CheatActive == true)
        {
            // Turn off cheats
            CheatActive = false;
            CubeController.HideBombAndTreasure();
        }
        // If we are not already cheating
        else
        {
            // Turn on cheats
            CheatActive = true;
            CubeController.RevealBombAndTreasure();
        }

        // Play button sound
        ButtonSource.clip = ButtonSound;
        ButtonSource.Play();
    }
}
