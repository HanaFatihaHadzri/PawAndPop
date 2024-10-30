using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager _instance;

    public TMP_Text savedCatCount;
    public TMP_Text defeatEnemyCount;

    public TMP_Text result_savedCatCount;
    public TMP_Text result_defeatEnemyCount;

    public TMP_Text result_survivedTime;
    public TMP_Text result_levelReached;

    public TMP_Text timerText;

    public GameObject TempLevelUp_panel;
    //public UpgradeUI upgradeUi;
    //public float panelDuration = 2f;

    public GameObject gameOverScreen;


    public static UiManager Instance
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
        TempLevelUp_panel.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void ShowLevelupPanel()
    {
        GameManager._instance.PauseGame(); // pause the game
        TempLevelUp_panel.SetActive(true); //show level up panel
    }

    public void CloseLevelupPanel()
    {
        GameManager._instance.UnPauseGame(); //unpause the game
        TempLevelUp_panel.SetActive(false); //close level up panel        
    }
    
    //public void levelUpPanel()
    //{
    //    StartCoroutine(levelUpPanelActive());
    //}

   //IEnumerator levelUpPanelActive()
   //{
   //    TempLevelUp_panel.SetActive(true);
   //    yield return new WaitForSeconds(panelDuration);
   //    TempLevelUp_panel.SetActive(false);
   //}

    public void GameOver()
    {
        //play sfx
        AudioManager.instance.Play("GameOver");
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        Timer._instance.DisplayResultTime();
        result_levelReached.text = GameManager._instance.level.ToString();
    }
}
