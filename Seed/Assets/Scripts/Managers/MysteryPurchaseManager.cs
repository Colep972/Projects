using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MysteryPurchaseManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private List<MysteryEffectSO> allEffects;
    [SerializeField] private float cost = 500f;
    [SerializeField] private MoneyManager moneyManager;

    [Header("Rarity Chances")]
    [Range(0, 1)] public float commonChance = 0.5f;
    [Range(0, 1)] public float rareChance = 0.25f;
    [Range(0, 1)] public float legendaryChance = 0.15f;
    [Range(0, 1)] public float cursedChance = 0.1f;

    public void TryPurchase()
    {
        if (moneyManager.GetMoney() < cost)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("🌱 Pas assez de graines !");
            return;
        }

        moneyManager.RemoveMoney((int)cost);
        MysteryEffectSO effect = RollEffect();
        effect.ApplyEffect(moneyManager);

        string rarityText = $"[{effect.rarity}]";
        string bonusOrPenalty = effect.isBonus ? "💚 BONUS" : "💀 MALUS";

        string message = $"{rarityText} {bonusOrPenalty}\n<size=28><b>{effect.effectName}</b></size>\n{effect.description}";

        PnjTextDisplay.Instance.DisplayMessagePublic(message);
    }


    private MysteryEffectSO RollEffect()
    {
        float roll = Random.value;
        MysteryEffectSO.Rarity chosenRarity;

        if (roll < commonChance) chosenRarity = MysteryEffectSO.Rarity.Common;
        else if (roll < commonChance + rareChance) chosenRarity = MysteryEffectSO.Rarity.Rare;
        else if (roll < commonChance + rareChance + legendaryChance) chosenRarity = MysteryEffectSO.Rarity.Legendary;
        else chosenRarity = MysteryEffectSO.Rarity.Cursed;

        var valid = allEffects.Where(e => e.rarity == chosenRarity).ToList();
        if (valid.Count == 0) valid = allEffects;

        return valid[Random.Range(0, valid.Count)];
    }
}
