using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 2f; //time btw each shot
    public Transform firePoint; // position where bullet will spawn

    private float fireCooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if(fireCooldown <=0f)
        {
            GameObject closestEnemy = FindClosestEnemy();

            if(closestEnemy != null)
            {
                Shoot(closestEnemy.transform);
                fireCooldown = 5f; //reset cooldown
            }
        }
    }
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distance;
            }
        }

        return closestEnemy;
    }

    void Shoot(Transform enemy)
    {
        //instantiate bullet and set target
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if(bulletScript != null)
        {
            bulletScript.SetTarget(enemy);
        }
    }


}
