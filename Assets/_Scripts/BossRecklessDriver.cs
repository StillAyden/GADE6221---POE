using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRecklessDriver : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody rb;

    [Header("Boss Stats")]
    bool isActive = false;
    [SerializeField] float timeActive = 30f;
    [Range(0.1f, 100f)][SerializeField] float speed = 2f;

    [Header("Boss Attack")]
    [SerializeField] float[] lanesXPos = { -2.5f, 0, 2.5f };
    [SerializeField] int selectedLane;

    private void Awake()
    {
        selectedLane = Random.Range(0, 2);
        //gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        this.transform.position = new Vector3((this.transform.position.x + lanesXPos[selectedLane]), 
                                                this.transform.position.y, 
                                                    this.transform.position.z);

        StartCoroutine(BossMechanic());
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, -speed);

    }

    IEnumerator BossMechanic()
    {
        //Change all lane types to plain/flat
        //Choose Lane
        //
        yield return new WaitForSeconds(15f);
        Destroy(this);
    }
}
