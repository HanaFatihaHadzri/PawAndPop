using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public static XPManager _instance;

    //xp manager declaration
    public float CurrentXp;
    public float TargetXp;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private Image XpProgressBar;

    public static XPManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("XP Manager is Null !");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentXp = 0;
        TargetXp = CalculateTargetXp(GameManager._instance.catsRequiredForNextLevel);
    }

    // Update is called once per frame
    void Update()
    {
        ExperienceController();
    }

    public void AddExperience(float amount)
    {
        CurrentXp += amount;
        ExperienceController();
    }

    public void ExperienceController()
    {
        LevelText.text = "Lv " + GameManager._instance.level.ToString();

        float barFillAmount = Mathf.Clamp01(CurrentXp / TargetXp);
        XpProgressBar.fillAmount = barFillAmount;

        if(CurrentXp >= TargetXp)
        {
            LevelUp();
        }

    }

    void LevelUp()
    {
        GameManager._instance.CheckLevelUp();
        CurrentXp = 0;
        TargetXp = CalculateTargetXp(GameManager._instance.catsRequiredForNextLevel);

        //play sfx
        AudioManager.instance.Play("LevelUp");

        //ui panel level up
        UiManager._instance.ShowLevelupPanel();
    }

    float CalculateTargetXp(int level)
    {
        return Mathf.Pow(level, 2) * 50f; //exponantial growth
    }
}
