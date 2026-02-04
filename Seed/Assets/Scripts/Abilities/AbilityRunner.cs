using UnityEngine;

public class AbilityRunner : MonoBehaviour
{
    [SerializeField] private GrowthCycle growthCycle;
    [SerializeField] private SeedInventoryUI inventory;

    public AbilityContext HandleAction(AbilityContext.ActionType action, SeedData seed, float baseClickPower = 1f, int linkedPots = 1)
    {
        AbilityContext ctx = new AbilityContext
        {
            action = action,
            seed = seed,
            rng = new System.Random(),
            linkedPots = linkedPots,
            clickPower = baseClickPower,
            cancelPlant = false,
            baseGrowth = 0f,
            growthDuration = 5f
        };

        if (seed.ability != null)
        {
            seed.ability.ApplyBonus(ref ctx);
            seed.ability.ApplyPenalty(ref ctx);
        }


        return ctx;
    }

}
