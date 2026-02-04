using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Seed/Abilities/LuckySprout")]
public class LuckySproutSO : AbilitiesData
{
    public float startChance = 0.3f;     // chance for bonus
    public float failChance = 0.3f;      // chance for penalty
    public float startProgress = 0.25f;   // progress percentage (0-1)

    public override void ApplyBonus(ref AbilityContext ctx)
    {
        if (ctx.action != AbilityContext.ActionType.Plant) return;
        if (ctx.rng.NextDouble() < startChance)
        {
            ctx.growthDelta = Mathf.RoundToInt(GrowthCycle.maxStage * 10 * startProgress);
            Debug.Log("[Lucky Sprout] Bonus applied!");
        }
    }

    public override void ApplyPenalty(ref AbilityContext ctx)
    {
        if (ctx.action != AbilityContext.ActionType.Plant) return;
        if (ctx.rng.NextDouble() < failChance)
        {
            ctx.cancelPlant = true;
            Debug.Log("[Lucky Sprout] Penalty applied!");
        }
    }
}
