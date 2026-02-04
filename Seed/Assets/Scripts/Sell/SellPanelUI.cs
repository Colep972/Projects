using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellPanelUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown plantDropdown;
    [SerializeField] private TMP_InputField quantityInput;
    [SerializeField] private TMP_InputField priceInput;

    [Header("Market Info UI")]
    [SerializeField] private TextMeshProUGUI basePriceText;
    [SerializeField] private TextMeshProUGUI minMaxText;
    [SerializeField] private TextMeshProUGUI successChanceText;
    [SerializeField] private TextMeshProUGUI moodText;
    [SerializeField] private TextMeshProUGUI totalGainText;
    [SerializeField] private TextMeshProUGUI feedbackText;

    [Header("References")]
    [SerializeField] private PlantDropdownUI plantDropdownUI;
    [SerializeField] private DynamicMarket dynamicMarket;

    private PlantsData currentPlant;

    void Start()
    {
        // Initial refresh
        UpdateMarketInfo();

        // Add listeners
        quantityInput.onValueChanged.AddListener(delegate { UpdateMarketInfo(); });
        priceInput.onValueChanged.AddListener(delegate { UpdateMarketInfo(); });
        if (plantDropdownUI != null)
            plantDropdownUI.onPlantSelected.AddListener(OnPlantSelected);
    }

    private void OnPlantSelected(PlantsData newPlant)
    {
        currentPlant = newPlant;
        Debug.Log($"Selected plant: {newPlant.plantName}");
        UpdateMarketInfo();
    }

    public void UpdateMarketInfo()
    {
        if (currentPlant == null || dynamicMarket == null) return;

        // --- Inputs ---
        float basePrice = dynamicMarket.baseMarketPrice;
        float minPrice = dynamicMarket.minMarketPrice;
        float maxPrice = dynamicMarket.maxMarketPrice;

        float askedPrice = float.TryParse(priceInput.text, out var p) ? p : basePrice;
        int quantity = int.TryParse(quantityInput.text, out var q) ? q : 1;

        // --- Calculations ---
        float chance = dynamicMarket.GetSuccessChance(askedPrice, quantity);
        float totalGain = askedPrice * quantity;
        float priceRatio = askedPrice / basePrice;

        // --- UI Updates ---
        basePriceText.text = $"Base: {basePrice:F2}$";
        minMaxText.text = $"?? {minPrice:F2}$ - {maxPrice:F2}$";
        successChanceText.text = $"Chance: {chance * 100:F1}%";
        totalGainText.text = $"Total: {totalGain:F2}$";

        // --- Feedback ---
        if (priceRatio < 0.9f)
            feedbackText.text = "Below market price — quick to sell.";
        else if (priceRatio < 1.2f)
            feedbackText.text = "Fair price — balanced odds.";
        else if (chance > 0.6f)
            feedbackText.text = "Pricey but still reasonable.";
        else
            feedbackText.text = "Too expensive — low demand.";

        // Optional: mood or trend if you want to show dynamic feedback
        if (moodText != null)
            moodText.text = GetMarketMood(askedPrice, chance);
    }


    private string GetMarketMood(float askedPrice, float chance)
    {
        if (chance > 0.75f) return "Favorable (Buyers Active)";
        if (chance > 0.4f) return "Stable (Balanced Market)";
        return "Harsh (Few Buyers)";
    }

    public void OnConfirmSell()
    {
        if (!float.TryParse(priceInput.text, out float askedPrice)) return;
        if (!int.TryParse(quantityInput.text, out int quantity)) return;

        float chance = dynamicMarket.GetSuccessChance(askedPrice, quantity);
        bool success = Random.value <= chance;

        if (success)
        {
            dynamicMarket.RegisterSale();
            Debug.Log($"Sale success! You sold {quantity} items at {askedPrice}$ each.");
        }
        else
        {
            dynamicMarket.RegisterFailure();
            Debug.Log("Sale failed! Buyers refused your price.");
        }

        UpdateMarketInfo();
    }
}
