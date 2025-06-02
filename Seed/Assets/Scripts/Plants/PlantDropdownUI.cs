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

    private void Awake()
    {
        
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
            /*if (plant.number <= 0) continue;*/

            GameObject item = Instantiate(plantItemPrefab, contentContainer);

            Button itemButton = item.GetComponent<Button>();

            PlantItemUI ui = item.GetComponent<PlantItemUI>();
            ui.Set(plant, cmpt);
            itemButton.onClick.AddListener(() =>
            {
                int choice = 0;
                dropdownListPanel.SetActive(false);
                selectedPlant = plant;
                if (currentSelectedUI != null)
                    Destroy(currentSelectedUI);

                currentSelectedUI = Instantiate(plantItemPrefab, selectedPlantContainer);
                switch(plant.plantName)
                {
                    case "Plant of Seed 0":
                        choice = 0;
                        break;
                    case "Plant of Seed 1":
                        choice = 1;
                        break;
                    case "Plant of Seed 2":
                        choice = 2;
                        break;
                    default:
                        break;
                }
                currentSelectedUI.GetComponent<PlantItemUI>().Set(plant,choice);

                dropdownListPanel.SetActive(false);
            });
            cmpt++;
        }
    }
}
