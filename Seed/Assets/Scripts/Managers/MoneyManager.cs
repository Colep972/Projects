// MoneyManager.cs
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MoneyManager : MonoBehaviour
{
    public System.Action<int> onMoneyChanged;
    private static MoneyManager _instance;
    public static MoneyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<MoneyManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("MoneyManager");
                    _instance = go.AddComponent<MoneyManager>();
                }
            }
            return _instance;
        }
    }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI plantText;
    [SerializeField] private TextMeshProUGUI amountText;

    [Header("Buttons")]
    [SerializeField] private Button sellOneButton;
    [SerializeField] private Button sellTenButton;
    
    private int currentMoney = 0;

    [Header("Market Dynamics")]
    [SerializeField] private DynamicMarket dynamicMarket;
    [SerializeField] private bool debugLogMarket = false;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        UpdateMoneyDisplay();
        UpdatePlantDisplay();

        if (sellOneButton != null)
        {
            sellOneButton.onClick.AddListener(SellOnePlant);
        }
        else
        {
            Debug.LogError("Sell One Button is not assigned.");
        }

        if (sellTenButton != null)
        {
            sellTenButton.onClick.AddListener(SellAllPlants);
        }
        else
        {
            Debug.LogError("Sell Ten Button is not assigned.");
        }
    }

    private int GetCurrentPlantsFromPotManager()
    {
        PotManager potManager = Object.FindFirstObjectByType<PotManager>();
        if (potManager != null && potManager.growButton != null)
        {
            return potManager.growButton.totalPlantesProduites;
        }
        Debug.LogError("PotManager or GrowButton is not assigned.");
        return 0;
    }

    public int GetMoney()
    {
        return currentMoney;
    }

    public void SetMoney(int amount)
    {
        currentMoney = amount;
        UpdateMoneyDisplay();
        onMoneyChanged?.Invoke(currentMoney); // Notifier du changement
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyDisplay();
        onMoneyChanged?.Invoke(currentMoney); // Notifier du changement
    }

    public void RemoveMoney(int amount)
    {
        currentMoney = Mathf.Max(0, currentMoney - amount);
        UpdateMoneyDisplay();
        onMoneyChanged?.Invoke(currentMoney); // Notifier du changement
    }

    private void SellOnePlant()
    {
        SellPlants(1);
    }

    private void SellAllPlants()
    {
        int total = GetCurrentPlantsFromPotManager();
        SellPlants(total);
    }

    private void SellPlants(int quantity)
    {
        if (quantity <= 0) return;

        int currentPlants = GetCurrentPlantsFromPotManager();
        if (currentPlants < quantity)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("Not enough plants to sell.");
            return;
        }

        if (!int.TryParse(amountText.text, out int askedPrice))
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("Invalid amount in input field.");
            return;
        }

        float chance = dynamicMarket.GetSuccessChance(askedPrice, quantity);
        bool success = Random.value < chance;

        if (success)
        {
            AddMoney(quantity * askedPrice);
            UpdatePlantDisplay(currentPlants - quantity);
            dynamicMarket.RegisterSale();
            if (debugLogMarket) Debug.Log($"[Market] Sale success! Chance: {chance * 100f:F1}%, New base price: {dynamicMarket.baseMarketPrice}");
        }
        else
        {
            dynamicMarket.RegisterFailure();
            if (debugLogMarket) Debug.Log($"[Market] Sale failed. Chance: {chance * 100f:F1}%, New base price: {dynamicMarket.baseMarketPrice}");
        }
    }

    
    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = currentMoney.ToString();
            Debug.Log($"Money Display Updated: {currentMoney}");
        }
        else
        {
            Debug.LogError("Money Text is not assigned.");
        }
    }

    private void UpdatePlantDisplay(int updatedPlants = -1)
    {
        if (updatedPlants >= 0)
        {
            PotManager potManager = Object.FindFirstObjectByType<PotManager>();
            if (potManager != null && potManager.growButton != null)
            {
                potManager.growButton.totalPlantesProduites = updatedPlants;
                potManager.growButton.UpdateTotalPlantesText();
            }
        }
    }
}
