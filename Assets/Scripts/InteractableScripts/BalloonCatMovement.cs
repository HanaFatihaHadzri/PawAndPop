using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCatMovement : MonoBehaviour
{
    public float moveSpeed = 1.2f; //movement speed upwards
    public float swaySpeed = .05f; //speed of swaying motion
    public float swayAmount = .05f; //amplitude of the sway(how much it sways left & right)
    public float swayStartDelay; //time or distance bfr swaying starts

    public float swayOffSet;

    private bool isSwaying = false;
    private float initialPositionY;

    private void Start()
    {
        //record starting y pos to calculate distance moved
        initialPositionY = transform.position.y;

        //randomize starting offset for variety in swaying
        swayOffSet = Random.Range(0, (Mathf.PI * 2));
        swayStartDelay = Random.Range(5, 8);
    }

    void Update()
    {
        //calculate distance moved upward
        float distanceMoved = Mathf.Abs(transform.position.y - initialPositionY);
        //check if we moved far enough to start swaying
        if(distanceMoved >= swayStartDelay)
        {
            isSwaying = true; //start swaying after delay
        }

        //move upward
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);

        if(isSwaying)
        {
            //calculate swaying using a sin wave
            float sway = Mathf.Sin(Time.time * swaySpeed + swayOffSet) * swayAmount;
            print(sway);
            
            //apply horizontal swaying to position
            transform.position += new Vector3(sway, 0, 0);
        }
    }
}
