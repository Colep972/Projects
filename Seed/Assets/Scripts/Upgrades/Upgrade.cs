using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public UpgradeData data;
    public int currentLevel { get; private set; }
    public bool isUnlocked { get; private set; }
    public float currentValue { get; private set; }
    public int currentPrice { get; private set; }

    public Upgrade(UpgradeData data)
    {
        this.data = data;
        currentLevel = 0;
        isUnlocked = false;
        currentValue = data.baseValue;
        currentPrice = data.basePrice; 
    }

    public bool CanLevelUp(int playerMoney)
    {
        return currentLevel < data.maxLevel && playerMoney >= GetNextLevelPrice();
    }

    public int GetNextLevelPrice()
{
    return Mathf.RoundToInt(data.basePrice * Mathf.Pow(data.priceMultiplier, currentLevel));
}

    public void LevelUp()
    {
        if (currentLevel >= data.maxLevel) return;

        currentLevel++;
        isUnlocked = true;
        currentValue = data.baseValue + (data.valueIncrement * (currentLevel - 1));
        currentPrice = GetNextLevelPrice(); // Met � jour le prix pour le prochain niveau
    }

    public void LoadFromData(SaveSystem.UpgradeSaveData data)
    {
        if (data.id == this.data.upgradeType)
        {
            currentLevel = data.currentLevel;
            isUnlocked = data.isUnlocked;
            currentValue = this.data.baseValue + this.data.valueIncrement * (currentLevel - 1);
            currentPrice = GetNextLevelPrice();
            UpgradeManager.Instance.ApplyUpgradeEffects(this);
        }
    }

}