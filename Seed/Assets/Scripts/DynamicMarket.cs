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
    public float maxMultiplier = 1.5f;

    [Header("Chance Bounds")]
    public float minChance = 0.1f;
    public float maxChance = 0.95f;

    [Header("Market Dynamics")]
    public float priceDecreasePerSale = 2f;    // baisse apr�s vente
    public float priceIncreaseOnFailure = 4f;  // hausse apr�s �chec

    public float multiplierHardeningRate = 0.05f;  // rend la vente plus dure progressivement

    // Call this on a successful sale
    public void RegisterSale()
    {
        baseMarketPrice = Mathf.Max(minMarketPrice, baseMarketPrice - priceDecreasePerSale);
        maxMultiplier = Mathf.Max(1.2f, maxMultiplier - multiplierHardeningRate);
    }

    // Call this on a failed sale
    public void RegisterFailure()
    {
        baseMarketPrice = Mathf.Min(maxMarketPrice, baseMarketPrice + priceIncreaseOnFailure);
        maxMultiplier = Mathf.Min(3.0f, maxMultiplier + multiplierHardeningRate); 
    }

    public float GetSuccessChance(float askedPrice, int quantity)
    {
        float priceRatio = askedPrice / baseMarketPrice;
        float t = Mathf.InverseLerp(minMultiplier, maxMultiplier, priceRatio);
        float quantityPenalty = Mathf.Log(quantity + 1) * 0.35f;

        float baseChance = Mathf.Lerp(maxChance, minChance, t + quantityPenalty);

        return Mathf.Clamp01(baseChance);
    }
}
