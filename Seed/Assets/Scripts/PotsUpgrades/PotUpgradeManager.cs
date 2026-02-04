using UnityEngine;
using System.Collections.Generic;

public class PotUpgradeManager : MonoBehaviour
{
    [SerializeField] private PotManager potManager;
    [SerializeField] private List<PotUpgradeData> potUpgrades;
    private MoneyManager moneyManager;
    public PotUpgradeData potData;
    public int totalPots = 4;

    public int GetScaledUpgradePrice(PotUpgradeData potData, int currentLevel, int globalOtherLevels)
    {
        if (currentLevel < 0 || currentLevel >= potData.levelCosts.Length)
        {
            Debug.LogWarning("Invalid pot level");
            return -1;
        }

        int basePrice = potData.levelCosts[currentLevel];
        if (basePrice <= 0) return 0;

        float levelMultiplier = Mathf.Pow(1.6f, currentLevel);
        float globalMultiplier = Mathf.Pow(1.15f, globalOtherLevels);

        float scaledPrice = basePrice * levelMultiplier * globalMultiplier;
        return Mathf.CeilToInt(scaledPrice);
    }



    private void Start()
    {
        moneyManager = MoneyManager.Instance;
        if (moneyManager == null)
            Debug.LogError("MoneyManager not found!");
        if (potManager == null)
            Debug.LogError("PotManager not assigned!");
    }

    public string GetPotDescription(PotUpgradeData potData)
    {
        int level = GetPotLevel(potData.slotIndex);

        switch (level)
        {
            case 0:
                return potData.description = "A simple  pot to grow your seeds.";
            case 1:
                return potData.description = "You have an updated pot.";
            case 2:
                return potData.description = "Your pot can now auto grow your seeds.";
            case 3:
                return potData.description = "Your pot can now auto plant your seeds.";
            default:
                return $"{potData.potName}: Unknown level.";
        }
    }


    public bool TryUpgradePot(PotUpgradeData potData)
    {
        int currentLevel = GetPotLevel(potData.slotIndex);
        if (currentLevel >= 3)
            return false;

        int globalOtherLevels = GetGlobalUpgradeLevelsExcluding(potData.slotIndex);
        int cost = GetScaledUpgradePrice(potData, currentLevel, globalOtherLevels);
        if (moneyManager.GetMoney() < cost)
            return false;

        moneyManager.RemoveMoney(cost);
        SetPotLevel(potData.slotIndex, currentLevel + 1);

        return true;
    }

    public int GetUpgradeCost(PotUpgradeData potData)
    {
        int currentLevel = GetPotLevel(potData.slotIndex);
        if (currentLevel >= 3)
            return -1;
        int globalOtherLevels = GetGlobalUpgradeLevelsExcluding(potData.slotIndex);
        return GetScaledUpgradePrice(potData, currentLevel, globalOtherLevels);
    }

    public int GetGlobalUpgradeLevelsExcluding(int excludedSlot)
    {
        int total = 0;
        for (int i = 0; i < totalPots; i++)
        {
            if (i == excludedSlot) continue;
            total += GetPotLevel(i);
        }
        return total;
    }


    // Maintenant public
    public int GetPotLevel(int slotIndex)
    {
        switch (slotIndex)
        {
            case 0: return potManager.potStateSlot1;
            case 1: return potManager.potStateSlot2;
            case 2: return potManager.potStateSlot3;
            case 3: return potManager.potStateSlot4;
            default: return 0;
        }
    }

    public void TryForceSetLevel(int slotIndex, int level)
    {
        if (level < 0 || level > 3) return;
        switch (slotIndex)
        {
            case 0: potManager.potStateSlot1 = level; break;
            case 1: potManager.potStateSlot2 = level; break;
            case 2: potManager.potStateSlot3 = level; break;
            case 3: potManager.potStateSlot4 = level; break;
        }
    }

    private void SetPotLevel(int slotIndex, int level)
    {
        switch (slotIndex)
        {
            case 0:
                potManager.potStateSlot1 = level;
                break;
            case 1:
                potManager.potStateSlot2 = level;
                break;
            case 2:
                potManager.potStateSlot3 = level;
                break;
            case 3:
                potManager.potStateSlot4 = level;
                break;
        }
    }

    public bool IsMaxLevel(PotUpgradeData potData)
    {
        return GetPotLevel(potData.slotIndex) >= 3;
    }

    public List<PotUpgradeData> GetAllPotUpgrades()
    {
        return potUpgrades;
    }
}