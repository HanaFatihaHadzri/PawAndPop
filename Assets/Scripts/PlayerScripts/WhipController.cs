using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipController : MonoBehaviour
{
    public int _speed = 5;
    Vector3 shootDirection;
    public AudioSource whipMoveSfx;
    public float destroyTime = 3;

    public void Start()
    {
        // Play the whip sound when instantiated
        if (whipMoveSfx != null)
        {
            whipMoveSfx.Play();
        }
        // Schedule destruction
        Destroy(gameObject, destroyTime);
    }

    public void SetDirection(Vector3 dir)
    {
        shootDirection = new Vector3(
           (Mathf.Abs(dir.x) / (Mathf.Abs(dir.x) + Mathf.Abs(dir.y)))
           * Mathf.Sign(dir.x),
           (Mathf.Abs(dir.y) / (Mathf.Abs(dir.x) + Mathf.Abs(dir.y)))
           * Mathf.Sign(dir.y),
           dir.z);

    }

    void Update()
    {
        transform.position += shootDirection * _speed * Time.deltaTime;
    }

    void OnDestroy()
    {
        // Stop the sound when the whip is destroyed
        if (whipMoveSfx != null && whipMoveSfx.isPlaying)
        {
            whipMoveSfx.Stop();
        }
    }
}
