using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MysteryEffect", menuName = "Seed/Mystery Effect")]
public class MysteryEffectSO : ScriptableObject
{
    public enum Rarity { Common, Rare, Legendary, Cursed }
    public enum EffectType { AddMoney, RemoveMoney, BoostGrowth, SlowGrowth, AddUpgrade,RemoveUpgrade , AddPlant,
        RemovePlant, Other }

    [Header("Effect Info")]
    public string effectName;
    [TextArea] public string description;
    public Sprite icon;
    public bool isBonus;
    public Rarity rarity;
    public EffectType type;
    public float value = 1f;
    public float duration = 0f;

    public void ApplyEffect(MoneyManager moneyManager)
    {
        UpgradeManager upgradeManager = UpgradeManager.Instance;
        GrowthCycle growthCycle = FindFirstObjectByType<GrowthCycle>();
        switch (type)
        {
            case EffectType.AddMoney:
                moneyManager.AddMoney((int)value);
                break;

            case EffectType.RemoveMoney:
                moneyManager.RemoveMoney((int)value);
                break;

            // Optional: trigger other systems
            case EffectType.BoostGrowth:
                if (growthCycle != null)
                {
                    growthCycle.GrowSpeed = Mathf.RoundToInt(growthCycle.GrowSpeed * (1 + (value/100)));
                    PnjTextDisplay.Instance.DisplayMessagePublic
                    (
                        $"{effectName}! Growth speed boosted x{value} for {duration}s!"
                    );
                    growthCycle.StartCoroutine(ResetGrowthSpeedAfter(growthCycle, duration));
                }
                break;

            case EffectType.SlowGrowth:
                if (growthCycle != null)
                {
                    growthCycle.GrowSpeed = Mathf.RoundToInt(growthCycle.GrowSpeed * value);
                    PnjTextDisplay.Instance.DisplayMessagePublic
                    (
                        $"{effectName}! Growth slowed x{value} for {duration}s..."
                    );
                    growthCycle.StartCoroutine(ResetGrowthSpeedAfter(growthCycle, duration));
                }
                break;

            case EffectType.AddUpgrade:
                {
                    var upgrades = new List<Upgrade>(upgradeManager.GetAllUpgrades());
                    if (upgrades.Count == 0) return;

                    var randomUpgrade = upgrades[UnityEngine.Random.Range(0, upgrades.Count)];
                    randomUpgrade.LevelUp();
                    upgradeManager.ApplyUpgradeEffects(randomUpgrade);

                    PnjTextDisplay.Instance.DisplayMessagePublic(
                        $"{effectName}! {randomUpgrade.data.upgradeName} improved by 1 level!"
                    );
                    break;
                }

            case EffectType.RemoveUpgrade:
                {
                    var upgrades = new List<Upgrade>(upgradeManager.GetAllUpgrades());
                    if (upgrades.Count == 0) return;

                    var randomUpgrade = upgrades[UnityEngine.Random.Range(0, upgrades.Count)];
                    bool downgraded = randomUpgrade.LevelDown();

                    if (downgraded)
                    {
                        upgradeManager.ApplyUpgradeEffects(randomUpgrade);
                        PnjTextDisplay.Instance.DisplayMessagePublic(
                            $"{effectName}! {randomUpgrade.data.upgradeName} lost a level..."
                        );
                    }
                    else
                    {
                        PnjTextDisplay.Instance.DisplayMessagePublic(
                            $"{effectName}! All upgrades are already at minimum level."
                        );
                    }
                    break;
                }


            case EffectType.AddPlant:
                SeedInventoryUI.Instance.AddPlant((int)value);
                break;

            case EffectType.RemovePlant:
                SeedInventoryUI.Instance.RemovePlant((int)value);
                break;

            default:
                Debug.Log("Special or custom effect triggered.");
                break;
        }

        Debug.Log($"Mystery Effect Applied → {effectName} ({(isBonus ? "Bonus" : "Penalty")})");
    }

    private IEnumerator ResetGrowthSpeedAfter(GrowthCycle growthCycle, float duration)
    {
        int originalSpeed = growthCycle.GrowSpeed;
        yield return new WaitForSeconds(duration);
        growthCycle.GrowSpeed = 1; // or whatever your normal default is
        PnjTextDisplay.Instance.DisplayMessagePublic("Growth speed returned to normal.");
    }
}
