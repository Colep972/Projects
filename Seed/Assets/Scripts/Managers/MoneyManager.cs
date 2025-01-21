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

    [Header("Debug Tools")]
    [SerializeField] private int debugAmount = 10;
    

    private int currentMoney = 0;
  

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
        int currentPlants = GetCurrentPlantsFromPotManager();

        if (amountText == null || string.IsNullOrEmpty(amountText.text))
        {
            Debug.LogError("Amount text is null or empty");
            return;
        }
        if (currentPlants > 0)
        {
                
            AddMoney(currentPlants * int.Parse(amountText.text));
            UpdatePlantDisplay(--currentPlants);
        }
        else
        {
            Debug.LogWarning("Not enough plants to sell.");
        }
    }

    private void SellAllPlants()
    {
        int currentPlants = GetCurrentPlantsFromPotManager();
        Debug.Log("Attempting to sell all plants...");
        Debug.Log($"Before Sale: Current Plants: {currentPlants}, Current Money: {currentMoney}");

        if (currentPlants > 0)
        {
            AddMoney(currentPlants*int.Parse(amountText.text));
            UpdatePlantDisplay(0);
            Debug.Log($"Sold all plants. After Sale: Current Plants: 0, Current Money: {currentMoney}");
        }
        else
        {
            Debug.LogWarning("No plants available to sell.");
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

    public void DebugSetMoney()
    {
        SetMoney(debugAmount);
        Debug.Log($"Money set to {debugAmount} via DebugSetMoney.");
    }

    public void DebugAddMoney()
    {
        AddMoney(debugAmount);
        Debug.Log($"Added {debugAmount} money via DebugAddMoney.");
    }

    public void DebugRemoveMoney()
    {
        RemoveMoney(debugAmount);
        Debug.Log($"Removed {debugAmount} money via DebugRemoveMoney.");
    }
}
