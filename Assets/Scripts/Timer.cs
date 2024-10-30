using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer _instance;

    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;

    private float minutes, seconds;

    public static Timer Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is Null !");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        timeRemaining = 0;
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsRunning)
        {
            if(timeRemaining>=0)
            {
                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
       
    }

    public void DisplayResultTime()
    {
        UiManager._instance.result_survivedTime.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void ResetTimer()
    {
        timeRemaining = 0;
        minutes = 0;
        seconds = 0;
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
