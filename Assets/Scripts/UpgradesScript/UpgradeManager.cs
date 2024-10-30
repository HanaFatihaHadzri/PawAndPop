using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager _instance;

    public PlayerMovement playerMovement;

    public GameObject[] flyingWeaponPrefab;
    //public Transform parentObject;

    public AimShooting aimShooter;

    //private float baseMovementSpeed;
    private float currentMovementSpeed;
    private int currentAdditiveWeapon = 0;

    public static UpgradeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Upgrade Manager is Null !");
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
        currentMovementSpeed = playerMovement.moveSpeed;

        ApplyUpgrades();
    }

    public void BuyMovementSpeedUpgrade(float multiplier)
    {
        currentMovementSpeed *= multiplier;
        
        ApplyUpgrades();
    }

    public void BuyAimShooterUpgrade()
    {
        aimShooter.enabled = true;

        ApplyUpgrades();

        Debug.Log("Bought upgrade: aim shooter enabled!");
    }

    public void BuyAdditiveWeaponUpgrade(int amount)
    {
        if(currentAdditiveWeapon < 3)
        {
            for(int i = 0; i < amount && currentAdditiveWeapon < 3; i++)
            {
                ActivateNextWeapon();
            }

            ApplyUpgrades();

            Debug.Log("Bought upgrade: additive weapon!");
        }
    }

    void ActivateNextWeapon()
    {
        if(currentAdditiveWeapon < flyingWeaponPrefab.Length)
        {
            GameObject weapon = flyingWeaponPrefab[currentAdditiveWeapon];
            weapon.SetActive(true);
    
            //ensure local position is set correctly upon activation
            weapon.transform.localPosition = weapon.transform.localPosition; //keeep ori local position
            currentAdditiveWeapon++;

            if(currentAdditiveWeapon > 3)
            {
                UpgradeUI._instance.additiveWeaponBtn.interactable = false;
            }
        }
    }

    //void AddFlyingWeapon(int multiplier)
    //{
    //    currentAdditiveWeapon += multiplier;
    //    
    //    GameObject newWeapon = Instantiate(flyingWeaponPrefab, parentObject);
    //    
    //    FlyingWeapon flyingWeapon = newWeapon.GetComponent<FlyingWeapon>();
    //    flyingWeapon.offset = (Mathf.PI * 2 / currentAdditiveWeapon); //ensure proper space for new weapons
    //}

    public void ApplyUpgrades()
    {
        playerMovement.moveSpeed = currentMovementSpeed;

        //instatiate prefab weapon under Player(parent)

        //update balloon popping power ( other script : shooting )

        Debug.Log("Player current speed: " + currentMovementSpeed);
    }

    public void DeActivateNextWeapon()
    {
        for (int i = 0; i < flyingWeaponPrefab.Length; i++)
        {
            flyingWeaponPrefab[i].SetActive(false); // Deactivate all weapons
        }
        currentAdditiveWeapon = 0; // Reset the weapon count
        UpgradeUI._instance.additiveWeaponBtn.interactable = true; // Re-enable the upgrade button if needed
    }

}
