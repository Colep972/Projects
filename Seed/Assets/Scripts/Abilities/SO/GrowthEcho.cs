using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Seed/Abilities/GrowthEcho")]
public class GrowthEchoSO : AbilitiesData
{
    [Range(0f, 1f)] public float bonusProgress = 0.05f; // 5% head start
    [Range(0f, 1f)] public float chance = 0.35f; // chance to lose the stored echo if switching

    // We keep track of which seeds have an "echo" stored
    private static Dictionary<SeedData, bool> storedEcho = new Dictionary<SeedData, bool>();

    public override void ApplyBonus(ref AbilityContext ctx)
    {
        if (ctx.seed.ability != this) return;
        // When harvesting, store an echo for this seed
        if (ctx.action == AbilityContext.ActionType.Harvest)
        {
            storedEcho[ctx.seed] = true;
            Debug.Log($"[Growth Echo] Bonus stored for {ctx.seed.seedName}!");
            return;
        }

        // When planting the same seed, consume the echo
        if (ctx.action == AbilityContext.ActionType.Plant)
        {
            if (storedEcho.ContainsKey(ctx.seed) && storedEcho[ctx.seed])
            {
                if (ctx.rng.NextDouble() < chance)
                {
                    ctx.growthDelta += (GrowthCycle.maxStage * 10) * bonusProgress;
                    storedEcho[ctx.seed] = false; // consume it
                    Debug.Log($"[Growth Echo] Bonus applied: Starting {ctx.seed.seedName} at {bonusProgress * 100}%!");
                }
            }
        }
    }

    public override void ApplyPenalty(ref AbilityContext ctx)
    {
        if (ctx.seed.ability != this) return;
        // Penalty: if player switches seeds, they might lose the echo
        if (ctx.action == AbilityContext.ActionType.Plant)
        {
            foreach (var key in new List<SeedData>(storedEcho.Keys))
            {
                if (key != ctx.seed && storedEcho[key])
                {
                    if (ctx.rng.NextDouble() < chance)
                    {
                        storedEcho[key] = false;
                        Debug.Log($"[Growth Echo] Penalty: Lost stored echo for {key.seedName} (switched seeds).");
                    }
                }
            }
        }
    }
}
