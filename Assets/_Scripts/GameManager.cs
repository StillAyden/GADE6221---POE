 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    ScoreCounter scoreCounter;

    [Header("Rounds")]
    [Range(10, 1000)][SerializeField] int levelLength;
    int levelScore;

    [Header("Boss Orb")]
    [SerializeField] GameObject bossOrb;
    public Transform initialSpawn;
    public bool isOrbBossActive;

    //[Header("Reckless Driver")]
    //[SerializeField] GameObject RecklessDriver;
    private void Awake()
    {
        //DontDestroyOnLoad(this);
        scoreCounter = GetComponent<ScoreCounter>();

        levelScore = 0;
        isOrbBossActive = false;
    }

    private void Update()
    {
        CheckAndSpawnOrbBoss();
        ChangeLevel();
    }

    public void CheckAndSpawnOrbBoss()
    {
        if (!isOrbBossActive)
        {
            if (ScoreData.instance.score != 0)
            {
                if (ScoreData.instance.score == levelLength || ScoreData.instance.score == levelLength + 1 || ScoreData.instance.score == levelLength - 1)
                {
                    isOrbBossActive = true;
                    //Debug.Log("SpawnBoss");
                    Instantiate(bossOrb, initialSpawn.position, Quaternion.identity);
                }
            }
        }
    }
    
    public void ChangeLevel()
    {
        if ((ScoreData.instance.score == levelLength * 2) && (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("WorldControlTest")))
        {
            SceneManager.LoadScene("Level_2");
            incremementLevelScore();
        }
        else if ((ScoreData.instance.score == levelLength * 2) && (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_2")))
        {
            SceneManager.LoadScene("WorldControlTest");
            incremementLevelScore();
        }
    }

    public void incremementLevelScore()
    {
        levelScore++;
    }
}
