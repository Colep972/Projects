using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Unity.VisualScripting;
[DefaultExecutionOrder(-10)]
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    [System.Serializable]
    public struct GameData
    {
        public int totalPlants;
        public int coins;
        public string factoryName;
        public List<PotData> pots;
        public List<UpgradeSaveData> upgrades;
    }

    [System.Serializable]
    public struct PotData
    {
        public int slotIndex;
        public int potState;
        public int pousse;
        public int produced;
        public bool isAutoGrow;
        public bool isAutoPlant;
    }

    [System.Serializable]
    public struct UpgradeSaveData
    {
        public UpgradeType id;
        public int currentLevel;
        public bool isUnlocked;
        public float currentValue;
        public int currentPrice;
    }

    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep SaveSystem between scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
        savePath = Application.persistentDataPath + "/save.json";
    }
    public void Save()
    {
        GetName name = GameObject.Find("FactoryName").GetComponent<GetName>();
        GameData data = new GameData
        {
            coins = MoneyManager.Instance.GetMoney(),
            factoryName = name.getFactoryName(),
            pots = new List<PotData>(),
            upgrades = new List<UpgradeSaveData>()
        };

        foreach (PotData pot in PotManager.Instance.GetPotData())
        {
            PotData potData = new PotData
            {
                slotIndex = pot.slotIndex,
                potState = pot.potState,
                pousse = pot.pousse,
                produced = pot.produced,
                isAutoGrow = pot.isAutoGrow,
                isAutoPlant = pot.isAutoPlant
            };
            data.pots.Add(potData);
        }

        foreach (Upgrade upgrade in UpgradeManager.Instance.GetAllUpgrades())
        {
            UpgradeSaveData saveData = new UpgradeSaveData
            {
                id = upgrade.data.upgradeType,
                currentLevel = upgrade.currentLevel,
                isUnlocked = upgrade.isUnlocked,
                currentValue = upgrade.currentValue,
                currentPrice = upgrade.currentPrice
            };
            data.upgrades.Add(saveData);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to: " + savePath);
    }



    public void Load()
    {
        GetName name = new GetName();
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        string json = File.ReadAllText(savePath);
        GameData data = JsonUtility.FromJson<GameData>(json);

        MoneyManager.Instance.SetMoney(data.coins);
        name.setFactoryName(data.factoryName);

        List<PotData> loadedPots = new List<PotData>();
        foreach (PotData pot in data.pots)
        {
            PotData potData = new PotData
            {
                slotIndex = pot.slotIndex,
                potState = pot.potState,
                pousse = pot.pousse,
                produced = pot.produced,
                isAutoGrow = pot.isAutoGrow,
                isAutoPlant = pot.isAutoPlant
            };
            loadedPots.Add(potData);
        }
        PotManager.Instance.LoadPotData(loadedPots);

        foreach (UpgradeSaveData savedUpgrade in data.upgrades)
        {
            Upgrade upgrade = UpgradeManager.Instance.GetUpgrade(savedUpgrade.id);
            if (upgrade != null)
            {
                upgrade.LoadFromData(savedUpgrade);
            }
            else
            {
                Debug.LogWarning($"Upgrade {savedUpgrade.id} not found in UpgradeManager!");
            }
        }

        Debug.Log("Game loaded from: " + savePath);
    }


    public bool SaveExists()
    {
        return File.Exists(savePath);
    }
}
