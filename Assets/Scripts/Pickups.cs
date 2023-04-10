using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] PickupType pickupType;
    
    [Header("Score Multiplier")]
    ScoreCounter scoreCounter;
    [SerializeField] float multiplierIncreaseAmount = 0.5f;

    [Header("JumpBoost")]
    [SerializeField] float JumpBoostAmount;
    

    private void Awake()
    {
        GameObject gameControl = GameObject.Find("GameControl");
        scoreCounter = gameControl.GetComponent<ScoreCounter>();
    }
    public enum PickupType 
    {
        ScoreMultiplier, 
        JumpBoost
    };

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Destroy(this.gameObject);
            if(pickupType == PickupType.ScoreMultiplier)
            {
                ScoreMultiplier();
            }
            else if (pickupType == PickupType.JumpBoost)
            {
                JumpBoost();
            }
        }
    }

    void ScoreMultiplier()
    {
        scoreCounter.multiplier = scoreCounter.multiplier + multiplierIncreaseAmount;
    }

    void JumpBoost()
    {

    }
}
