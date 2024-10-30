using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    public static ChangeSceneManager instance;

    public static ChangeSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Game Manager is Null !");
            }
            return instance;
        }
    }
    private void Awake()
    {
       instance = this;
        
       //DontDestroyOnLoad(gameObject);
    }

    public void StartGameBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("1-1");
    }

    public void QuitToMainScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScreen");
    }
}
