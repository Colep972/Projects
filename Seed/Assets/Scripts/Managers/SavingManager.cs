/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
    public List<Upgrade> upgrades;
    public GetName _factoryName;

    private void Start()
    {
        if (upgrades == null)
        {
            upgrades = new List<Upgrade>();
            Debug.LogWarning("`upgrades` list was NULL, creating an empty list.");
        }

        if (upgrades.Count == 0)
        {
            Debug.LogWarning("`upgrades` list was EMPTY, trying to get upgrades from GameManager...");

            GameManager gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager != null && gameManager._upgrades != null)
            {
                upgrades = gameManager._upgrades;
            }

            if (upgrades == null || upgrades.Count == 0)
            {
                Debug.LogError("ERROR: `upgrades` list is STILL NULL or EMPTY after trying to get it!");
            }
        }
    }

    public GameData GetGameData()
    {
        if (gameData == null)
        {
            Debug.LogError("ERROR: gameData is NULL in SavingManager!");
            return null;
        }

        if (_factoryName == null)
        {
            Debug.LogError("ERROR: _factoryName (GetName) is NULL in SavingManager!");
            return null;
        }

        if (upgrades == null)
        {
            Debug.LogWarning("`upgrades` list is NULL in SavingManager, initializing it...");
            upgrades = new List<Upgrade>();
        }

        GameData data = new GameData
        {
            totalPlants = gameData.totalPlants,
            coins = gameData.coins,
            pots = gameData.pots,
            factoryName = _factoryName.getFactoryName(),
            upgrades = new List<UpgradeSaveData>()
        };

        foreach (var upgrade in upgrades)
        {
            if (upgrade == null || upgrade.data == null)
            {
                Debug.LogWarning("Skipping NULL upgrade in Save!");
                continue;
            }

            UpgradeSaveData saveData = new UpgradeSaveData
            {
                id = upgrade.data.name,
                currentLevel = upgrade.currentLevel,
                isUnlocked = upgrade.isUnlocked,
                currentValue = upgrade.currentValue,
                currentPrice = upgrade.currentPrice
            };

            data.upgrades.Add(saveData);
        }

        return data;
    }

    public void LoadGameData(GameData data)
    {
        if (data == null) return;

        gameData.totalPlants = data.totalPlants;
        gameData.coins = data.coins;
        gameData.pots = data.pots;

        _factoryName.setFactoryName(data.factoryName);

        foreach (var upgrade in upgrades)
        {
            UpgradeSaveData savedUpgrade = data.upgrades.Find(u => u.id == upgrade.data.name);
            if (savedUpgrade != null)
            {
                upgrade.LoadFromData(savedUpgrade);
            }
        }
    }
}
*/