using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [HideInInspector][Header("References")]
    InputSystem inputs;
    Rigidbody rb;
    SceneManagement sceneManagement;
    UIManager uiManager;
    ScoreCounter scoreCounter;
    TerrainControl terrainControl;

    [Header("Stats")]
    public int healthPoints = 3; 
    int maxHealthPoints = 3;

    [Header("Movement")]
    [SerializeField] float moveForce = 10f;
    [SerializeField] float moveInput;

    [Header("Jump")]
    public float jumpForce;
    [SerializeField] bool isGrounded = false;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float overlapRadius = 1.05f;

    [Header("Sliding")]
    [SerializeField] float slideForce = 10f;
    Coroutine hasPickup = null;

    [Header("Power-Ups")]
    [Range(1, 30)][SerializeField] float timeActive = 3f;
    public string currentPickup = "None";

    [Header("Power-Up: Jump Boost")]
    float defaultJump;
    [SerializeField] float increasedJump = 10f;

    [Header("Power-Up: Score Multiplier")]
    [SerializeField] int defaultMultiplier = 1;
    [SerializeField] int increasedMultiplier = 2;

    [Header("Power-Up: Speed Boost")]
    [SerializeField] float speedMultiplier = 2f;

    [Header("Power-Up: Immunity")]
    public bool isImmunityActive = false;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        instance = this;
        inputs = new InputSystem();
        rb = GetComponent<Rigidbody>();

        sceneManagement = GameObject.FindWithTag("GameManager").GetComponent<SceneManagement>();
        uiManager = GameObject.FindWithTag("GameManager").GetComponent<UIManager>();
        scoreCounter = GameObject.FindWithTag("GameManager").GetComponent<ScoreCounter>();
        terrainControl = GameObject.FindWithTag("GameManager").GetComponent<TerrainControl>();

        defaultJump = jumpForce;
        healthPoints = maxHealthPoints;
        hasPickup = null;

        //for (int k = 0; k < healthBarRef.Length - 1; k++)
        //{
        //    healthBars.Add(healthBarRef[k]);
        //}
    }
    private void OnEnable()
    {
        inputs.Player.Enable();
    }

    private void Start()
    {
        inputs.Player.Move.performed += Move;
        inputs.Player.Jump.performed += x => Jump();
        inputs.Player.Restart.performed += x => Restart();
        //Slide not impletemented yet
        inputs.Player.SlideForceDown.performed += x => Slide();
    }

    private void OnDisable()
    {
        inputs.Player.Disable();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveInput * moveForce * Time.fixedDeltaTime, rb.velocity.y, rb.velocity.z);

        //Increase speed
        terrainControl.moveSpeed += 0.00001f;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, overlapRadius, groundLayer);

        if (healthPoints <= 0)
            OnDeath();

        uiManager.UpdateHealth();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 7)
        {
            OnDeath();
        }
        
        //determinePickup(col.gameObject);
        
    }

    public void determinePickup(GameObject obj)
    {
        if(obj.GetComponent<Pickups>() == true)
        {
            Pickups pickup = obj.gameObject.GetComponent<Pickups>();
            
            if (pickup.pickupType == Pickups.PickupType.ScoreMultiplier)
            {
                if (hasPickup == null)
                    hasPickup = StartCoroutine(ScoreMultiplier());
            }
            else if (pickup.pickupType == Pickups.PickupType.JumpBoost)
            {
                if (hasPickup == null)
                    hasPickup = StartCoroutine(JumpBoost());
            }
            else if (pickup.pickupType == Pickups.PickupType.SpeedBoost)
            {
                if (hasPickup == null)
                    hasPickup = StartCoroutine(SpeedBoost());
            }
            else if (pickup.pickupType == Pickups.PickupType.Immunity)
            {
                if (hasPickup == null)
                    hasPickup = StartCoroutine(Immunity());
            }
            else if (pickup.pickupType == Pickups.PickupType.Health)
            {
                AddHealth();
            }
            Destroy(obj);
        }
    }

    void Move(InputAction.CallbackContext info)
    {
        moveInput = info.ReadValue<float>();
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }
    void Slide()
    {
        rb.AddForce(slideForce * Vector3.down, ForceMode.Impulse);
    }

    public void TakeDamage()
    {
        healthPoints--;
        //healthBars.Remove(healthBars[healthBars.Count - 1]);
    }
    void Restart()
    {
        sceneManagement.ReloadCurrentScene();
    }

    IEnumerator OnDeathTimed()
    {
        uiManager.ShowDeathScreen();
        uiManager.finalScore.text = "Your Score: " + ScoreData.instance.score;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        sceneManagement.MoveToScene(0);
        hasPickup = null;
    }

    void OnDeath()
    {
        sceneManagement.MoveToScene(2);
        hasPickup = null;

        //Save data here
        ScoreData.instance.score = 0;
    }

    #region Pickups

    public IEnumerator ScoreMultiplier()
    {
        currentPickup = "ScoreMultiplier";
        uiManager.UpdatePickupImage();
        //Debug.Log("Start ScoreMultiplier");
        scoreCounter.multiplier = increasedMultiplier;
        yield return new WaitForSecondsRealtime(timeActive);
        scoreCounter.multiplier = defaultMultiplier;
        //Debug.Log("Stop ScoreMultiplier");
        hasPickup = null;
        currentPickup = "None";
        uiManager.UpdatePickupImage();
    }

    public IEnumerator JumpBoost()
    {
        currentPickup = "JumpBoost";
        uiManager.UpdatePickupImage();
        jumpForce = increasedJump;
        yield return new WaitForSecondsRealtime(timeActive);
        jumpForce = defaultJump;
        hasPickup = null;
        currentPickup = "None";
        uiManager.UpdatePickupImage();
    }

    public IEnumerator SpeedBoost()
    {
        currentPickup = "SpeedBoost";
        uiManager.UpdatePickupImage();
        Camera camera = Camera.main;
        //camera.fieldOfView = 75;
        terrainControl.moveSpeed = terrainControl.moveSpeed * speedMultiplier;
        yield return new WaitForSecondsRealtime(timeActive);
        terrainControl.moveSpeed = terrainControl.moveSpeed/speedMultiplier;
        camera.fieldOfView = 60;
        hasPickup = null;
        currentPickup = "None";
        uiManager.UpdatePickupImage();
    }

    public IEnumerator Immunity()
    {
        currentPickup = "Immunity";
        uiManager.UpdatePickupImage();
        //Debug.Log("Start Immunity");
        isImmunityActive = true;
        yield return new WaitForSecondsRealtime(timeActive);
        isImmunityActive = false;
        //Debug.Log("Stop Immunity");
        hasPickup = null;
        currentPickup = "None";
        uiManager.UpdatePickupImage();
    }

    public void AddHealth()
    {
        if (healthPoints < maxHealthPoints)
        {
            healthPoints++;
        }
    }

    #endregion
}
