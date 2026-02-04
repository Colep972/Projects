using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class PotGridGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PotUpgradeManager potUpgradeManager;
    [SerializeField] private GameObject upgradeButtonPrefab;

    [Header("Grid Settings")]
    [SerializeField] private int columns = 3;
    [SerializeField] private float spacing = 10f;
    [SerializeField] private Vector2 buttonSize = new Vector2(100, 100);

    [Header("Button Colors")]
    [SerializeField] private Color unlockedColor = Color.white;
    [SerializeField] private Color lockedColor = Color.gray;
    [SerializeField] private Color cantAffordColor = new Color(1, 0.5f, 0.5f);

    private GridLayoutGroup gridLayout;
    private MoneyManager moneyManager;

    void Start()
    {
        moneyManager = MoneyManager.Instance;
        if (moneyManager != null)
        {
            moneyManager.onMoneyChanged += OnMoneyChanged;
        }
        else
        {
            Debug.LogError("MoneyManager not found!");
        }

        gridLayout = GetComponent<GridLayoutGroup>();
        if (gridLayout == null)
        {
            gridLayout = gameObject.AddComponent<GridLayoutGroup>();
        }

        InitializeGridLayout();
        GenerateUpgradeButtons();
    }

    void OnDestroy()
    {
        if (moneyManager != null)
        {
            moneyManager.onMoneyChanged -= OnMoneyChanged;
        }
    }

    private void OnMoneyChanged(int newAmount)
    {
        UpdateAllButtonsUI();
    }

    void InitializeGridLayout()
    {
        if (gridLayout != null)
        {
            gridLayout.cellSize = buttonSize;
            gridLayout.spacing = new Vector2(600, spacing);
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = columns;
            gridLayout.childAlignment = TextAnchor.UpperLeft;
        }
    }

    void GenerateUpgradeButtons()
    {
        if (potUpgradeManager == null || upgradeButtonPrefab == null)
        {
            Debug.LogError("Missing references in PotGridGenerator!");
            return;
        }

        // Nettoyer les boutons existants
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var potUpgrades = potUpgradeManager.GetAllPotUpgrades();
        foreach (var potUpgrade in potUpgrades)
        {
            if (potUpgrade == null) continue;

            GameObject buttonObj = Instantiate(upgradeButtonPrefab, transform);
            SetupUpgradeButton(buttonObj, potUpgrade);
        }
    }

    void SetupUpgradeButton(GameObject buttonObj, PotUpgradeData potData)
    {
        Button button = buttonObj.GetComponent<Button>();
        Image icon = buttonObj.transform.Find("Icon")?.GetComponent<Image>();
        TextMeshProUGUI levelText = buttonObj.transform.Find("Level")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = buttonObj.transform.Find("Price")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nameText = buttonObj.transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = buttonObj.transform.Find("Description")?.GetComponent<TextMeshProUGUI>();

        if (icon == null || levelText == null || priceText == null)
        {
            Debug.LogError("Missing UI components on upgrade button!");
            return;
        }

        var potRef = buttonObj.GetComponent<PotReference>();
        if (potRef == null)
        {
            potRef = buttonObj.AddComponent<PotReference>();
        }
        potRef.potData = potData;

        // Configuration initiale
        UpdateButtonUI(potRef, icon, levelText, priceText, nameText, descriptionText, button);

        // Configuration du bouton
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OnUpgradeButtonClicked(potData));
    }

    void UpdateButtonUI(PotReference potRef, Image icon, TextMeshProUGUI levelText,
                       TextMeshProUGUI priceText, TextMeshProUGUI nameText,
                       TextMeshProUGUI descriptionText, Button button)
    {
        bool isMaxLevel = potUpgradeManager.IsMaxLevel(potRef.potData);
        int cost = potUpgradeManager.GetUpgradeCost(potRef.potData);
        bool canAfford = cost >= 0 && moneyManager.GetMoney() >= cost;

        // Mise à jour des visuels
        icon.sprite = potRef.potData.icon;
        if (nameText != null) nameText.text = potRef.potData.potName;
        if (descriptionText != null)
        {
            descriptionText.text = potUpgradeManager.GetPotDescription(potRef.potData);
        }

        int currentLevel = cost >= 0 ? potUpgradeManager.GetPotLevel(potRef.potData.slotIndex) : 3;
        levelText.text = $"Level {currentLevel}/3";
        priceText.text = isMaxLevel ? "MAX" : $"${cost}";

        // Interactivité et couleur
        button.interactable = !isMaxLevel && canAfford;
        icon.color = isMaxLevel ? lockedColor :
                    !canAfford ? cantAffordColor :
                    unlockedColor;
    }

    void OnUpgradeButtonClicked(PotUpgradeData potData)
    {
        if (potUpgradeManager.TryUpgradePot(potData))
        {
            UpdateAllButtonsUI();
        }
    }

    void UpdateAllButtonsUI()
    {
        foreach (Transform child in transform)
        {
            var potRef = child.GetComponent<PotReference>();
            if (potRef != null && potRef.potData != null)
            {
                Button button = child.GetComponent<Button>();
                Image icon = child.transform.Find("Icon")?.GetComponent<Image>();
                TextMeshProUGUI levelText = child.transform.Find("Level")?.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI priceText = child.transform.Find("Price")?.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI nameText = child.transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI descriptionText = child.transform.Find("Description")?.GetComponent<TextMeshProUGUI>();

                UpdateButtonUI(potRef, icon, levelText, priceText, nameText, descriptionText, button);
            }
        }
    }
}

public class PotReference : MonoBehaviour
{
    public PotUpgradeData potData;
}