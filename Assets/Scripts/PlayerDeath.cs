using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] Canvas HUD;
    [SerializeField] Canvas deathScreen;
    [SerializeField] Text score;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 7)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        GameObject gameControl = GameObject.Find("GameControl");
        TerrainControl terrainControl = gameControl.GetComponent<TerrainControl>();
        ScoreCounter scoreCounter = gameControl.GetComponent<ScoreCounter>();

        terrainControl.moveSpeed = 0f;
        HUD.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
        score.text = "Your Score: " + scoreCounter.score;
        //Time.timeScale = 0;
        Invoke("ChangeScene", 3);
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("DeathScreen");
    }
}
