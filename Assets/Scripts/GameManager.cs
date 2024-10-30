using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    //script ref declarations
    public EnemySpawner _enemySpawner; //remove enemy from enemybasecontroller
    public TargetObjectSpawner _targetObjectSpawner;
    public PlayerController playerController;
    public PlayerMovement playerMove;
    public PopupWindow popup;

    //game declarations
    public int savedCatCount = 0; // cat == XP
    public int baseCatsRequired = 10;
    public int catsRequiredIncrement = 5;
    public int catsRequiredForNextLevel;

    public int level = 1;
    private int defeatEnemy = 0;

    public GameObject xpFeedback;
    public GameObject TopPanelUi;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Game Manager is Null !");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;

        //initialize catsRequiredForNextLevel
        catsRequiredForNextLevel = baseCatsRequired;
    }


    public void AddSavedCats(int amount)
    {
        savedCatCount += amount;

        //xp manager add experience
        XPManager._instance.AddExperience(savedCatCount * 100);

        GameObject xpFeedbackInstance = Instantiate(xpFeedback);

        // Set the instantiated object as a child of TopPanelUi
        xpFeedbackInstance.transform.SetParent(TopPanelUi.transform, false); // 'false' keeps local scale/position

        // Reset the local position to make sure it's at the desired location relative to the TopPanelUi
        xpFeedbackInstance.transform.localPosition = new Vector3(0, -300, 0);// Adjust this to control exact placement

        // Modify the text component (assuming you're using TextMeshProUGUI)
        TextMeshProUGUI feedbackText = xpFeedbackInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (feedbackText != null)
        {
            feedbackText.text = "Cat Rescued!";  // Set the dynamic text
        }

        Destroy(xpFeedbackInstance, 1f);

        //Ui manager
        UiManager._instance.savedCatCount.text = savedCatCount.ToString();
        UiManager._instance.result_savedCatCount.text = savedCatCount.ToString();
    }

    public void CheckLevelUp()
    {
        level++;

        int catsToCarryOver = savedCatCount / 2;
        savedCatCount = catsToCarryOver;

        catsRequiredForNextLevel += catsRequiredIncrement;

        //new obstacles activation

        _enemySpawner.levelTimer = 0f; // Reset the level timer to 0 to restart the spawn rate
        _enemySpawner.RemoveAllEnemy(); // Clear all enemies from the previous level

        // Restart the enemy spawning process
        StopAllCoroutines(); // Stop any previous spawns
        StartCoroutine(_enemySpawner.SpawnEnemies());
    }

    public void AddDefeatEnemy()
    {
        defeatEnemy++;

        //xp manager add experience
        XPManager._instance.AddExperience(25);

        GameObject xpFeedbackInstance = Instantiate(xpFeedback);

        // Set the instantiated object as a child of TopPanelUi
        xpFeedbackInstance.transform.SetParent(TopPanelUi.transform, false); // 'false' keeps local scale/position

        // Reset the local position to make sure it's at the desired location relative to the TopPanelUi
        xpFeedbackInstance.transform.localPosition = new Vector3(0, -300, 0); // Adjust this to control exact placement

        // Modify the text component (assuming you're using TextMeshProUGUI)
        TextMeshProUGUI feedbackText = xpFeedbackInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (feedbackText != null)
        {
            feedbackText.text = "+25 XP";  // Set the dynamic text
        }

        Destroy(xpFeedbackInstance, 1f);

        //ui manager
        UiManager._instance.defeatEnemyCount.text = defeatEnemy.ToString();
        UiManager._instance.result_defeatEnemyCount.text = defeatEnemy.ToString();
    }

    public void RestartGame()
    {
        savedCatCount = 0;
        level = 1;
        defeatEnemy = 0;

        baseCatsRequired = 10;
        catsRequiredForNextLevel = baseCatsRequired;

        //remove enemy pool
        _enemySpawner.RemoveAllEnemy();
        //restart target object spawner
        _targetObjectSpawner.RestartTargetObjectSpawner();

        //timer reset
        Timer._instance.ResetTimer();

        playerController.transform.position = new Vector3(0.5f, -5.5f, 0f);
        ResetHealth();
        

        //pause
        Time.timeScale = 1;

        //reset UI manager
        UiManager._instance.savedCatCount.text = savedCatCount.ToString();
        UiManager._instance.defeatEnemyCount.text = defeatEnemy.ToString();
        XPManager._instance.CurrentXp = 0;

        //spawn enemy later a few sec so balloon cat can spawn first
        StartCoroutine(spawnEnemyLater());

        //reset upgrades
        playerController.GetComponent<AimShooting>().enabled = false; //reset auto aim shooter upgrade
        playerMove.moveSpeed = 7f; //reset ori player move speed
        UpgradeManager._instance.DeActivateNextWeapon(); // deactivate all flying weapon
    }

    public void ResetHealth()
    {
        playerController.ResetHealth();
    }

    IEnumerator spawnEnemyLater()
    {
        yield return new WaitForSeconds(5f);
    }

    //used in Inspector
    public void PauseGame()
    {
        //pause
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        //unpause
        Time.timeScale = 1f;
    }
}
