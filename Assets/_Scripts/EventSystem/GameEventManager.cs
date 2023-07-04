using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;

    private void Awake()
    {
        instance = this;
    }

    //Subscribe to Events
    private void Start()
    {
        GameEventSystem.instance.obstaclePassed += increaseScore;
        GameEventSystem.instance.pickupCollected += activatePickup;
        GameEventSystem.instance.bossSpawn += spawnBoss;
        GameEventSystem.instance.bossDefeated += increaseLevelScore;
    }

    //Methods to execute
    void increaseScore(GameObject obj)
    {
        if (obj.layer == 7)
        {
            ScoreCounter.instance.increaseScore();
        }
    }
    void activatePickup(GameObject obj)
    {
        PlayerManager.instance.determinePickup(obj);
    }
    void spawnBoss()
    {

    }
    void increaseLevelScore()
    {
        GameManager.instance.incremementLevelScore();
    }

    private void OnDisable()
    {
        GameEventSystem.instance.obstaclePassed -= increaseScore;
        GameEventSystem.instance.pickupCollected -= activatePickup;
        GameEventSystem.instance.bossSpawn -= spawnBoss;
        GameEventSystem.instance.bossDefeated -= increaseLevelScore;
    }

    private void OnDestroy()
    {
        GameEventSystem.instance.obstaclePassed -= increaseScore;
        GameEventSystem.instance.pickupCollected -= activatePickup;
        GameEventSystem.instance.bossSpawn -= spawnBoss;
        GameEventSystem.instance.bossDefeated -= increaseLevelScore;
    }
}
