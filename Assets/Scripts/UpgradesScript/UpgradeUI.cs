using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI _instance;

    public Button movementSpeedBtn;
    public Button additiveWeaponBtn;
    public Button additiveShooterBtn;

    private UpgradeManager upgradeManager;

    public static UpgradeUI Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Upgrade UI is Null !");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        upgradeManager = UpgradeManager._instance;

        movementSpeedBtn.onClick.AddListener(() => upgradeManager.BuyMovementSpeedUpgrade(1.1f)); //increase speed by 10%
        additiveWeaponBtn.onClick.AddListener(() => upgradeManager.BuyAdditiveWeaponUpgrade(1)); //increase additive by 1
        additiveShooterBtn.onClick.AddListener(() => upgradeManager.BuyAimShooterUpgrade());    //auto aim shoot enemy
    }
}
