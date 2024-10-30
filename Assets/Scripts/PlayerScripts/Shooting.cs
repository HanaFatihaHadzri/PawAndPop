using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject whipPrefab;
    public GameObject reticlePrefab;
    public GameObject _spawnPoint;
    public float Radius = 5f;//distance shoot point with player
   
    public PlayerMovement playerMove;
    public Transform targetEnemy;

    private float minShootInterval = 0.5f;
    private float maxShootInterval = 1f;
    private float shootTimer = 0f;

    // To track the instantiated reticle
    private GameObject reticleInstance;
    public float reticleDelay = 0.1f;  // Delay before shooting after showing reticle
    private float reticleDisplayTimer = 0f;  // Timer for reticle delay

    void Start()
    {
        shootTimer = Random.Range(minShootInterval, maxShootInterval);

        // Instantiate the reticle but keep it inactive initially
        reticleInstance = Instantiate(reticlePrefab);
        reticleInstance.SetActive(false);  // Hide the reticle initially
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerShooting();
    }

    private void Shoot()
    {
        if(targetEnemy != null)
        {
            GameObject whipShoot = Instantiate(whipPrefab, _spawnPoint.transform.position, Quaternion.identity);

            // Get the WhipController component on the instantiated object
            WhipController whipController = 
                whipShoot.GetComponent<WhipController>();

            Vector3 directionToEnemy = (targetEnemy.position - _spawnPoint.transform.position).normalized;

            // Set the direction of the whip
            whipController.SetDirection(directionToEnemy);

            // Hide the reticle after shooting
            reticleInstance.SetActive(false);
        }
        
    }

    private void HandlePlayerShooting()
    {
        shootTimer -= Time.deltaTime;

        if(shootTimer <= 0f)
        {
            //find nearest enemy
            targetEnemy = FindNearestEnemy();

            //shoot if there's enemy within range
            if(targetEnemy != null)
            {
                reticleInstance.SetActive(true);  // Activate the reticle
                reticleInstance.transform.position = targetEnemy.position;  // Position the reticle on the enemy

                // Start the reticle delay timer
                reticleDisplayTimer = reticleDelay;
            }
            else
            {
                // No enemy found, hide the reticle
                reticleInstance.SetActive(false);
            }

            //reset shoot timer
            shootTimer = Random.Range(minShootInterval, maxShootInterval);
        }

        // If the reticle is active and the delay timer is running, count it down
        if (reticleDisplayTimer > 0f)
        {
            reticleDisplayTimer -= Time.deltaTime;

            // Once the delay is over, shoot
            if (reticleDisplayTimer <= 0f && targetEnemy != null)
            {
                Shoot();
            }
        }
    }

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;

        float shortestDistance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance && distanceToEnemy <= Radius)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }
}

