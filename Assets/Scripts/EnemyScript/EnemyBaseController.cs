using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseController : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject hitEffectPrefab;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected void DestroyEnemy()
    {
        GameManager.Instance._enemySpawner.EnemyRemove(gameObject);
        Destroy(gameObject);
    }

    protected void TriggerVibrationPattern()
    {
        long[] pattern = { 0, 100, 200, 300 };
        Vibration.VibrateAndroid(pattern, -1);
    }

}
