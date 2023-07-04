using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance;
    UIManager ui;

    //public int score = 0;
    public int multiplier = 1;

    private void Awake()
    {
        instance = this;
        ui = GetComponent<UIManager>();
    }

    private void OnTriggerExit(Collider col)
    {
       //if (col.gameObject.layer == 7)
       //{
       //     increaseScore();
       //} 
    }

    public void increaseScore()
    {
        ScoreData.instance.score += multiplier;
        ui.score.text = "SCORE: " + ScoreData.instance.score;
    }
}
