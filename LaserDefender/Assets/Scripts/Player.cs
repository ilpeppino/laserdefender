using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField] float moveSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // This is connected to Edit -> Project Settings -> Input. In here you can define the specs for input devices (keys, mouse, joystick) to move sprites 
        // Saying Horizontal means that deltaX is calculated from the input device movement on the x-axis
        // Time.deltaTime calculates the time between frames (times the Update is called)
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // Debug.Log(deltaX);

        // var is an undefined local variable, which takes the type from the assignment. P.e. deltaX will be a float because GetAxis is a float
        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        // The new position will be moving on x-axis but staying on y-axis
        transform.position = new Vector2(newXPos, newYPos);


    }
}
