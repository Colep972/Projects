using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SeedInventoryUI : MonoBehaviour // InventoryUI
{
    public static SeedInventoryUI Instance { get; private set; }

    [Header("Seed Data")]
    [SerializeField] private List<SeedData> availableSeeds; // List of seeds the player owns
    [SerializeField] private GameObject seedButtonPrefab; // Prefab for the buttons
    [SerializeField] private Transform seedButtonContainer; // Where buttons are placed

    [Header("Plant Panel")]
    [SerializeField] private Transform plantPanelContainer;
    [SerializeField] private GameObject plantSlotPrefab;

    private Dictionary<SeedData, PlantsUI> plantSlotMap;

    private Button selectedButton;
    private SeedData selectedSeed; // The currently selected seed
    public List<Sprite> icons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Or disable if you prefer
            return;
        }
        Instance = this;
        plantSlotMap = new Dictionary<SeedData, PlantsUI>();
    }

    void Start()
    {
        GenerateSeedButtons();
    }

    public void GenerateSeedButtons()
    {
        int cmpt = 0;
        foreach (SeedData seed in availableSeeds)
        {
            GameObject buttonObj = Instantiate(seedButtonPrefab, seedButtonContainer);

            Button button = buttonObj.GetComponent<Button>();

            Image icon = buttonObj.transform.Find("Icon")?.GetComponent<Image>();

            TMP_Text seedText = buttonObj.transform.Find("Text").GetComponent<TMP_Text>();

            icon.sprite = seed.icon;
            seedText.text = seed.seedName;
            SeedData seedCopy = seed;
            PlantsData plantData = PlantDataManager.Instance.grownPlants[cmpt];
            cmpt++;

            GameObject plantSlotObj = Instantiate(plantSlotPrefab, plantPanelContainer);
            PlantsUI plantSlot = plantSlotObj.GetComponent<PlantsUI>();
            plantSlot.Set(plantData);
            plantSlotMap.Add(seedCopy, plantSlot);
        }
    }

    public void UpdatePlantSlot(SeedData seed)
    {
        if (plantSlotMap.TryGetValue(seed, out PlantsUI slot))
        {
            slot.UpdateCount();
        }
    }

    void SelectSeed(SeedData seed)
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

        Debug.Log("Selected button text changed for: " + selectedButton.name);
        selectedSeed = seed;
    }

    public SeedData GetSelectedSeed()
    {
        return selectedSeed;
    }
}
