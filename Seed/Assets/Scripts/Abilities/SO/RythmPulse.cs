using UnityEngine;

[CreateAssetMenu(menuName = "Seed/Abilities/TempoFlow")]
public class TempoFlowSO : AbilitiesData
{
    [Range(0.1f, 2f)] public float targetInterval = 0.6f; // ideal click rhythm in seconds
    [Range(0f, 0.3f)] public float tolerance = 0.2f;     // timing window allowance
    [Range(0f, 1f)] public float streakBonus = 0.5f;      // each streak adds +10% click power
    [Range(0f, 1f)] public float decayRate = 0.05f;       // streak decay per off-beat click

    private float lastClickTime = -1f;
    private float streak = 0f;

    public override void ApplyBonus(ref AbilityContext ctx)
    {
        if (ctx.action != AbilityContext.ActionType.Click) return;

        float now = Time.time;

        if (lastClickTime < 0f)
        {
            lastClickTime = now;
            streak = 0f;
            return;
        }

        float delta = now - lastClickTime;
        lastClickTime = now;

        // On-beat -> grow streak
        if (Mathf.Abs(delta - targetInterval) <= tolerance)
        {
            streak += 1f;
            float bonus = 1f + (streak * streakBonus);
            ctx.clickPower *= bonus;
            Debug.Log($"[TempoFlow] On-beat! Streak {streak}, bonus x{bonus:F2}");
        }
        else
        {
            // Off-beat -> gently decay streak (but don’t punish)
            streak = Mathf.Max(0f, streak - decayRate);
            float bonus = 1f + (streak * streakBonus);
            ctx.clickPower *= bonus;
            Debug.Log($"[TempoFlow] Off-beat. Streak decays to {streak:F1}, bonus x{bonus:F2}");
        }
    }

    public override void ApplyPenalty(ref AbilityContext ctx)
    {
        // TempoFlow has no penalties — always feel-good.
    }
}
