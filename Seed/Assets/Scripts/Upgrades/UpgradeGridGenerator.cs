using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class UpgradeGridGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private GameObject upgradeButtonPrefab;

    [Header("Grid Settings")]
    [SerializeField] private int columns = 3;
    [SerializeField] private float spacing = 10f;
    [SerializeField] private Vector2 buttonSize = new Vector2(1, 1);

    [Header("Button Components")]
    [SerializeField] private Color unlockedColor = Color.white;
    [SerializeField] private Color lockedColor = Color.gray;
    [SerializeField] private Color cantAffordColor = new Color(1, 0.5f, 0.5f);

    private GridLayoutGroup gridLayout;
    private MoneyManager moneyManager;

    void Start()
    {
        // Obtenir le MoneyManager
        moneyManager = MoneyManager.Instance;
        if (moneyManager != null)
        {
            moneyManager.onMoneyChanged += OnMoneyChanged;
        }
        else
        {
            Debug.LogError("MoneyManager not found!");
        }

        // Vérifier et configurer le GridLayoutGroup
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
        if (upgradeManager == null || upgradeButtonPrefab == null)
        {
            Debug.LogError("Missing references in UpgradeGridGenerator!");
            return;
        }

        // Nettoyer les boutons existants
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var upgrades = upgradeManager.GetAllUpgrades().ToList();
        foreach (var upgrade in upgrades)
        {
            if (upgrade == null || upgrade.data == null) continue;

            GameObject buttonObj = Instantiate(upgradeButtonPrefab, transform);
            SetupUpgradeButton(buttonObj, upgrade.data);
        }
    }

    void SetupUpgradeButton(GameObject buttonObj, UpgradeData upgradeData)
    {
        Button button = buttonObj.GetComponent<Button>();
        Image icon = buttonObj.transform.Find("Icon")?.GetComponent<Image>();
        TextMeshProUGUI levelText = buttonObj.transform.Find("Level")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = buttonObj.transform.Find("Price")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = buttonObj.transform.Find("Description")?.GetComponent<TextMeshProUGUI>();


        if (icon == null || levelText == null || priceText == null)
        {
            Debug.LogError("Missing UI components on upgrade button!");
            return;
        }

        var upgradeRef = buttonObj.GetComponent<UpgradeReference>();
        if (upgradeRef == null)
        {
            upgradeRef = buttonObj.AddComponent<UpgradeReference>();
        }
        upgradeRef.upgradeData = upgradeData;

        Upgrade upgrade = upgradeManager.GetUpgrade(upgradeData.upgradeType);
        if (upgrade != null)
        {
            int nextPrice = upgrade.GetNextLevelPrice();
            bool canAfford = moneyManager != null && moneyManager.GetMoney() >= nextPrice;
            bool isMaxLevel = upgrade.currentLevel >= upgradeData.maxLevel;

            // Configuration de l'apparence
            if (upgradeData.icons != null && upgradeData.icons.Count > 0)
            {
                int iconIndex = Mathf.Clamp(upgrade.currentLevel, 0, upgradeData.icons.Count - 1);
                icon.sprite = upgradeData.icons[iconIndex];
            }
            else
            {
                Debug.LogWarning($"No icons defined for upgrade: {upgradeData.upgradeName}");
            }
            levelText.text = $"Lvl {upgrade.currentLevel}/{upgradeData.maxLevel}";
            priceText.text = $"${nextPrice}";
            if (descriptionText != null)
                descriptionText.text = upgradeManager.GetBasicDescription(upgrade);

            // Mise à jour de l'interactivité et de la couleur
            button.interactable = !isMaxLevel && canAfford;
            icon.color = isMaxLevel ? lockedColor :
                        !canAfford ? cantAffordColor :
                        unlockedColor;

            // Configuration du bouton
            button.onClick.RemoveAllListeners();
            if (!isMaxLevel)
            {
                button.onClick.AddListener(() => OnUpgradeButtonClicked(upgradeData));
            }
        }
    }

    void OnUpgradeButtonClicked(UpgradeData upgradeData)
    {
        if (moneyManager == null) return;

        var upgrade = upgradeManager.GetUpgrade(upgradeData.upgradeType);
        if (upgrade == null) return;

        int nextPrice = upgrade.GetNextLevelPrice();
        int playerMoney = moneyManager.GetMoney();

        if (playerMoney >= nextPrice)
        {
            if (upgradeManager.TryPurchaseUpgrade(upgradeData.upgradeType, playerMoney))
            {
                Debug.Log($"Successfully purchased upgrade: {upgradeData.name} for ${nextPrice}");
                moneyManager.RemoveMoney(nextPrice);
                UpdateAllButtonsUI();
            }
        }
        else
        {
            Debug.Log($"Not enough money to purchase upgrade: {upgradeData.name}. Needs ${nextPrice}, has ${playerMoney}");
        }
    }

    void UpdateAllButtonsUI()
    {
        foreach (Transform child in transform)
        {
            var upgradeRef = child.GetComponent<UpgradeReference>();
            if (upgradeRef != null && upgradeRef.upgradeData != null)
            {
                SetupUpgradeButton(child.gameObject, upgradeRef.upgradeData);
            }
        }
    }
}

public class UpgradeReference : MonoBehaviour
{
    public UpgradeData upgradeData;
}