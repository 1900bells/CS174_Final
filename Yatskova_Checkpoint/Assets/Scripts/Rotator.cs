using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************************
File: Rotator.cs
Author: Anya Yatskova
DP Email: anna.yatskova@digipen.edu
Date: 4/10/2022
Course: CS174
Section: A
Description: Script that rotates the objects and bomb

****************************************************************************************************/

public class Rotator : MonoBehaviour
{
    // function is called every frame
    void Update()
    {
        // rotate the cube
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
