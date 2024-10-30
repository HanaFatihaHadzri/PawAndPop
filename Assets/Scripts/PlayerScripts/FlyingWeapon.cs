using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingWeapon : MonoBehaviour
{
    public GameObject _centerPoint;
    public float curve = 1; //
    public float Radius = 2;//distance shoot point with player
    public float offset = 0;

    

    private void Start()
    {
        _centerPoint = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
       float delta =  _centerPoint.GetComponent<PlayerMovement>().delta;

        transform.position = _centerPoint.transform.position +
                new Vector3(
                    Mathf.Sin(delta + offset),
                   Mathf.Cos(delta + offset),
                    0) * Radius;
    }
}
