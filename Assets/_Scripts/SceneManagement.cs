using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] Canvas canvasReference;

    [Header("Leaderboard")]
    [SerializeField] Text LeaderboardScores;

    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Leaderboard"))
        {
            LeaderboardScores.text = data_SaveLoad.instance.GetScoreData();
        }
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MoveToScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
        Time.timeScale = 1f;

        if (SceneID == 0)
        {
            if (GameObject.FindWithTag("GameManager"))
            {
                Destroy(GameObject.FindWithTag("GameManager"));
            }

            if (GameObject.FindWithTag("Player"))
            {
                Destroy(GameObject.FindWithTag("Player"));
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowReferences()
    {
        canvasReference.gameObject.SetActive(true);
    }

    public void HideReferences()
    {
        canvasReference.gameObject.SetActive(false);
    }

    public void GoToLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
