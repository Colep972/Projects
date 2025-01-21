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
    [SerializeField] private PotUpgradeButton[] upgradeButtons;
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
        // Mise à jour de l'icône
        button.icon.sprite = button.potData.icon;

        // Obtenir le niveau actuel et les informations
        bool isMaxLevel = potUpgradeManager.IsMaxLevel(button.potData);
        int cost = potUpgradeManager.GetUpgradeCost(button.potData);
        bool canAfford = cost >= 0 && moneyManager.GetMoney() >= cost;

        // Mettre à jour les textes
        button.nameText.text = button.potData.potName;
        button.descriptionText.text = button.potData.description;

        // Pour le texte de niveau, montrer le niveau actuel et max
        int currentLevel = cost >= 0 ? isMaxLevel ? 3 : cost / button.potData.levelCosts[0] : 3;
        button.levelText.text = $"Level {currentLevel}/3";

        // Pour le prix, montrer le coût du prochain niveau ou "MAX"
        button.priceText.text = isMaxLevel ? "MAX" : $"${cost}";

        // Mise à jour de l'apparence
        button.button.interactable = !isMaxLevel && canAfford;

        // Choix de la couleur
        if (isMaxLevel)
            button.icon.color = lockedColor;
        else if (!canAfford)
            button.icon.color = cantAffordColor;
        else
            button.icon.color = unlockedColor;
    }
}