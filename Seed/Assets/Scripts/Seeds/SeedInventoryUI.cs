using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SeedInventoryUI : MonoBehaviour
{
    [Header("Seed Data")]
    [SerializeField] private List<SeedData> availableSeeds; // List of seeds the player owns
    [SerializeField] private GameObject seedButtonPrefab; // Prefab for the buttons
    [SerializeField] private Transform seedButtonContainer; // Where buttons are placed

    private Button selectedButton;
    private SeedData selectedSeed; // The currently selected seed
    private static SeedData globalSelectedSeed = null;

    void Start()
    {
        GenerateSeedButtons();
    }

    void GenerateSeedButtons()
    {
        foreach (SeedData seed in availableSeeds)
        {
            GameObject buttonObj = Instantiate(seedButtonPrefab, seedButtonContainer);

            Button button = buttonObj.GetComponent<Button>();
            if (button == null) Debug.LogError("Button component is missing on: " + buttonObj.name);

            Image icon = buttonObj.transform.Find("Icon")?.GetComponent<Image>();
            if (icon == null) Debug.LogError("Icon child or Image component is missing on: " + buttonObj.name);

            TMP_Text seedText = buttonObj.transform.Find("Text").GetComponent<TMP_Text>();
            if (seedText == null) Debug.LogError("Text child or Text component is missing on: " + buttonObj.name);


            icon.sprite = seed.icon;
            seedText.text = seed.seedName;
            SeedData seedCopy = seed;
            button.onClick.AddListener(() => SelectSeed(seedCopy));
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

        // Change only the text color of the selected button
        TMP_Text selectedText = selectedButton.GetComponentInChildren<TMP_Text>();
        if (selectedText != null)
        {
            selectedText.color = Color.green; // Highlight selected button's text
        }

        Debug.Log("Selected button text changed for: " + selectedButton.name);
        selectedSeed = seed;
        globalSelectedSeed = seed;
    }

    public static SeedData GetGlobalSelectedSeed()
    {
        return globalSelectedSeed;
    }


    public SeedData GetSelectedSeed()
    {
        return selectedSeed;
    }
}
