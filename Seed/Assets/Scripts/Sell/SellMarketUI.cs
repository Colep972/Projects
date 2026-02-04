using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellMarketUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DynamicMarket dynamicMarket;   // Reference to your market system
    [SerializeField] private PlantDropdownUI plantDropdown; // The dropdown selector

    [Header("Input Fields")]
    [SerializeField] private TMP_Text priceInput;
    [SerializeField] private TMP_Text quantityInput;

    [Header("Text Displays")]
    [SerializeField] private TMP_Text basePriceText;
    [SerializeField] private TMP_Text minMaxText;
    [SerializeField] private TMP_Text successChanceText;
    [SerializeField] private TMP_Text totalGainText;
    [SerializeField] private TMP_Text moodText;

    private PlantsData currentPlant;

    private void Start()
    {
        // Example setup (optional)
        if (plantDropdown != null)
        {
            currentPlant = plantDropdown.GetCurrentPlant();
            Debug.LogError(currentPlant+ " 2");
        }
        else
            Debug.LogError(currentPlant + " 2");
        UpdateMarketInfo(); // Initialize display
    }

    private void Update()
    {
        // Keep UI synced when player types
        UpdateMarketInfo();
    }

    public void UpdateMarketInfo()
    {
        if (currentPlant == null || dynamicMarket == null)
        {
            Debug.LogError(currentPlant);
            Debug.LogError(dynamicMarket);
            return;
        }


        if (!float.TryParse(priceInput.text, out float askedPrice))
            askedPrice = dynamicMarket.baseMarketPrice;
        if (!int.TryParse(quantityInput.text, out int quantity))
            quantity = 1;

        float chance = dynamicMarket.GetSuccessChance(askedPrice, quantity);
        float totalGain = askedPrice * quantity;

        string marketStatus = dynamicMarket.baseMarketPrice > (dynamicMarket.maxMarketPrice * 0.8f)
            ? "Rising"
            : dynamicMarket.baseMarketPrice < (dynamicMarket.minMarketPrice * 1.2f)
                ? "Falling"
                : "Stable";

        basePriceText.text = $"Base Price: {dynamicMarket.baseMarketPrice:F2}$";
        minMaxText.text = $"Range: {dynamicMarket.minMarketPrice:F0}$ – {dynamicMarket.maxMarketPrice:F0}$";
        successChanceText.text = $"Success Chance: {chance * 100:F1}%";
        totalGainText.text = $"Total: {totalGain:F2}$";
        moodText.text = $"Market Status: {marketStatus}";
        Debug.LogError("UpdateMarketInfo() ran");
    }

    public void AddOneToPrice()
    {
        int currentValue = int.Parse(priceInput.text);
        currentValue++;
        priceInput.text = currentValue.ToString();
    }

    public void RemoveOneFromPrice()
    {
        int currentValue = int.Parse(priceInput.text);
        currentValue = Mathf.Max(0, currentValue - 1); // prevents going below 0
        priceInput.text = currentValue.ToString();
    }

    public void AddOneToQuantity()
    {
        int currentValue = int.Parse(quantityInput.text);
        currentValue++;
        quantityInput.text = currentValue.ToString();
    }

    public void RemoveOneFromQuantity()
    {
        int currentValue = int.Parse(quantityInput.text);
        currentValue = Mathf.Max(0, currentValue - 1);
        quantityInput.text = currentValue.ToString();
    }
}
