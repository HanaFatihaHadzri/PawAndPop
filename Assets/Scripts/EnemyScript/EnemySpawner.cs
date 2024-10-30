using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; //stores enemy prefab variations
    public Transform[] spawnPoints; //stores spawn point transforms
    public List<GameObject> _allEnemyInScene; //keeps track of enemies currently spawned

    //MANAGE SPAWN TIMING
    public float spawnInterval = 5f; //time btw spawns

    //MANAGE HOW MANY ENEMIES SPAWNED AT ANY TIME
    public int maxEnemyCount = 5; //max enemy count to spawn

    //animation curve that determines enemy spawn rate changes over time
    public AnimationCurve _EnemySpawnRate;

    //TRACK TIMER for debugging
    public float timer;
    public float levelTimer = 0f; // Tracks time for current level


    void Start()
    {
        InitializeSpawner();
    }

    public void InitializeSpawner()
    {
        GameObject[] spawnPointObjects =
            GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        _allEnemyInScene = new List<GameObject>();
        spawnPoints = new Transform[spawnPointObjects.Length];

        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].transform;
        }

        // Start the spawn process
        StartCoroutine(SpawnEnemies());
    }

    void SpawnEnemy()
    {
        //to random select spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        //random prefabs from the array
        int randomPrefabIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject randomEnemyPrefab = enemyPrefabs[randomPrefabIndex];

        //instantiate selected enemy prefab
        GameObject newEnemy = Instantiate(randomEnemyPrefab, spawnPoint.position, Quaternion.identity);

        //add to list
        _allEnemyInScene.Add(newEnemy);
    }

    private void Update()
    {
        timer = Timer._instance.timeRemaining;
    }

    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float normalizedTime = timer / 60f; //convert timer in secs to mins
            int spawnCount = 0;
            
            if (normalizedTime < 5)
            {
                spawnCount = Mathf.Min((int)_EnemySpawnRate.Evaluate(normalizedTime),
                   maxEnemyCount - _allEnemyInScene.Count); //evaluate return 
            }
            else
            {
                spawnCount = 10;
            }
            
            // Spawn multiple enemies based on currentEnemyCount
            for (int i = 0; i < spawnCount; i++)
            //spawn according to the count
            {
                SpawnEnemy();
            }
            // Wait for the next spawn based on the updated interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void EnemyRemove(GameObject enemy)
    {
        _allEnemyInScene.Remove(enemy);
    }

    public void RemoveAllEnemy()
    {
        foreach ( var enemy in _allEnemyInScene)
        {
            Destroy(enemy);
        }

        _allEnemyInScene.Clear();
    }

}
