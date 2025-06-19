using UnityEngine;

public enum UpgradeType
{
    ClickPower,
    SellPrice,
    AutoGrowPower,
    AutoGrowSpeed,
    PlantsPerHarvest
}

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public Sprite icon;
    public int basePrice = 10; 
    public float priceMultiplier = 1.5f;
    public UpgradeType upgradeType;
    public float baseValue = 1f;
    public float valueIncrement = 1f;
    public int maxLevel = 5;
    [TextArea]
    public string description;
}