using UnityEngine;

[CreateAssetMenu(menuName = "Seed/Abilities/InventoryGamble")]
public class InventoryGambleSO : AbilitiesData
{
    [Range(0f, 1f)] public float duplicateChance = 0.2f;
    [Range(0f, 1f)] public float lossChance = 0.15f;
    public float maxAllowedDuration = 30f; // seconds before penalty risk

    public override void ApplyBonus(ref AbilityContext ctx)
    {
        if (ctx.action != AbilityContext.ActionType.Harvest) return;

        if (ctx.rng.NextDouble() < duplicateChance)
        {
            SeedInventoryUI.Instance.AddToInventory(ctx.seed, 1);
            Debug.Log("[InventoryGamble] BONUS! Duplicated plant into inventory.");
        }
    }

    public override void ApplyPenalty(ref AbilityContext ctx)
    {
        if (ctx.action != AbilityContext.ActionType.Harvest) return;

        if (ctx.growthDuration > maxAllowedDuration && ctx.rng.NextDouble() < lossChance)
        {
            SeedInventoryUI.Instance.RemoveFromInventory(ctx.seed, 1);
            Debug.Log("[InventoryGamble] PENALTY! Lost one plant due to slow growth.");
        }
    }
}
