using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Volatile Growth")]
public class VolatileGrowth : AbilitiesData
{
    public override void ApplyBonus(ref AbilityContext ctx)
    {
        if (ctx.action != AbilityContext.ActionType.Click) return;

        // 10% chance to boost the click
        if (ctx.rng.NextDouble() < 0.4f)
        {
            float boost = Random.Range(1.05f, 1.1f); // 110–120% of normal
            float increase = (boost - 1f) * 100f; 
            ctx.clickPower *= increase;
            Debug.Log($"[VolatileGrowth] BONUS! +{ctx.clickPower:F1}% click effectiveness");
        }
    }


    public override void ApplyPenalty(ref AbilityContext ctx)
    {
        if (ctx.action != AbilityContext.ActionType.Click) return;

        // 5% chance for small setback
        if (ctx.rng.NextDouble() < 0.3f)
        {
            float setback = Random.Range(-0.9f, -0.95f);   // keep 10%–90% of original power
            ctx.clickPower *= setback;                 // shrink to that percentage
            float reduction = (1f - setback) * 100f;   // just for logging
            Debug.Log($"[VolatileGrowth] PENALTY! -{reduction:F1}% click effectiveness");
        }
    }

}
