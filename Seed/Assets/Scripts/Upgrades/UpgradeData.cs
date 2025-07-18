using System.Collections.Generic;
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
    public List<Sprite> icons = new List<Sprite>();
    public int basePrice = 10; 
    public float priceMultiplier = 1.8f;
    public UpgradeType upgradeType;
    public float baseValue = 1f;
    public float valueIncrement = 1f;
    public int maxLevel = 5;
    [TextArea]
    public string description;
}