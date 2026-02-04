using UnityEngine;

public struct AbilityContext
{
    public enum ActionType { Plant, Click, Harvest }

    public ActionType action;
    public SeedData seed;

    public int linkedPots;       // number of pots affected (for some bonuses)
    public float growthDelta;    // how much growth to add or remove
    public int inventoryDelta;   // how many plants to add/remove
    public System.Random rng;    // for chance-based effects
    public float clickPower;
    public bool cancelPlant;
    public float baseGrowth;
    public float growthDuration;
}
