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
    [SerializeField] private Button sellAllButton;
    
    private int currentMoney = 500000;

    [Header("Market Dynamics")]
    [SerializeField] private DynamicMarket dynamicMarket;


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

    public int PlantSum()
    {
        int sum = 0;
        foreach(SeedData seed in SeedInventoryUI.Instance.availableSeeds)
        {
            if(seed.unlocked)
            {
                sum += SeedInventoryUI.Instance.plantSlotMap[seed].getData().number;
            }
        }
        return sum;
    }

    public void SellPlants(int quantity)
    {
        if (quantity <= 0) return;

        PlantsData currentPlant = PlantDropdownUI.Instance.GetCurrentPlant();
        if (currentPlant == null)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("No plant selected.");
            return;
        }

        if (currentPlant.number < quantity)
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
            currentPlant.number -= quantity;
            UpdatePlantDisplay(PlantSum());
            dynamicMarket.RegisterSale();
            PnjTextDisplay.Instance.DisplayMessagePublic("Successful sell you sold " + quantity + " for " + askedPrice*quantity);
            Debug.Log($"[Market] Sale success! {quantity} of {currentPlant.plantName}, price: {askedPrice}, chance: {chance:P1}");
        }
        else
        {
           currentPlant.number -= quantity;
           UpdatePlantDisplay(PlantSum());
           dynamicMarket.RegisterFailure();
           PnjTextDisplay.Instance.DisplayMessagePublic("The sell failed you lost " + quantity);
           Debug.Log($"[Market] Sale failed. Plant: {currentPlant.plantName}, price: {askedPrice}, chance: {chance:P1}");
        }
        SeedInventoryUI.Instance.RefreshAllPlantSlots();
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
                potManager.growButton.plantesProduites = updatedPlants;
                potManager.growButton.UpdateTotalPlantesText();
            }
        }
    }
}
