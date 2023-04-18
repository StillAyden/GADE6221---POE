using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] PickupType pickupType;
    
    [Header("Score Multiplier")]
    ScoreCounter scoreCounter;
    //[SerializeField] float multiplierIncreaseAmount = 0.5f;
    [SerializeField] float multiplierAmount = 2f;
    [SerializeField] float multiplierActivationLength = 5.0f;

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
                //Invoke("EndScoreMultiplier", 5f);
                StartCoroutine(EndScoreMultiplier());
            }
            else if (pickupType == PickupType.JumpBoost)
            {
                JumpBoost();
            }
        }
    }

    void ScoreMultiplier()
    {
        scoreCounter.multiplier =  multiplierAmount;
    }

    IEnumerator EndScoreMultiplier()
    {
        yield return new WaitForSeconds(multiplierActivationLength);
        scoreCounter.multiplier = 1f;
    }

    void JumpBoost()
    {
        //Not yet implemented
    }

   
}
