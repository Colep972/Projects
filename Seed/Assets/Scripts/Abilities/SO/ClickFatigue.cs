using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ClickFatigue2")]
public class ClickFatigueSO : AbilitiesData
{
    private float lastClickTime = -1f;
    [Range(0f, 1f)] public float good = 0.50f;
    [Range(0f, 1f)] public float evil = 0.20f;

    public override void ApplyBonus(ref AbilityContext ctx)
    {
        if (ctx.action != AbilityContext.ActionType.Click) return;

        float timeNow = Time.time;
        float delta = (lastClickTime < 0f) ? 999f : timeNow - lastClickTime;
        lastClickTime = timeNow;

        // if waiting more than 1.5s → give up to 5% of total bar
        if (delta > 1.5f)
        {
            if (ctx.rng.NextDouble() < good)
            {
                float bonus = (GrowthCycle.maxStage * 10) * 0.05f; // 5% of total
                ctx.growthDelta += bonus;
                Debug.Log($"[ClickFatigue] Bonus patience! +{bonus:F1} progress");
            }
        }
    }

    public override void ApplyPenalty(ref AbilityContext ctx)
    {
        if (ctx.action != AbilityContext.ActionType.Click) return;

        float timeNow = Time.time;
        float delta = (lastClickTime < 0f) ? 999f : timeNow - lastClickTime;
        lastClickTime = timeNow;

        // if spamming faster than 0.3s => reduce growth
        if (delta < 0.3f)
        {
            if (ctx.rng.NextDouble() < evil)
            {
                float penalty = -1 * Random.Range(1f, 3f) * 0.8f; // cancel 80% of this click
                ctx.growthDelta += penalty;
                Debug.Log($"[ClickFatigue] Penalty spam! {penalty:F1} progress lost");
            }
        }
    }
}

