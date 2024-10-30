using UnityEngine;
[CreateAssetMenu(fileName ="NewUpgrade", menuName ="Upgrades/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public enum UpgradeType { MovementSpeed, AdditiveWeapon, BalloonPopping}
    
    public UpgradeType upgradeType;     //define type of upgrade
    public string upgradeName;          //define upgrade name
    public string upgradeDesc;          //define upgrade do
    public Sprite upgradeIcon;          //define icon for UI
    
    public float[] upgradeValue;         //store value of upgrade(speed, multiplier, num needles, damage multiplier)
    public int maxLevel;                //define max level for this upgrade
}
