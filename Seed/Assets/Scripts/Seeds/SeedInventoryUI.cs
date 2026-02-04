using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SeedInventoryUI : MonoBehaviour // InventoryUI
{
    public static SeedInventoryUI Instance { get; private set; }

    [Header("Seed Data")]
    public List<SeedData> availableSeeds; // List of seeds the player owns
    [SerializeField] private GameObject seedButtonPrefab; // Prefab for the buttons
    [SerializeField] public Transform seedButtonContainer; // Where buttons are placed

    [Header("Plant Panel")]
    [SerializeField] private Transform plantPanelContainer;
    [SerializeField] private GameObject plantSlotPrefab;

    public Dictionary<SeedData, PlantsUI> plantSlotMap;

    public Button selectedButton;
    public SeedData selectedSeed; // The currently selected seed
    public List<Sprite> icons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Debug.Log("SeedInventoryUI Awake OK");
        plantSlotMap = new Dictionary<SeedData, PlantsUI>();
    }

    void Start()
    {
        GenerateSeedButtons();
        GetUnlockedSeeds();
    }

    public void GenerateSeedButtons()
    {
        // Nettoyage : on détruit les anciens boutons
        foreach (Transform child in seedButtonContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in plantPanelContainer)
        {
            Destroy(child.gameObject);
        }

        // Nettoyage de la map pour éviter les doublons
        plantSlotMap.Clear();

        int cmpt = 0;
        foreach (SeedData seed in availableSeeds)
        {
            if (!seed.unlocked) continue;

            GameObject buttonObj = Instantiate(seedButtonPrefab, seedButtonContainer);
            Button button = buttonObj.GetComponent<Button>();
            Image icon = buttonObj.transform.Find("Icon")?.GetComponent<Image>();
            TMP_Text seedText = buttonObj.transform.Find("Text").GetComponent<TMP_Text>();

            icon.sprite = seed.icon;
            seedText.text = seed.seedName;
            SeedData seedCopy = seed;

            // Sécurise l'accès à grownPlants (évite les IndexOutOfRange)
            if (cmpt >= PlantDataManager.Instance.grownPlants.Count)
            {
                Debug.LogWarning($"Pas assez de grownPlants pour seed {seed.seedName}");
                continue;
            }

            PlantsData plantData = PlantDataManager.Instance.grownPlants[cmpt];
            cmpt++;

            GameObject plantSlotObj = Instantiate(plantSlotPrefab, plantPanelContainer);
            PlantsUI plantSlot = plantSlotObj.GetComponent<PlantsUI>();
            plantSlot.Set(plantData);

            if (PlantDropdownUI.Instance != null)
            {
                PlantDropdownUI.Instance.SetSelectedPlantFromSeed(seed);
            }

            if (!plantSlotMap.ContainsKey(seed))
            {
                plantSlotMap.Add(seedCopy, plantSlot);
            }
            else
            {
                Debug.LogWarning($"La graine {seed.seedName} est déjà dans plantSlotMap !");
            }

            button.onClick.AddListener(() => SelectSeed(seedCopy));
            Debug.Log($"Seed button created for {seed.seedName}");
        }
    }


    public void UpdatePlantSlot(SeedData seed)
    {
        if (plantSlotMap.TryGetValue(seed, out PlantsUI slot))
        {
            slot.UpdateCount();
        }
    }

    public void SelectSeed(SeedData seed)
    {
        if (seed == null)
        {
            Debug.LogError("SelectSeed() called with a NULL seed!");
            return;
        }

        Debug.Log("Selecting seed: " + seed.seedName);

        // Reset previous button text color
        if (selectedButton != null)
        {
            TMP_Text previousText = selectedButton.GetComponentInChildren<TMP_Text>();
            if (previousText != null)
            {
                previousText.color = Color.white; // Reset previous selection
            }
        }

        // Find the button associated with this seed
        selectedButton = seedButtonContainer
            .GetComponentsInChildren<Button>()
            .FirstOrDefault(b =>
                b.GetComponentInChildren<TMP_Text>() != null &&
                b.GetComponentInChildren<TMP_Text>().text == seed.seedName
            );

        if (selectedButton == null)
        {
            Debug.LogError("No button found for seed: " + seed.seedName);
            return;
        }

        TMP_Text selectedText = selectedButton.GetComponentInChildren<TMP_Text>();
        if (selectedText != null)
        {
            
            selectedText.color = Color.green;
        }

        if (TutorialManager.Instance != null && TutorialManager.Instance.currentStep == TutorialStep.GetFirstPot)
        {
            TutorialManager.Instance.AdvanceStep();
        }
        Debug.Log("Selected button text changed for: " + selectedButton.name);
        selectedSeed = seed;
    }

    public SeedData GetSelectedSeed()
    {
        return selectedSeed;
    }

    public void RefreshAllPlantSlots()
    {
        foreach (SeedData seed in availableSeeds)
        {
            UpdatePlantSlot(seed);
        }
    }

    public List<SeedData> GetUnlockedSeeds()
    {
        List<SeedData> unlockedSeeds;
        unlockedSeeds = new List<SeedData>();
        foreach (SeedData seed in availableSeeds)
        {
            if(seed.unlocked)
            {
                unlockedSeeds.Add(seed);
            }
        }
        return unlockedSeeds;
    }

    public void AddToInventory(SeedData add, int amount)
    {
        foreach (SeedData seed in availableSeeds)
        {
            if (seed == add)
            {
                plantSlotMap[add].SetNumber(plantSlotMap[add].GetNumber() + amount);
            }
        }
    }

    public void AddPlant(int amount)
    {
        plantSlotMap[GetUnlockedSeeds()[Random.Range(0, GetUnlockedSeeds().Count)]].SetNumber(plantSlotMap[GetUnlockedSeeds()[Random.Range(0, GetUnlockedSeeds().Count)]].GetNumber() + amount);
    }

    public void RemovePlant(int amount)
    {
        plantSlotMap[GetUnlockedSeeds()[Random.Range(0, GetUnlockedSeeds().Count)]].SetNumber(plantSlotMap[GetUnlockedSeeds()[Random.Range(0, GetUnlockedSeeds().Count)]].GetNumber() - amount);
    }

    public void RemoveFromInventory(SeedData remove, int amount)
    {
        foreach (SeedData seed in availableSeeds)
        {
            if (seed == remove)
            {
                if(amount <= plantSlotMap[remove].GetNumber())
                {
                    plantSlotMap[remove].SetNumber(plantSlotMap[remove].GetNumber() - amount);
                } 
            }
        }
    }

    public bool HasSeed(SeedData seed)
    {
        foreach(SeedData find  in availableSeeds)
        {
            if(seed == find)
            {
                return true;
            }
        }
        return false;
    }
}
