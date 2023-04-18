using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************************
File: CameraController.cs
Author: Anya Yatskova
DP Email: anna.yatskova@digipen.edu
Date: 4/10/2022
Course: CS174
Section: A
Description: Script that controls the camera

****************************************************************************************************/

public class CameraController : MonoBehaviour
{

    // create player
    public GameObject player;

    // create offset value
    private Vector3 offset;

    void Start()
    {
        // take current camera transform position and find difference between camera and player position
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // set camera's transform position to the player's transform position, every frame
        transform.position = player.transform.position + offset;
    }
}
