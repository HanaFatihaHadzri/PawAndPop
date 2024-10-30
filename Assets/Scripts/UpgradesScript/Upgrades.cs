using UnityEngine;

[System.Serializable]

public class Upgrades
{
    public enum UpgradeType { MovementSpeed, AdditiveWeapon, BalloonPopping }

    public string upgradeName;
    public string upgradeDesc;
    public Sprite upgradeIcon;
    public UpgradeType upgradeType;

    public float[] upgradeMultipliers;
    public int maxLevel;

}
