using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    //The Events
    public event Action<GameObject> obstaclePassed;
    public event Action<GameObject> pickupCollected;
    public event Action bossSpawn;
    public event Action bossDefeated;

    //The Event Listeners
    public void onObstaclePassed(GameObject obj)
    {
        obstaclePassed?.Invoke(obj);
    }

    public void onActivatePickup(GameObject obj)
    {
        pickupCollected?.Invoke(obj);
    }

    public void onBossSpawn()
    {
        bossSpawn?.Invoke();
    }

    public void onBossCompleted()
    {
        bossDefeated?.Invoke();
    }
}
