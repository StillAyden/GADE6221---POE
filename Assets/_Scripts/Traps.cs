using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    PlayerManager playerManager;
    Collider thisCollider;

    private void Awake()
    {
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        thisCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && playerManager.isImmunityActive == false)
        {   
            playerManager.TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
