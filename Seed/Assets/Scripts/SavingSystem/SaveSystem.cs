using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
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
        public bool hasShownTutorialOnce;
        public List<PotData> pots;
        public List<UpgradeSaveData> upgrades;
        public List<SeedSaveData> unlockedSeeds;
        public List<PlantSaveData> savedPlants;
        public List<PotUpgradeSaveData> potUpgrades;
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
    public struct PotUpgradeSaveData
    {
        public string potName;
        public int currentLevel;
    }

    [System.Serializable]
    public struct UpgradeSaveData
    {
        public UpgradeType id;
        public int currentLevel;
        public bool isUnlocked;
        public float currentValue;
        public int currentPrice;
        public float valueIncrement;
    }



    [System.Serializable]
    public struct SeedSaveData
    {
        public string seedName;
        public string seedPousseColorHex;
        public bool seedUnlocked;
    }

    [System.Serializable]
    public struct PlantSaveData
    {
        public string plantName;
        public string seedName;
        public int number;
    }

    private string savePath;
    public bool firstPotAgain;

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
        Debug.Log(savePath);
    }

    private void Start()
    {
        StartCoroutine(DelayedLoad());
        firstPotAgain = false;
    }

    private IEnumerator DelayedLoad()
    {
        // On attend que la scène soit bien chargée
        yield return new WaitForSeconds(0.1f);

        // Activation forcée du GameObject si désactivé
        GameObject seedInventoryGO = GameObject.Find("InventoryPanel");

        if (seedInventoryGO != null && !seedInventoryGO.activeSelf)
        {
            seedInventoryGO.SetActive(true); // L’Awake sera appelé maintenant
            Debug.Log("SeedInventoryUI activé manuellement pour chargement.");
        }

        // Attente jusqu’à ce que l’instance soit bien initialisée
        yield return new WaitUntil(() => SeedInventoryUI.Instance != null);

        // Petite sécurité de frame
        yield return null;
    }



    public void Save()
    {
        GetName name = GameObject.Find("FactoryName").GetComponent<GetName>();
        if (name == null)
        {
            Debug.LogError("FactoryName or GetName component is missing!");
            return;
        }
        GameData data = new GameData
        {
            coins = MoneyManager.Instance.GetMoney(),
            factoryName = name.getFactoryName(),
            pots = new List<PotData>(),
            upgrades = new List<UpgradeSaveData>(),
            unlockedSeeds = new List<SeedSaveData>(),
            savedPlants = new List<PlantSaveData>(),
            potUpgrades = new List<PotUpgradeSaveData>()
        };

        CameraRaycastPigController pigController = GameObject.FindFirstObjectByType<CameraRaycastPigController>();
        if (pigController != null)
        {
            data.hasShownTutorialOnce = pigController.hasShownTutorialOnce;
        }
        else
        {
            Debug.LogWarning("CameraRaycastPigController not found during save.");
        }

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

        foreach (SeedData seed in SeedInventoryUI.Instance.availableSeeds)
        {
            data.unlockedSeeds.Add(new SeedSaveData
            {
                seedName = seed.seedName,
                seedPousseColorHex = ColorUtility.ToHtmlStringRGB(seed.pousseColor),
                seedUnlocked = seed.unlocked
            });
        }

        foreach (PlantsData plant in PlantDataManager.Instance.grownPlants)
        {
            if (plant.originSeed != null && plant.originSeed.unlocked) // on ne sauve que les plantes actives
            {
                PlantSaveData plantSave = new PlantSaveData
                {
                    plantName = plant.plantName,
                    seedName = plant.originSeed.seedName,
                    number = plant.number
                };
                data.savedPlants.Add(plantSave);
                data.totalPlants += plant.number;
            }
        }

        foreach (Upgrade upgrade in UpgradeManager.Instance.GetAllUpgrades())
        {
            UpgradeSaveData saveData = new UpgradeSaveData
            {
                id = upgrade.data.upgradeType,
                currentLevel = upgrade.currentLevel,
                isUnlocked = upgrade.isUnlocked,
                currentValue = upgrade.currentValue,
                currentPrice = upgrade.currentPrice,
                valueIncrement = upgrade.data.valueIncrement
            };
            data.upgrades.Add(saveData);
        }

        PotUpgradeManager potUpgradeManager = GameObject.FindFirstObjectByType<PotUpgradeManager>();
        if (potUpgradeManager == null)
        {
            Debug.LogWarning("PotUpgradeManager not found during save.");
        }
        else
        {
            foreach (var potData in potUpgradeManager.GetAllPotUpgrades())
            {
                SaveSystem.PotUpgradeSaveData potSave = new SaveSystem.PotUpgradeSaveData
                {
                    potName = potData.potName,
                    currentLevel = potUpgradeManager.GetPotLevel(potData.slotIndex)
                };
                data.potUpgrades.Add(potSave);
            }
        }



        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to: " + savePath);
        PnjTextDisplay.Instance.DisplayMessagePublic("You just saved !! *Groink*");
    }



    public void Load()
    {
        GetName name = GameObject.Find("FactoryName")?.GetComponent<GetName>(); ;
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        string json = File.ReadAllText(savePath);
        GameData data = JsonUtility.FromJson<GameData>(json);
        CameraRaycastPigController pigController = GameObject.FindFirstObjectByType<CameraRaycastPigController>();
        if (pigController != null)
        {
            Debug.Log("not nuuuuuuuuuuuuuulll");
            pigController.hasShownTutorialOnce = data.hasShownTutorialOnce;
        }
        else
        {
            Debug.LogWarning("Pig controller not found during load.");
        }

        MoneyManager.Instance.SetMoney(data.coins);
        if (data.factoryName == null)
        {
            Debug.Log("here");
        }
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

        foreach (SeedSaveData savedSeed in data.unlockedSeeds)
        {
            if(SeedInventoryUI.Instance == null)
            {
                Debug.Log("NULLLLLLLLLLLLLLLL");
            }
            SeedData matchingSeed = SeedInventoryUI.Instance.availableSeeds
                .Find(seed => seed.seedName == savedSeed.seedName);

            if (matchingSeed != null)
            {
                if(savedSeed.seedUnlocked)
                {
                    matchingSeed.unlocked = true;
                }
                SeedInventoryUI.Instance.GenerateSeedButtons();
                if (ColorUtility.TryParseHtmlString("#" + savedSeed.seedPousseColorHex, out Color loadedColor))
                {
                    matchingSeed.pousseColor = loadedColor;
                }
                else
                {
                    Debug.LogWarning("Color parsing failed for seed: " + savedSeed.seedName);
                }
            }
            else
            {
                Debug.LogWarning("Seed not found: " + savedSeed.seedName);
            }
        }
        PotUpgradeManager potUpgradeManager = GameObject.FindFirstObjectByType<PotUpgradeManager>();
        if (potUpgradeManager == null)
        {
            Debug.LogWarning("PotUpgradeManager not found during load.");
        }
        else
        {
            foreach (PotUpgradeSaveData savedUpgrade in data.potUpgrades)
            {
                PotUpgradeData matchingData = potUpgradeManager.GetAllPotUpgrades()
                    .Find(p => p.potName == savedUpgrade.potName);

                if (matchingData != null)
                {
                    // Réappliquer le niveau sauvegardé
                    int slotIndex = matchingData.slotIndex;
                    potUpgradeManager.TryForceSetLevel(slotIndex, savedUpgrade.currentLevel);
                }
                else
                {
                    Debug.LogWarning($"PotUpgradeData not found for {savedUpgrade.potName}");
                }
            }
        }

        int totalPlants = 0;
        foreach (SeedSaveData savedSeed in data.unlockedSeeds)
        {
            SeedData matchingSeed = SeedInventoryUI.Instance.availableSeeds
                .Find(seed => seed.seedName == savedSeed.seedName);

            if (matchingSeed != null)
            {
                matchingSeed.unlocked = savedSeed.seedUnlocked;
                if (ColorUtility.TryParseHtmlString("#" + savedSeed.seedPousseColorHex, out Color loadedColor))
                {
                    matchingSeed.pousseColor = loadedColor;
                }
            }
        }



        GrowButton growButton = GameObject.FindFirstObjectByType<GrowButton>();
        if (growButton != null)
        {
            growButton.SetTotalPlantesFromSave(totalPlants);
        }




        Debug.Log("Game loaded from: " + savePath);
    }


    public bool SaveExists()
    {
        return File.Exists(savePath);
    }
}
