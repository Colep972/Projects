using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotUpgradeUI : MonoBehaviour
{
    [System.Serializable]
    public class PotUpgradeButton
    {
        public Button button;
        public Image icon;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI priceText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public PotUpgradeData potData;
    }

    [SerializeField] private PotUpgradeManager potUpgradeManager;
    public PotUpgradeButton[] upgradeButtons;
    [SerializeField] private Color unlockedColor = Color.white;
    [SerializeField] private Color lockedColor = Color.gray;
    [SerializeField] private Color cantAffordColor = new Color(1, 0.5f, 0.5f);

    private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = MoneyManager.Instance;
        if (moneyManager == null)
            Debug.LogError("MoneyManager not found!");

        InitializeButtons();
        moneyManager.onMoneyChanged += (money) => UpdateAllButtonsUI();
    }

    private void OnDestroy()
    {
        if (moneyManager != null)
            moneyManager.onMoneyChanged -= (money) => UpdateAllButtonsUI();
    }

    private void InitializeButtons()
    {
        foreach (var upgradeButton in upgradeButtons)
        {
            upgradeButton.button.onClick.AddListener(() => OnUpgradeButtonClicked(upgradeButton));
            UpdateButtonUI(upgradeButton);
        }
    }

    private void OnUpgradeButtonClicked(PotUpgradeButton button)
    {
        if (potUpgradeManager.TryUpgradePot(button.potData))
        {
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

    private void UpdateButtonUI(PotUpgradeButton button)
    {
        // Icon
        button.icon.sprite = button.potData.icon;

        // Get current level and status
        int currentLevel = potUpgradeManager.GetPotLevel(button.potData.slotIndex);
        bool isMaxLevel = potUpgradeManager.IsMaxLevel(button.potData);

        // Get scaled cost
        int cost = -1;
        if (!isMaxLevel)
        {
            int globalOtherLevels = potUpgradeManager.GetGlobalUpgradeLevelsExcluding(button.potData.slotIndex);
            cost = potUpgradeManager.GetScaledUpgradePrice(button.potData, currentLevel, globalOtherLevels);
        }

        bool canAfford = cost >= 0 && moneyManager.GetMoney() >= cost;

        // Text updates
        button.nameText.text = button.potData.potName;
        button.descriptionText.text = potUpgradeManager.GetPotDescription(button.potData);
        button.levelText.text = $"Level {currentLevel}/3";
        button.priceText.text = isMaxLevel ? "MAX" : $"${cost}";

        // Interactivity and color
        button.button.interactable = !isMaxLevel && canAfford;

        if (isMaxLevel)
            button.icon.color = lockedColor;
        else if (!canAfford)
            button.icon.color = cantAffordColor;
        else
            button.icon.color = unlockedColor;
    }
}