using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    /*This event trigger needs to be placed on: 
        PlayerManager, GameManager, */  


    private void OnTriggerEnter(Collider col)
    {
        if (this.CompareTag("Player"))
        {
            GameEventSystem.instance.onActivatePickup(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        GameEventSystem.instance.onObstaclePassed(col.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        GameEventSystem.instance.onBossSpawn();
    }

}
