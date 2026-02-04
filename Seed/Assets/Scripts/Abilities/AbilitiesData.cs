using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbilitiesData : ScriptableObject
{
    public string abilityName;
    public abstract void ApplyBonus(ref AbilityContext ctx);
    public abstract void ApplyPenalty(ref AbilityContext ctx);
}
