using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

public class UpgradeUI : MonoBehaviour
{
    [System.Serializable]
    public class UpgradeButton
    {
        public Button button;
        public UnityEngine.UI.Image icon;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI priceText;
        public TextMeshProUGUI nameText; // Ajout du nom
        public TextMeshProUGUI descriptionText; // Ajout de la description
        public UpgradeType upgradeType;
    }

    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private UpgradeButton[] upgradeButtons;

    private MoneyManager moneyManager;

    void Start()
    {
        moneyManager = MoneyManager.Instance;
        if (moneyManager == null)
        {
            UnityEngine.Debug.LogError("MoneyManager not found!");
        }

        InitializeButtons();
    }

    private void InitializeButtons()
    {
        foreach (var upgradeButton in upgradeButtons)
        {
            upgradeButton.button.onClick.AddListener(() => OnUpgradeButtonClicked(upgradeButton.upgradeType));
            UpdateButtonUI(upgradeButton);
        }
    }

    private void OnUpgradeButtonClicked(UpgradeType type)
    {
        var upgrade = upgradeManager.GetUpgrade(type);
        if (upgrade == null) return;

        int playerMoney = moneyManager.GetMoney();
        if (upgradeManager.TryPurchaseUpgrade(type, playerMoney))
        {
            // Déduire l'argent
            moneyManager.RemoveMoney(upgrade.currentPrice);
            UpdateAllButtonsUI();
        }
    }

    private void UpdateAllButtonsUI()
    {
        foreach (var upgradeButton in upgradeButtons)
        {
            UpdateButtonUI(upgradeButton);
        }
    }

    private void UpdateButtonUI(UpgradeButton upgradeButton)
    {
        Upgrade upgrade = upgradeManager.GetUpgrade(upgradeButton.upgradeType);
        if (upgrade == null) return;

        // Mise à jour de l'icône
        upgradeButton.icon.sprite = upgrade.data.icons[Mathf.Clamp(upgrade.currentLevel, 0, upgrade.data.icons.Count - 1)];


        // Mise à jour du texte
        upgradeButton.levelText.text = $"Level {upgrade.currentLevel}/{upgrade.data.maxLevel}";
        upgradeButton.priceText.text = $"${upgrade.currentPrice}";
        if (upgradeButton.nameText != null)
            upgradeButton.nameText.text = upgrade.data.upgradeName;
        if (upgradeButton.descriptionText != null)
            upgradeButton.descriptionText.text = upgrade.data.description;

        // Vérification si niveau max atteint
        bool isMaxLevel = upgrade.currentLevel >= upgrade.data.maxLevel;

        // Vérification si assez d'argent
        bool canAfford = moneyManager.GetMoney() >= upgrade.currentPrice;

        // Mise à jour de l'apparence du bouton
        //upgradeButton.button.interactable = !isMaxLevel && canAfford;

        // Choix de la couleur
        /*if (isMaxLevel)
            upgradeButton.icon.color = lockedColor;
        else if (!canAfford)
            upgradeButton.icon.color = Color.white;
        else
            upgradeButton.icon.color = Color.white;*/
    }
}