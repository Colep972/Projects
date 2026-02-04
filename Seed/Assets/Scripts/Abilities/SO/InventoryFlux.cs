using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Seed/Abilities/InventoryFlux")]
public class InventoryFluxSO : AbilitiesData
{
    [Range(0f, 1f)] public float bonusChance = 0.4f;   // chance to trigger on harvest
    [Range(0f, 1f)] public float penaltyChance = 0.4f; // chance to shuffle inventory

    public override void ApplyBonus(ref AbilityContext ctx)
    {
        if (ctx.seed.ability != this) return;
        if (ctx.action != AbilityContext.ActionType.Harvest) return;

        if (ctx.rng.NextDouble() < bonusChance)
        {
            // Pick a random seed that is unlocked (but different from the current one)
            List<SeedData> unlocked = SeedInventoryUI.Instance.GetUnlockedSeeds();
            if (unlocked.Count > 1)
            {
                SeedData extra = unlocked[ctx.rng.Next(unlocked.Count)];
                if (extra != ctx.seed)
                {
                    SeedInventoryUI.Instance.AddToInventory(extra, 1);
                    SeedInventoryUI.Instance.AddToInventory(ctx.seed, 1);
                    Debug.Log($"[Inventory Flux] Bonus: Gained 1 extra {extra.seedName}!");
                }
            }
        }
    }

    public override void ApplyPenalty(ref AbilityContext ctx)
    {
        if (ctx.seed.ability != this) return;
        if (ctx.action != AbilityContext.ActionType.Harvest) return;

        if (ctx.rng.NextDouble() < penaltyChance)
        {
            // Convert one plant of current seed into another
            if (SeedInventoryUI.Instance.HasSeed(ctx.seed))
            {
                List<SeedData> unlocked = SeedInventoryUI.Instance.GetUnlockedSeeds();
                if (unlocked.Count > 1)
                {
                    SeedData convertTo = unlocked[ctx.rng.Next(unlocked.Count)];
                    if (convertTo != ctx.seed)
                    {
                        SeedInventoryUI.Instance.RemoveFromInventory(ctx.seed, 1);
                        SeedInventoryUI.Instance.AddToInventory(convertTo, 1);
                        Debug.Log($"[Inventory Flux] Penalty: 1 {ctx.seed.seedName} converted into {convertTo.seedName}!");
                    }
                }
            }
        }
    }
}

