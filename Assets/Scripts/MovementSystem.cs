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

    [Header("Jump Limit")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool grounded = true;
    [SerializeField] float overlapRadius = 1.05f;

    InputSystem _inputs;
    Rigidbody rb;
    Vector2 moveInput; //Only need 2 axids for forward, backward, left, right movement
    bool doubleJump = true;

    private void Awake() //Executed before Start, good for setting veriables
    {
        //Connect Rigidbody component of Player
        rb = GetComponent<Rigidbody>();

        //Create a Input Stystem to use to monitor input
        _inputs = new InputSystem();

        // _inputs.Enable(); //Enables the entire Input System

        _inputs.Player.Enable(); //Need to enable the action map on the Input System we have created

        _inputs.Player.Move.performed += OnMove;

        _inputs.Player.Jump.performed += x => Jump();

        Camera.main.transform.parent = this.transform; //Laxy man's way of attaching camera to Player
    }

    void Jump()
    {
        if (grounded || doubleJump)
        {
            if (grounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doubleJump = true;
            }
            else if (doubleJump & !grounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doubleJump = false;
            }
            else if (grounded && !doubleJump)
            {
                doubleJump = true;
            }

        }
    }

    private void OnMove(InputAction.CallbackContext info)
    {
        //Input x = A & D Keys (But saved in the x value of our Vector 2)
        //Input y = W & S Keys (But saved in the y value of our Vector 2)
        //Save relevent/needed  information reived from input
        moveInput = info.ReadValue<Vector2>();
    }

    private void FixedUpdate() //Constant, No jarring or delays
    {
        //Make Player move here when keyboard input is received
        //In order to make player move, we will be adding velocity to the Rigidbody
        rb.velocity = new Vector3(moveInput.x * movespeed * Time.fixedDeltaTime, rb.velocity.y, moveInput.y * movespeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        grounded = Physics.CheckSphere(transform.position, overlapRadius, groundLayer);
    }


    private void OnDisable()
    {
        //Unsubscribe to the events for safety
        _inputs.Player.Move.performed -= OnMove;

        _inputs.Player.Jump.performed -= x => Jump();
    }
}
