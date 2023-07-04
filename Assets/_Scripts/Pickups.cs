using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public PickupType pickupType;

    public enum PickupType 
    {
        ScoreMultiplier, 
        JumpBoost,
        SpeedBoost,
        Immunity,
        Health
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 2, 0));
    }
}
