using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public float score = 0;
    public float multiplier = 1;

    private void OnTriggerExit(Collider col)
    {
       if (col.gameObject.layer == 7)
       {
            score += multiplier;
       } 
    }
}
