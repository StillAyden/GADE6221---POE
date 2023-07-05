 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Rounds")]
    [Range(10, 1000)][SerializeField] int levelLength;
    [SerializeField] float levelLengthInSeconds;
    //int levelScore;
    bool isHalfway = false;
    bool isEnd = false;
    Coroutine timerCoroutine = null; 

    [Header("Boss Orb")]
    [SerializeField] GameObject bossOrb;
    public Transform initialSpawn;
    public bool isOrbBossActive;

    [Header("Reckless Driver")]
    [SerializeField] GameObject recklessDriver;
    [SerializeField] Transform driverInitialSpawn;
    bool isDriverActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(this);

        if (ScoreData.instance != null)
        {
            ScoreData.instance.levelScore = 0;
        }
        isOrbBossActive = false;
        isDriverActive = false;
    }

    private void Update()
    {
        if (timerCoroutine == null)
        {
            timerCoroutine = StartCoroutine(halfLevelTimer());
        }
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
                    Instantiate(bossOrb, initialSpawn.position, Quaternion.identity);
                }
            }
        }
    }

    void SpawnOrbBoss()
    {
        if (!isOrbBossActive)
        {
            isOrbBossActive = true;
            Instantiate(bossOrb, initialSpawn.position, Quaternion.identity);
        }
    }

    void SpawnRecklessDriver()
    {
        if (!isDriverActive)
        {
            isDriverActive = true;
            Instantiate(recklessDriver, driverInitialSpawn.position, Quaternion.identity);
        }
    }
    
    public void ChangeLevel()
    {
        this.GetComponent<TerrainControl>().enabled = false;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("WorldControlTest"))
        {
            SceneManager.LoadScene("Level_2");
            incremementLevelScore();
            this.GetComponent<TerrainControl>().enabled = true;
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_2"))
        {
            SceneManager.LoadScene("WorldControlTest");
            incremementLevelScore();
            this.GetComponent<TerrainControl>().enabled = true;
        }
    }

    public void incremementLevelScore()
    {
        ScoreData.instance.levelScore++;
    }

    public IEnumerator halfLevelTimer()
    {
        yield return new WaitForSeconds(levelLengthInSeconds);
        isHalfway = true;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("WorldControlTest"))
        {
            SpawnOrbBoss();
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_2"))
        {
            SpawnRecklessDriver();
            yield return new WaitForSeconds(3f);
            isDriverActive = false;
            SpawnRecklessDriver();
            yield return new WaitForSeconds(3f);
            isDriverActive = false;
            SpawnRecklessDriver();
            yield return new WaitForSeconds(3f);
            isDriverActive = false;
            SpawnRecklessDriver();
        }

        yield return new WaitForSeconds(levelLengthInSeconds);
        isEnd = true;
        timerCoroutine = null;

        ChangeLevel();
    }
}
