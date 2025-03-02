using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private static string savePath = Application.persistentDataPath + "/save.json";
    public static void Save(GameData data, List<Upgrade> upgrades)
    {
        data.upgrades.Clear();
        foreach (var upgrade in upgrades)
        {
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

        // Convert data to JSON and save to file
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to: " + savePath);
    }


    public static GameData Load(List<Upgrade> upgrades)
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            // Load Upgrades Data
            foreach (var upgrade in upgrades)
            {
                UpgradeSaveData savedUpgrade = data.upgrades.Find(u => u.id == upgrade.data.name);
                if (savedUpgrade != null)
                {
                    upgrade.LoadFromData(savedUpgrade);
                }
            }

            Debug.Log("Game loaded from: " + savePath);
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found.");
            return new GameData();  // Return a default save if no file exists
        }
    }

    public static bool SaveExists()
    {
        return File.Exists(savePath);
    }
}
