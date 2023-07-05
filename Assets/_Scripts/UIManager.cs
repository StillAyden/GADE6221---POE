using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //[SerializeField] Text displayScore;
    ScoreCounter scoreCounter;
    PlayerManager playerManager;

    [Header("HUD")]
    public Canvas HUD;
    public Text score;
    [SerializeField] Image[] healthBarRef = new Image[3];
    [SerializeField] Image pickupImage;
    [SerializeField] Sprite[] pickupIcons;


    [Header("Death Screen")]
    public Canvas deathScreen;
    public Text finalScore;

    private void Awake()
    {

        scoreCounter = GetComponent<ScoreCounter>();
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

        HUD.gameObject.SetActive(true);
        DontDestroyOnLoad(HUD);
        DontDestroyOnLoad(deathScreen);
    }

    public void ShowDeathScreen()
    {
        HUD.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
    }
    public void ShowHUD()
    {
        HUD.gameObject.SetActive(true);
        deathScreen.gameObject.SetActive(false);
    }

    public void UpdateHealth()
    {
        if (playerManager.healthPoints == 3)
        {
            healthBarRef[0].gameObject.SetActive(true);
            healthBarRef[1].gameObject.SetActive(true);
            healthBarRef[2].gameObject.SetActive(true);
        }
        else if (playerManager.healthPoints == 2)
        {
            healthBarRef[0].gameObject.SetActive(true);
            healthBarRef[1].gameObject.SetActive(true);
            healthBarRef[2].gameObject.SetActive(false);
        }
        else if (playerManager.healthPoints == 1)
        {
            healthBarRef[0].gameObject.SetActive(true);
            healthBarRef[1].gameObject.SetActive(false);
            healthBarRef[2].gameObject.SetActive(false);
        }
        else if (playerManager.healthPoints == 0)
        {
            healthBarRef[0].gameObject.SetActive(false);
            healthBarRef[1].gameObject.SetActive(false);
            healthBarRef[2].gameObject.SetActive(false);
        }
    }

    public void UpdatePickupImage()
    {
        if(playerManager.currentPickup == "None")
        {
            pickupImage.gameObject.SetActive(false);
        }
        else if (playerManager.currentPickup == "JumpBoost")
        {
            pickupImage.gameObject.SetActive(true);
            pickupImage.sprite = pickupIcons[1];
        }
        else if (playerManager.currentPickup == "ScoreMultiplier")
        {
            pickupImage.gameObject.SetActive(true);
            pickupImage.sprite = pickupIcons[2];
        }
        else if (playerManager.currentPickup == "SpeedBoost")
        {
            pickupImage.gameObject.SetActive(true);
            pickupImage.sprite = pickupIcons[4];
        }
        else if (playerManager.currentPickup == "Immunity")
        {
            pickupImage.gameObject.SetActive(true);
            pickupImage.sprite = pickupIcons[3];
        }
        else if (playerManager.currentPickup == "Health")
        {
            pickupImage.gameObject.SetActive(true);
            pickupImage.sprite = pickupIcons[0];
        }
    }

    public void SaveData()
    {

    }

}
