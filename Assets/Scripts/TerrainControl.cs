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
    public float moveSpeed = 1f;
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
        TileManagement();
    }

    GameObject RandomPrefab()
    {
        //Choose random prefab from List
        int random = Random.Range(0, groundPrefabCollection.Length);
        return groundPrefabCollection[random];
    }

    void InitialTileSpawn()
    {
        //Choosing Floor prefab and populating array
        //*Note: first few tiles will have no obstacles
        for (int k = 1; k < loadedGroundedPrefabs.Length; k++)
        {
            if (loadedGroundedPrefabs[k] == null)
            {
                loadedGroundedPrefabs[k] = Instantiate(groundPrefabCollection[0]);
                loadedGroundedPrefabs[k].transform.position = new Vector3( 0, 0, tileSpawnPoints[k]);
                loadedGroundedPrefabs[k].layer = 6;
            }
        }
    }

    void TileManagement()
    {
        for (int k = 0; k < loadedGroundedPrefabs.Length; k++)
        {
            if (loadedGroundedPrefabs[k] == null)
            {
                TileSpawn(k);
            }
            else
            {
                MoveTiles(k);
                DespawnTiles(k);
            }
            
        }
        
    }
    void TileSpawn(int k)
    {
        //add prefab if one is despawned
        loadedGroundedPrefabs[k] = Instantiate(RandomPrefab());
        loadedGroundedPrefabs[k].transform.position = new Vector3(0, 0, groundPrefabSize * (prefabsLoaded - 1));
        loadedGroundedPrefabs[k].layer = 6;
    }

    void DespawnTiles(int k)
    {
        //Check if every prefab is infront of "despawnPoint", else destroy it
        if (loadedGroundedPrefabs[k].transform.position.z < despawnPoint)
        {
            Destroy(loadedGroundedPrefabs[k]);
            loadedGroundedPrefabs[k] = null;
        }
    }

    void MoveTiles(int k)
    {
        //Move specific tile in loaded array
        loadedGroundedPrefabs[k].transform.position = new Vector3(loadedGroundedPrefabs[k].transform.position.x,
                                                                    loadedGroundedPrefabs[k].transform.position.y,
                                                                        loadedGroundedPrefabs[k].transform.position.z + moveSpeed * -1 * Time.deltaTime);
    }
}
