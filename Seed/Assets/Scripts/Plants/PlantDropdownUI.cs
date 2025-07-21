using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlantDropdownUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject plantItemPrefab;
    public Transform contentContainer;
    public Button dropdownToggleButton; // le bouton pour ouvrir/fermer le menu
    public GameObject dropdownListPanel; // le panneau scrollable

    private PlantsData selectedPlant;
    private List<PlantsData> plantList;
    [SerializeField] private Transform selectedPlantContainer;

    private GameObject currentSelectedUI;
    [SerializeField] private TextMeshProUGUI plantAmountText;
    public static PlantDropdownUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void UpdateSelectedPlantDisplay()
    {
        if (selectedPlant != null && plantAmountText != null)
        {
            if (selectedPlant.number > 0)
            {
                plantAmountText.gameObject.SetActive(true);
                plantAmountText.text = selectedPlant.number.ToString();
            }
            else
            {
                plantAmountText.gameObject.SetActive(false);
            }
        }
        else if (plantAmountText != null)
        {
            plantAmountText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        UpdateSelectedPlantDisplay();
    }

    private void Start()
    {
        plantList = PlantDataManager.Instance.grownPlants;
        PopulateDropdown();
    }

    public void ToggleDropdown()
    {
        dropdownListPanel.SetActive(!dropdownListPanel.activeSelf);
        if (dropdownListPanel.activeSelf && currentSelectedUI != null && currentSelectedUI.activeSelf )
        {
            currentSelectedUI.SetActive(false);
        }
    }

    void PopulateDropdown()
    {
        int cmpt = 0;
        foreach (Transform child in contentContainer)
            Destroy(child.gameObject);

        foreach (PlantsData plant in plantList)
        {
            if (plant.originSeed == null || !plant.originSeed.unlocked)
                continue;

            GameObject item = Instantiate(plantItemPrefab, contentContainer);

            Button itemButton = item.GetComponent<Button>();

            PlantItemUI ui = item.GetComponent<PlantItemUI>();
            ui.Set(plant, cmpt);
            itemButton.onClick.AddListener(() =>
            {
                int choice = 0;
                dropdownListPanel.SetActive(false);
                selectedPlant = plant;
                UpdateSelectedPlantDisplay();

                if (currentSelectedUI != null)
                    Destroy(currentSelectedUI);

                currentSelectedUI = Instantiate(plantItemPrefab, selectedPlantContainer);
                switch (plant.plantName)
                {
                    case "Plant of Seed 0": choice = 0; 
                    break;
                    case "Plant of Seed 1": choice = 1; 
                    break;
                    case "Plant of Seed 2": choice = 2; 
                    break;
                }

                currentSelectedUI.GetComponent<PlantItemUI>().Set(plant, choice);
            });

            cmpt++;
        }

    }

    public PlantsData GetCurrentPlant()
    {
        return selectedPlant;
    }

    public void SetSelectedPlant(PlantsData plant)
    {
        selectedPlant = plant;
        UpdateSelectedPlantDisplay();

        if (currentSelectedUI != null)
            Destroy(currentSelectedUI);

        currentSelectedUI = Instantiate(plantItemPrefab, selectedPlantContainer);
        int index = PlantDataManager.Instance.grownPlants.IndexOf(plant);
        currentSelectedUI.GetComponent<PlantItemUI>().Set(plant, index);
    }

    public void SetSelectedPlantFromSeed(SeedData seed)
    {
        if (seed == null || PlantDataManager.Instance == null) return;

        PlantsData match = PlantDataManager.Instance.grownPlants
            .Find(p => p.originSeed == seed);

        if (match != null)
        {
            SetSelectedPlant(match);
        }
    }
}
