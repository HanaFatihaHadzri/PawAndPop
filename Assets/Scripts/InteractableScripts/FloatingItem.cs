using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    float delta = 0;
    float range = 0.15f;
    float speed = 1.8f;
    Vector3 startPos;
    Vector3 startScale;
    public GameObject myShadow;
    float min_scale = 0.5f;
    private void Start()
    {
        startPos = transform.localPosition;
        startScale = myShadow.transform.localScale;
    }
    private void Update()
    {
        delta += Time.deltaTime * speed;
        transform.localPosition = startPos + new Vector3(0, Mathf.Sin(delta) * range, 0);
        myShadow.transform.localScale = startScale *(min_scale+((1- min_scale)*(1-((Mathf.Sin(delta)+1)/2 ))));
    }
}
