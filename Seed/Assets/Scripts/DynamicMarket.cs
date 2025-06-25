using UnityEngine;

[System.Serializable]
public class DynamicMarket
{
    [Header("Base Market Price")]
    public float baseMarketPrice = 100f;
    public float minMarketPrice = 50f;
    public float maxMarketPrice = 200f;

    [Header("Multipliers")]
    public float minMultiplier = 0.5f;
    public float maxMultiplier = 2.0f;

    [Header("Chance Bounds")]
    public float minChance = 0.1f;
    public float maxChance = 0.95f;

    [Header("Market Dynamics")]
    public float priceDecreasePerSale = 1f;    // baisse après vente
    public float priceIncreaseOnFailure = 2f;  // hausse après échec

    public float multiplierHardeningRate = 0.01f;  // rend la vente plus dure progressivement

    // Call this on a successful sale
    public void RegisterSale()
    {
        baseMarketPrice = Mathf.Max(minMarketPrice, baseMarketPrice - priceDecreasePerSale);
        maxMultiplier = Mathf.Max(1.2f, maxMultiplier - multiplierHardeningRate); // moins de tolérance
    }

    // Call this on a failed sale
    public void RegisterFailure()
    {
        baseMarketPrice = Mathf.Min(maxMarketPrice, baseMarketPrice + priceIncreaseOnFailure);
        maxMultiplier = Mathf.Min(3.0f, maxMultiplier + multiplierHardeningRate); // plus de tolérance
    }

    // Calculate chance of successful sale
    public float GetSuccessChance(float askedPrice, int quantity)
    {
        float priceRatio = askedPrice / baseMarketPrice;
        float t = Mathf.InverseLerp(minMultiplier, maxMultiplier, priceRatio);
        float baseChance = Mathf.Lerp(maxChance, minChance, t);

        // Bonus sur la quantité vendue
        float bulkBonus = Mathf.Log(quantity + 1) * 0.05f;
        return Mathf.Clamp01(baseChance + bulkBonus);
    }
}
