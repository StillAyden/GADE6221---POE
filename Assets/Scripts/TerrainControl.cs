using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainControl : MonoBehaviour
{
    [SerializeField] GameObject[] groundPrefabCollection;
    GameObject[] loadedGroundedPrefabs;
    float[] tileSpawnPoints;

    [Header("Spawning/Despawning Object")]
    [SerializeField] float groundPrefabSize = 10f;
    [SerializeField] int prefabsLoaded = 5;
    [SerializeField] float despawnPoint = -10;
    
    [Header("Movement")]
    [SerializeField] float moveSpeed = 1f;
    private void Awake()
    {
        loadedGroundedPrefabs = new GameObject[prefabsLoaded];
        tileSpawnPoints = new float[prefabsLoaded];

        //Populate spawn points
        for (int i = 0; i < prefabsLoaded; i++)
        {
            tileSpawnPoints[i] = i * groundPrefabSize;
        }
    }
    private void Start()
    {
        InitialTileSpawn();
    }

    private void FixedUpdate()
    {
        TileSpawn();
        MoveTiles();
        DespawnTiles();
    }

    GameObject RandomPrefab()
    {
        //Choose random prefab from List
        int random = Random.Range(0, groundPrefabCollection.Length);
        return groundPrefabCollection[random];
    }

    void InitialTileSpawn()
    {
        //Choosing Prefab and populating array
        for (int k = 0; k < loadedGroundedPrefabs.Length; k++)
        {
            if (loadedGroundedPrefabs[k] == null)
            {
                //loadedGroundedPrefabs[k] = RandomPrefab();
                loadedGroundedPrefabs[k] = Instantiate(RandomPrefab());
                loadedGroundedPrefabs[k].transform.position = new Vector3( 0, 0, tileSpawnPoints[k]); 
            }
        }
    }

    void TileSpawn()
    {
        //Check if one of the loaded prefabs has been despawned, then add one
        for (int k = 0; k < loadedGroundedPrefabs.Length; k++)
        {
            if (loadedGroundedPrefabs[k] == null)
            {
                loadedGroundedPrefabs[k] = Instantiate(RandomPrefab());
                loadedGroundedPrefabs[k].transform.position = new Vector3(0, 0, groundPrefabSize * (prefabsLoaded - 1));
            }
        }
    }

    void DespawnTiles()
    {
        //Check if every prefab is infront of "despawnPoint", else destroy it
        for(int k = 0; k < loadedGroundedPrefabs.Length; k++)
        {
            if (loadedGroundedPrefabs[k] != null)
            {
                if (loadedGroundedPrefabs[k].transform.position.z < despawnPoint)
                {
                    Destroy(loadedGroundedPrefabs[k]);
                    loadedGroundedPrefabs[k] = null;
                }
            }
        }
    }

    void MoveTiles()
    {
        //Go through all Loaded prefabs and move them
        for (int k = 0; k < loadedGroundedPrefabs.Length; k++)
        {
            if (loadedGroundedPrefabs[k] != null)
            {
                loadedGroundedPrefabs[k].transform.position = new Vector3(loadedGroundedPrefabs[k].transform.position.x, 
                                                                            loadedGroundedPrefabs[k].transform.position.y, 
                                                                                loadedGroundedPrefabs[k].transform.position.z + moveSpeed * -1 * Time.deltaTime);
            }
        }
    }
}
