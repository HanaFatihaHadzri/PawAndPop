using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectSpawner : MonoBehaviour
{
    public GameObject targetObjectPrefabs;
    public Transform[] spawnPoints;
    public List<GameObject> _allTargetInScene;

    public float initialSpawnInterval = 20f; //time btw spawn
    public float minSpawnInterval = 2f; //minimum time btw spawns
    public float spawnIntervalDecrease = 0.5f;
    public float spawnInterval; //current spawn interval

    public int maxCatCount = 3; //max num of cat to spawn

    public AnimationCurve _TargetSpawnRate;

    //TRACK TIMER for debugging
    public float timer;

    private void Start()
    {
        InitializeSpawner();
    }

    public void InitializeSpawner()
    {
        GameObject[] spawnPointObjects =
            GameObject.FindGameObjectsWithTag("CatPickupSpawnPoint");

        _allTargetInScene = new List<GameObject>();
        spawnPoints = new Transform[spawnPointObjects.Length];

        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].transform;
        }

        //initialize current interval
        spawnInterval = initialSpawnInterval;

        StartCoroutine(SpawnTargets());
    }

    private void Update()
    {
        timer = Timer._instance.timeRemaining;
    }

    private IEnumerator SpawnTargets()
    {
        while(true)
        {

            float normalizedTime = timer / 60f; //convert timer in secs to mins
            int spawnCount = 0;

            if (normalizedTime < 5)
            {
                spawnCount = Mathf.Min((int)_TargetSpawnRate.Evaluate(normalizedTime),
                   maxCatCount - _allTargetInScene.Count); //evaluate return 
            }
            else
            {
                spawnCount = 3;
            }

            //spawn multiple cats based on current cat count
            for (int i = 0; i< spawnCount; i++)
            {
                //spawn prefab
                SpawnTargetObject();
            }

            //shorten spawn interval
            //spawnInterval = Mathf.Max(spawnInterval - spawnIntervalDecrease, minSpawnInterval);

            yield return new WaitForSeconds(spawnInterval);

            //to check max prefab spawn according to next level requirement
            //int maxSpawnNum = GameManager._instance.catsRequiredForNextLevel;            
            //if (_allTargetInScene.Count >= maxSpawnNum)
            //{
            //    break;
            //}
        }
    }

    public GameObject GetRandTargetObject() //Applied in EnemyMovement.cs
    {
        if (_allTargetInScene.Count <= 0)
        {
            return null;
        }
        else
        {
            return _allTargetInScene[Random.Range(0, _allTargetInScene.Count)];
        }
    }

    void SpawnTargetObject()
    {
        //to random select spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        //instantiate selected target objetc prefab // can be diff cat breed
        GameObject newTargetObject = Instantiate(targetObjectPrefabs, spawnPoint.position, Quaternion.identity);

        //add to list
        _allTargetInScene.Add(newTargetObject);

    }
    public void targetObjectRemove(GameObject a)
    {
        _allTargetInScene.Remove(a);
    }

    public void RemoveAllTarget()
    {
        foreach (GameObject target in _allTargetInScene)
        {
            Destroy(target);
        }

        _allTargetInScene.Clear();
    }

    public void RestartTargetObjectSpawner()
    {
        RemoveAllTarget();
        InitializeSpawner();
    }
}
