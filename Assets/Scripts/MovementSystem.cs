using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MovementSystem : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField] float jumpForce = 7.5f;
    [SerializeField] float movespeed = 300f;
    [SerializeField] float moveForce = 10f;

    [Header("Jump Limit")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool grounded = true;
    [SerializeField] float overlapRadius = 1.05f;

    InputSystem _inputs;
    Rigidbody rb;
    float moveInput; //Only need 2 axids for forward, backward, left, right movement
    bool doubleJump = true;

    private void Awake() //Executed before Start, good for setting veriables
    {
        //Connect Rigidbody component of Player
        rb = GetComponent<Rigidbody>();

        //Create a Input Stystem to use to monitor input
        _inputs = new InputSystem();

        // _inputs.Enable(); //Enables the entire Input System

        _inputs.Player.Enable(); //Need to enable the action map on the Input System we have created

        _inputs.Player.Jump.performed += x => MovePlayer();
        _inputs.Player.Move.performed += OnMove;
        _inputs.Player.SlideForceDown.performed += x => MovePlayer();

        Camera.main.transform.parent = this.transform; //Laxy man's way of attaching camera to Player
    }

    void MovePlayer()
    {
        
        if (_inputs.Player.Jump.triggered) //Move Forward 
        {
            rb.AddForce(moveForce * Vector3.up, ForceMode.Impulse);
        }
        else if (_inputs.Player.SlideForceDown.triggered)  //Move Backward
        {
            rb.AddForce(moveForce * Vector3.down, ForceMode.Impulse);

        }
    }

    private void OnMove(InputAction.CallbackContext info)
    {
        //Input x = A & D Keys (But saved in the x value of our Vector 2)
        //Input y = W & S Keys (But saved in the y value of our Vector 2)
        //Save relevent/needed  information reived from input
        moveInput = info.ReadValue<float>();
    }

    private void FixedUpdate() //Constant, No jarring or delays
    {
        //Make Player move here when keyboard input is received
        //In order to make player move, we will be adding velocity to the Rigidbody
        rb.velocity = new Vector3(moveInput * movespeed * Time.fixedDeltaTime, rb.velocity.y, rb.velocity.z);
    }

    private void Update()
    {
        grounded = Physics.CheckSphere(transform.position, overlapRadius, groundLayer);
    }
}
