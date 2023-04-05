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

    InputSystem _Inputs;
    Rigidbody RB;
    Vector2 moveInput; //Only need 1 axids for left, right movement

    private void Awake() //Executed before Start, good for setting veriables
    {
        //Connect Rigidbody component of Player
        RB = GetComponent<Rigidbody>();

        //Create a Input Stystem to use to monitor input
        _Inputs = new InputSystem();

        // _inputs.Enable(); //Enables the entire Input System

        _Inputs.Player.Enable(); //Need to enable the action map on the Input System we have created

        _Inputs.Player.Move.performed += OnMove;

        _Inputs.Player.Jump.performed += x => Jump();

        Camera.main.transform.parent = this.transform; //Laxy man's way of attaching camera to Player
    }

    private void OnMove(InputAction.CallbackContext info)
    {
        //Input x = A & D Keys (But saved in the x value of our Vector 2)
        //Save relevent/needed  information reived from input
        moveInput = info.ReadValue<Vector2>();
    }

    private void FixedUpdate() //Constant, No jarring or delays
    {
        //Make Player move here when keyboard input is received
        //In order to make player move, we will be adding velocity to the Rigidbody
    }

    void Jump()
    {
        if (grounded)
        {
            RB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
