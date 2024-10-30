using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 2f; //time btw each shot
    public Transform firePoint; // position where bullet will spawn

    private float fireCooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            GameObject closestPlayer = FindClosestPlayer();

            if (closestPlayer != null)
            {
                Shoot(closestPlayer.transform);
                fireCooldown = 5f; //reset cooldown
            }
        }
    }

    GameObject FindClosestPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject closestPlayer = null;
        float minDistance = Mathf.Infinity;

        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < minDistance)
        {
            closestPlayer = player;
            minDistance = distance;
        }

        return closestPlayer;
    }

    void Shoot(Transform player)
    {
        //instantiate bullet and set target
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetTarget(player);
        }
    }
}
