using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    public GameObject hitEffectPrefab;

    public void SetTarget (Transform enemy)
    {
        target = enemy;
    }

    private void Update()
    {
        if(target != null)
        {
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            //instatiate hit prefab
            GameObject hitInstance = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

            Destroy(hitInstance, .5f); //finish animation then destroy after 0.5 sec

            Destroy(gameObject);
        }
    }
}
