using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(-10)]
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }
    private SeedData seedToSelectAfterLoad;


    [System.Serializable]
    public struct GameData
    {
        public string selectedSeedName;
        public bool isAutoSaveEnabled;
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
        // On attend que la sc�ne soit bien charg�e
        yield return new WaitForSeconds(0.1f);

        // Activation forc�e du GameObject si d�sactiv�
        GameObject seedInventoryGO = GameObject.Find("InventoryPanel");

        if (seedInventoryGO != null && !seedInventoryGO.activeSelf)
        {
            seedInventoryGO.SetActive(true); // L�Awake sera appel� maintenant
            Debug.Log("SeedInventoryUI activ� manuellement pour chargement.");
        }

        // Attente jusqu�� ce que l�instance soit bien initialis�e
        yield return new WaitUntil(() => SeedInventoryUI.Instance != null);

        // Petite s�curit� de frame
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

        SeedData selected = SeedInventoryUI.Instance.selectedSeed;
        if (selected != null)
        {
            data.selectedSeedName = selected.seedName;
        }
        else
        {
            data.selectedSeedName = "";
        }

        Toggle autoSaveToggle = GameObject.Find("Toggle")?.GetComponent<Toggle>();
        if (autoSaveToggle != null)
        {
            data.isAutoSaveEnabled = autoSaveToggle.isOn;
        }
        else
        {
            Debug.LogWarning("AutoSaveToggle not found during save.");
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

        foreach (PlantSaveData savedPlant in data.savedPlants)
        {
            PlantsData matchingPlant = PlantDataManager.Instance.grownPlants
                .Find(p => p.plantName == savedPlant.plantName && p.originSeed.seedName == savedPlant.seedName);

            if (matchingPlant != null)
            {
                matchingPlant.number = savedPlant.number;
                SeedInventoryUI.Instance.UpdatePlantSlot(matchingPlant.originSeed);
            }
            else
            {
                Debug.LogWarning($"Plant not found during load: {savedPlant.plantName} from seed {savedPlant.seedName}");
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
        SeedInventoryUI.Instance.GenerateSeedButtons();

        if (!string.IsNullOrEmpty(data.selectedSeedName))
        {
            seedToSelectAfterLoad = SeedInventoryUI.Instance.availableSeeds
                .Find(seed => seed.seedName == data.selectedSeedName);

            if (seedToSelectAfterLoad != null)
            {
                Invoke(nameof(DelayedSelectSeed), 0.05f); // d�lai tr�s court
            }
            else
            {
                Debug.LogWarning("Saved selected seed not found: " + data.selectedSeedName);
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
                    // R�appliquer le niveau sauvegard�
                    int slotIndex = matchingData.slotIndex;
                    potUpgradeManager.TryForceSetLevel(slotIndex, savedUpgrade.currentLevel);
                }
                else
                {
                    Debug.LogWarning($"PotUpgradeData not found for {savedUpgrade.potName}");
                }
            }
        }

        Toggle autoSaveToggle = GameObject.Find("Toggle")?.GetComponent<Toggle>();
        if (autoSaveToggle != null)
        {
            autoSaveToggle.isOn = data.isAutoSaveEnabled;
        }
        else
        {
            Debug.LogWarning("AutoSaveToggle not found during load.");
        }


        GrowButton growButton = GameObject.FindFirstObjectByType<GrowButton>();
        if (growButton != null)
        {
            growButton.SetTotalPlantesFromSave(data.totalPlants);
        }




        Debug.Log("Game loaded from: " + savePath);
    }


    public bool SaveExists()
    {
        return File.Exists(savePath);
    }

    private void DelayedSelectSeed()
    {
        if (seedToSelectAfterLoad != null)
        {
            SeedInventoryUI.Instance.SelectSeed(seedToSelectAfterLoad);
            PlantDropdownUI.Instance?.SetSelectedPlantFromSeed(seedToSelectAfterLoad);
            Debug.Log("Seed selected after delay: " + seedToSelectAfterLoad.seedName);
        }
    }

}
