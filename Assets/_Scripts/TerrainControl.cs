using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainControl : MonoBehaviour
{
    [SerializeField] bool canSpawn = true;

    [Header("Level 1")]
    [SerializeField] GameObject[] firstLevelGroundPrefabCollection;

    [Header("Level 2")]
    [SerializeField] GameObject[] secondLevelGroundPrefabCollection;

    [Header("GameManagement")]
    GameObject[] currentLevelGroundPrefabCollection;
    [SerializeField] GameObject[] loadedGroundedPrefabs;
    float[] tileSpawnPoints;

    [Header("Spawning/Despawning Object")]
    [SerializeField] float groundPrefabSize = 10f;
    [SerializeField] int prefabsLoaded = 5;
    [SerializeField] float despawnPoint = -10;
    int previousPrefabIndex;
    
    [Header("Movement")]
    public float moveSpeed = 1f;

    private void OnEnable()
    {
        GetPrefabs();
        loadedGroundedPrefabs = new GameObject[prefabsLoaded];
        tileSpawnPoints = new float[prefabsLoaded];

        //Populate spawn points
        for (int i = 0; i < prefabsLoaded; i++)
        {
            tileSpawnPoints[i] = i * groundPrefabSize;
        }

        InitialTileSpawn();
    }

    private void FixedUpdate()
    {
        TileManagement();
    }

    GameObject RandomPrefab()
    {
        //Choose random prefab from List (*Note: 2 of the same prefabs should not spawn)
        //First Prefab is platform without obstacles, last Prefab is an empty Prefab
        int random = Random.Range(0, currentLevelGroundPrefabCollection.Length - 1);
        while (previousPrefabIndex == random)
        {
            random = Random.Range(0, currentLevelGroundPrefabCollection.Length - 1);
        }
        
        previousPrefabIndex = random;
        return currentLevelGroundPrefabCollection[random];
    }

    void InitialTileSpawn()
    {
        //Choosing Floor prefab and populating array
        //*Note: first few tiles will have no obstacles
        for (int k = 0; k < loadedGroundedPrefabs.Length - 1; k++)
        {
            if (loadedGroundedPrefabs[k] == null)
            {
                loadedGroundedPrefabs[k] = Instantiate(currentLevelGroundPrefabCollection[0]);
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
                if (canSpawn)
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
        loadedGroundedPrefabs[k].transform.position = new Vector3(0, 0, tileSpawnPoints[prefabsLoaded - 1]);
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

    void GetPrefabs()
    {

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("WorldControlTest"))
        {
            currentLevelGroundPrefabCollection = new GameObject[firstLevelGroundPrefabCollection.Length];
            for (int k = 0; k < firstLevelGroundPrefabCollection.Length - 1; k++)
            {
                currentLevelGroundPrefabCollection[k] = firstLevelGroundPrefabCollection[k];
            }
            
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_2"))
        {
            currentLevelGroundPrefabCollection = new GameObject[secondLevelGroundPrefabCollection.Length];
            for (int k = 0; k < secondLevelGroundPrefabCollection.Length - 1; k++)
            {
                currentLevelGroundPrefabCollection[k] = secondLevelGroundPrefabCollection[k];
            }
        }
    }

    private void OnDisable()
    {
        previousPrefabIndex = 0;
    }
}
