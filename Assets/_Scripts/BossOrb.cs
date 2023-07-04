using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOrb : MonoBehaviour
{
    GameManager gameManager;

    [Header("BossStats")]
    bool isActive = false;
    [SerializeField] float timeActive = 30f;
    [Range(0.1f, 10f)][SerializeField] float speed = 2f;
    [SerializeField] float distanceFromPlayer = 15f;

    [Header("Boss Attack")]
    [SerializeField] float[] lanesXPos = { -2.5f, 0, 2.5f };
    [SerializeField] int selectedLane;
    [SerializeField] GameObject slimeTrap;

    Vector3 activePosition;
    bool bossInPosition = false;
    Coroutine bossRountine = null;

    [Range(0, 1)][SerializeField] float percentageComplete;
    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        activePosition = new Vector3 (this.transform.position.x, this.transform.position.y, distanceFromPlayer);
        bossInPosition = false;
        bossRountine = null;
        isActive = false;
    }

    private void FixedUpdate()
    {
        
        if (bossRountine == null)
        {
            if (isActive == false)
            {
                StartCoroutine(BossTimer());
            }
            bossRountine = StartCoroutine(BossMechanic());
        }
    }

    IEnumerator BossMechanic()
    {
        if (bossInPosition == false)
        {
            MoveBossToPosition(activePosition);
        }

        if (bossInPosition == true)
        {
            yield return new WaitForSeconds(2f);
            int currentLane = selectedLane;
            StartCoroutine(ChooseLane());

            if (currentLane != selectedLane)
            {
                //Move to lane 
                activePosition = new Vector3(lanesXPos[selectedLane], this.transform.position.y, this.transform.position.z);

                if (bossInPosition == true)
                {
                    DropTrap();
                }
                bossInPosition = false;
                yield return new WaitForSeconds(2f);
            }
        }
        bossRountine = null;
    }

    void MoveBossToPosition(Vector3 endPos)
    {
        transform.position = Vector3.Lerp(gameManager.initialSpawn.position, endPos, percentageComplete);
        
        if(percentageComplete < 1)
            percentageComplete += 0.001f * speed;

        if (percentageComplete >= 1)
        {
            bossInPosition = true;
        }

    }

    void DropTrap()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                //Drop trap
                Instantiate(slimeTrap, hit.point, Quaternion.identity, hit.collider.gameObject.transform);
            }
        }
    }

    IEnumerator ChooseLane()
    {
        selectedLane = Random.Range(0, 3);
        yield return new WaitForSecondsRealtime(5f);
    }

    IEnumerator BossTimer()
    {
        isActive = true;
        yield return new WaitForSeconds(timeActive);
        isActive = false;
        MoveBossToPosition(new Vector3(this.transform.position.x, this.transform.position.y, -distanceFromPlayer));
        //Debug.Log("Boss Is Done");
        gameManager.isOrbBossActive = false;
        Destroy(this.gameObject);
    }
}
