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
    [SerializeField] private float baseMarketPrice; // Prix de référence
    [SerializeField] private float maxMultiplier;    // Le joueur peut tenter de vendre jusqu’à 2x plus cher
    [SerializeField] private float minMultiplier;  // Ou vendre à perte à 50%
    [SerializeField] private float baseSuccessRate; // Taux de réussite moyen à prix correct
    [SerializeField] private float bulkBonus;       // Bonus de réussite par plante vendue

    private System.Random rng = new System.Random();


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
        baseMarketPrice = 10f;
        maxMultiplier = 2f;
        minMultiplier = 0.5f;
        baseSuccessRate = 0.6f;
        bulkBonus = 0.01f;
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
        if (currentPlants <= 0)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("Not enough plants to sell !");
            return;
        }
        if (string.IsNullOrEmpty(amountText.text)) return;
        int quantity = 1;
        int priceAsked = int.Parse(amountText.text);
        bool success = EvaluateMarketSuccess(priceAsked, quantity);
        if (success)
        {
            AddMoney(priceAsked);
            UpdatePlantDisplay(--currentPlants);
            PnjTextDisplay.Instance.DisplayMessagePublic("Sold 1 plant at " + priceAsked + " coins.");
        }
        else
        {
            UpdatePlantDisplay(--currentPlants);
            PnjTextDisplay.Instance.DisplayMessagePublic("Sale failed: Price too high or too low for market conditions.");
        }
    }

    private void SellAllPlants()
    {
        int currentPlants = GetCurrentPlantsFromPotManager();
        if (currentPlants <= 0)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("Not enough plants to sell !");
            return;
        }
        if (string.IsNullOrEmpty(amountText.text)) return;

        int priceAsked = int.Parse(amountText.text);
        int sold = 0;

        for (int i = 0; i < currentPlants; i++)
        {
            if (EvaluateMarketSuccess(priceAsked, currentPlants))
            {
                sold++;
            }
        }

        int totalEarned = sold * priceAsked;
        AddMoney(totalEarned);
        UpdatePlantDisplay(currentPlants - sold);
        PnjTextDisplay.Instance.DisplayMessagePublic("Tried to sell" + currentPlants + " ,sold " + sold + " ,earned " + totalEarned + " coins ! ");
    }

    private bool EvaluateMarketSuccess(int askedPrice, int quantity)
    {
        float priceRatio = askedPrice / baseMarketPrice;
        float successChance;

        if (priceRatio <= minMultiplier)
        {
            successChance = 0.95f; // Très bon marché = presque toujours vendu
        }
        else if (priceRatio >= maxMultiplier)
        {
            successChance = 0.1f; // Trop cher = vente très difficile
        }
        else
        {
            // Linéarise autour de baseSuccessRate
            float t = (priceRatio - minMultiplier) / (maxMultiplier - minMultiplier);
            successChance = Mathf.Lerp(0.95f, 0.1f, t);
        }

        // Ajoute un bonus si le joueur vend en grande quantité
        successChance += quantity * bulkBonus;
        successChance = Mathf.Clamp01(successChance);

        return rng.NextDouble() < successChance;
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
