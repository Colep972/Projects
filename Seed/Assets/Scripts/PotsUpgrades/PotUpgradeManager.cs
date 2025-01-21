using UnityEngine;
using System.Collections.Generic;

public class PotUpgradeManager : MonoBehaviour
{
    [SerializeField] private PotManager potManager;
    [SerializeField] private List<PotUpgradeData> potUpgrades;
    private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = MoneyManager.Instance;
        if (moneyManager == null)
            Debug.LogError("MoneyManager not found!");
        if (potManager == null)
            Debug.LogError("PotManager not assigned!");
    }

    public bool TryUpgradePot(PotUpgradeData potData)
    {
        int currentLevel = GetPotLevel(potData.slotIndex);

        if (currentLevel >= 3)
            return false;

        int cost = potData.levelCosts[currentLevel];

        if (moneyManager.GetMoney() < cost)
            return false;

        moneyManager.RemoveMoney(cost);
        SetPotLevel(potData.slotIndex, currentLevel + 1);

        return true;
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

    public int GetUpgradeCost(PotUpgradeData potData)
    {
        int currentLevel = GetPotLevel(potData.slotIndex);
        if (currentLevel >= 3)
            return -1;
        return potData.levelCosts[currentLevel];
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