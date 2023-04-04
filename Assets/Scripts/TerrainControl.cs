using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainControl : MonoBehaviour
{
    [SerializeField] GameObject[] groundPrefabCollection;
    GameObject[] loadedGroundedPrefabs;

    [SerializeField] float groundPrefabSize = 10f;
    [SerializeField] float moveSpeed = 1f;


    /// <summary>
    GameObject thing1;
    GameObject thing2;
    /// </summary>
    private void Start()
    {
        thing1 = Instantiate(groundPrefabCollection[1]);
        thing2 = Instantiate(groundPrefabCollection[1]);
        Vector3 thing1Size = new Vector3(0, 0, groundPrefabSize);
        thing2.transform.position = thing1.transform.position + thing1Size;
    }

    private void Update()
    {
        thing1.transform.position = new Vector3(thing1.transform.position.x, thing1.transform.position.y, thing1.transform.position.z + moveSpeed * -1 * Time.deltaTime);
    }

    GameObject RandomPrefab()
    {
        int random = Random.Range(0, groundPrefabCollection.Length);

        return groundPrefabCollection[random];
    }

    void SpawnTiles()
    {

    }
}
