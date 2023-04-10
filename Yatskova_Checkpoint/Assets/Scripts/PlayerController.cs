using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/***************************************************************************************************
File: PlayerController.cs
Author: Anya Yatskova
DP Email: anna.yatskova@digipen.edu
Date: 4/10/2022
Course: CS174
Section: A
Description: Script that controls the player and many of the sounds caused by the player

****************************************************************************************************/

public class PlayerController : MonoBehaviour
{
    //public variable that keeps track of speed as an editable property
    public float speed;

    //determine the height of the character's jump
    public float jumpHeight;

    //assign text to variable
    public Text countText;

    //reference winText
    public Text winText;

    //create timer
    public Text timerText;

    public AudioSource JumpSource;
    public AudioClip JumpClip;

    public AudioSource MovementSound;
    public AudioClip MovementClip;

    
    public AudioSource[] SourceArray;

    public int maxTime;
    public int timerTime;

    //create the variable to hold the reference
    private Rigidbody rb;

    //collect objects
    private int count;

    //check if the game is active; this helps make sure the player cannot continue collecting objects after losing
    private bool GameActive = true;

    public CubeController bomb;

    //checks whether the player is jumping
    public bool NotJumping;

    //checks whether or not the player has won, which at the start of the game they have not yet
    public bool HasWon = false;

    GameObject BombObject;

    void UpdateGameActiveState(bool active)
    {
        if (GameActive == active)
        {
            // nothing changed
            return;
        }

        GameActive = active;
    }


    //called at the start of the frame
    void Start()
    {
        //access rigidbody
        rb = GetComponent<Rigidbody>();

        //our count is equal to 0
        count = 0;

        //call function
        SetCountText();

        //assign to string value
        winText.text = "";

        ResetTimer();
        TimerCount();

        NotJumping = true;

        SourceArray = GetComponents<AudioSource>();
        MovementSound = SourceArray[0];
        MovementSound.clip = MovementClip;
        MovementSound.Play();

        JumpSource = SourceArray[1];

        UpdateGameActiveState(true);


    }

    //called before performing physics calculations
    void FixedUpdate()
    {
        //get input from the player through the keyboard
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //keep track of movement
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //multiply movement by speed
        rb.AddForce(movement * speed);

        SetCountText();
        UpdateMovementSound();;
    }

    //the movement sound (sound of the ball rolling) will get louder depending on how fast the player is moving
    void UpdateMovementSound()
    {
        float speed = rb.velocity.x + rb.velocity.y + rb.velocity.z;

        if (NotJumping == false)
        {
            MovementSound.volume = 0;
        }

        else if (NotJumping == true)
        {
            MovementSound.volume = speed * 2;
        }
    }

    private void Update()
    {
        Jump();
    }

    //jump mechanic
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 jump = new Vector3(0.0f, jumpHeight, 0.0f);
            rb.AddForce(jump * speed);

            //Debug.Log("Player Jumped");

            NotJumping = false;

            JumpSource.clip = JumpClip;

            JumpSource.Play();

        }

    }


    //will be called when the player first touches a trigger collider
    void OnTriggerEnter(Collider other)
    {
        //Do nothing if the player has won or lost
        if (!GameActive)
        {
            return;
        }

        //If the player ran into a bomb
        if (other.gameObject.CompareTag("Bomb"))
        {
            //then you lose
            LoseState();
            bomb.Explode();
        }

        //If the player ran into a pickup
        if (other.gameObject.CompareTag("Pick Up"))
        {
            if (other.gameObject.GetComponent<CubeController>().Collected == false)
            {
                //Pick up the "pick up" object
                other.gameObject.GetComponent<CubeController>().PickupObject();

                //new value of count is equal to old value + 1
                count = count + 1;

                //call function
                SetCountText();
            }
        }

        NotJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
           NotJumping = true;
    }

    //set the count text
    void SetCountText()
    {
        //display count amount
        countText.text = "Count: " + count.ToString();

        //if the count is more than or equal to 12
        if (count >= 13)
        {
            UpdateGameActiveState(false);
 
            //tell the player they have won
            winText.text = "You Win!";

            //you have won
            HasWon = true;
            timerTime = 1;
        }

        //if you run out of time, and you haven't won yet
        if (timerTime == 0 && HasWon == false)
        {
            LoseState();
            //if you lost, the objects you collect won't add to your count
            count = 0;
        }    
    }

    //the game's lose state that tells the player they have lost, and sets the timer to display 0
    public void LoseState()
    {
        UpdateGameActiveState(false);

        winText.text = "You Lose!";
        timerTime = 1;
    }

    //reset timer
    void ResetTimer()
    {
        timerTime = maxTime;

        timerText.text = timerTime.ToString();
    }

    //count down with timer
    void TimerCount()
    {
        timerTime--;
        timerText.text = timerTime.ToString();

        if (timerTime > 0)
        {
            Invoke("TimerCount", 1f);
        }
    }

}


