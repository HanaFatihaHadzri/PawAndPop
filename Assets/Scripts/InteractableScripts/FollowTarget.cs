using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject indicatorPrefab;
    
    public Vector3 offset;

    private SpriteRenderer _spriteRenderer;
    private float _spriteWidth;
    private float _spriteHeight;

    private Camera _camera;

    private Dictionary<GameObject, GameObject> _targetIndicators = 
        new Dictionary<GameObject, GameObject>(); //hold target indicators

    private void Start()
    {
        _camera = Camera.main;
        _spriteRenderer = indicatorPrefab.GetComponent<SpriteRenderer>();

        var bounds = _spriteRenderer.bounds;
        _spriteHeight = bounds.size.y / 2f;
        _spriteWidth = bounds.size.x / 2f;

        foreach(var target in targets)
        {
            var indicator = Instantiate(indicatorPrefab);
            indicator.SetActive(false);
            _targetIndicators.Add(target, indicator);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach ( KeyValuePair<GameObject,GameObject> entry in _targetIndicators )
        {
            var target = entry.Key;
            var indicator = entry.Value;

            UpdateTarget(target, indicator);
        }

        //GetComponent<RectTransform>().anchoredPosition3D=Camera.main.WorldToScreenPoint(target.transform.position+ offset);
    }

    private void UpdateTarget(GameObject target, GameObject indicator)
    {
        var screenPos = _camera.WorldToScreenPoint(target.transform.position);
        bool isOffScreen = screenPos.x <= 0 || screenPos.x >= 1 ||
            screenPos.y <= 0 || screenPos.y >= 1;

        if(isOffScreen)
        {
            indicator.SetActive(true);
            var spriteSizeInViewPort = _camera.WorldToViewportPoint(new Vector3(_spriteWidth, _spriteHeight, 0))
                - _camera.WorldToViewportPoint(Vector3.zero);

            screenPos.x = Mathf.Clamp(screenPos.x, spriteSizeInViewPort.x, 1 - spriteSizeInViewPort.x);
            screenPos.y = Mathf.Clamp(screenPos.y, spriteSizeInViewPort.y, 1 - spriteSizeInViewPort.y);

            var worldPosition = _camera.ViewportToWorldPoint(screenPos);
            worldPosition.z = 0;
            indicator.transform.position = worldPosition;

            Vector3 direction = target.transform.position - indicator.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            indicator.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
