using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class MovementSystem : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField] float moveSpeed = 300f;
    [SerializeField] float moveForce = 10f;
    public float jumpForce = 10f;

    [Header("Jump Limit")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool grounded = true;
    [SerializeField] float overlapRadius = 1.05f;

    InputSystem _inputs;
    Rigidbody rb;
    Pickups pickup;
    float moveInput; //Only need 2 axids for forward, backward, left, right movement

    private void OnDisable()
    {
        _inputs.Player.Jump.performed -= x => MovePlayer();
        _inputs.Player.Move.performed -= OnMove;
        _inputs.Player.SlideForceDown.performed -= x => MovePlayer();
        _inputs.Player.Restart.performed -= x => Restart();
    }

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
        _inputs.Player.Restart.performed += x => Restart();

        
        Camera.main.transform.parent = this.transform; //Laxy man's way of attaching camera to Player
    }

    void MovePlayer()
    {
        //Lets the Player jump, as well as force itself down towards the ground
        
        if (_inputs.Player.Jump.triggered && grounded) //Jump Upwards
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
        else if (_inputs.Player.SlideForceDown.triggered)  //Force/Slide downward
        {
            rb.AddForce(moveForce * Vector3.down, ForceMode.Impulse);

        }
    }

    void Restart()
    {
        //By pressing the key "r" you can restart the game from any time frame
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnMove(InputAction.CallbackContext info)
    {
        //Input x = A & D Keys (But saved in the x value of our Vector 2)
        //Save relevent/needed  information reived from input
        moveInput = info.ReadValue<float>();
    }

    private void FixedUpdate() //Constant, No jarring or delays
    {
        //Make Player move here when keyboard input is received
        //Adding velocity to the Rigidbody
        rb.velocity = new Vector3(moveInput * moveSpeed * Time.fixedDeltaTime, rb.velocity.y, rb.velocity.z);
    }

    private void Update()
    {
        grounded = Physics.CheckSphere(transform.position, overlapRadius, groundLayer);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Pickup"))
        {
            pickup = col.gameObject.GetComponent<Pickups>();
        }
    }
    //public IEnumerator JumpBoost()
    //{
    //    Debug.Log("Jump Boost Active");
    //    jumpForce = jumpForce * pickup.JumpBoostAmount;
    //    yield return new WaitForSecondsRealtime(pickup.JumpBoostLength);
    //    jumpForce = jumpForce / pickup.JumpBoostAmount;
    //    Debug.Log("Jump Boost Deactivate");
    //}
}
