using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeData> upgradeDataList;
    private Dictionary<UpgradeType, Upgrade> upgrades = new Dictionary<UpgradeType, Upgrade>();

    private PotManager potManager;
    private GrowButton growButton;

    public static UpgradeManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep SaveSystem between scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
        InitializeUpgrades();
        potManager = PotManager.Instance;
        growButton = FindFirstObjectByType<GrowButton>();

        if (potManager == null)
            Debug.LogError("PotManager not found!");
        if (growButton == null)
            Debug.LogError("GrowButton not found!");
    }

    private void InitializeUpgrades()
    {
        if (upgradeDataList == null || upgradeDataList.Count == 0)
        {
            Debug.LogError("UpgradeDataList is empty!");
            return;
        }

        foreach (var upgradeData in upgradeDataList)
        {
            if (upgradeData == null)
            {
                Debug.LogError("Null UpgradeData found in list!");
                continue;
            }
            upgrades[upgradeData.upgradeType] = new Upgrade(upgradeData);
        }
    }

    public bool TryPurchaseUpgrade(UpgradeType type, int playerMoney)
    {
        if (!upgrades.ContainsKey(type)) return false;

        var upgrade = upgrades[type];
        if (!upgrade.CanLevelUp(playerMoney)) return false;

        upgrade.LevelUp();
        ApplyUpgradeEffects(upgrade);
        return true;
    }

    public void ApplyUpgradeEffects(Upgrade upgrade)
    {
        if (potManager == null || growButton == null) return;

        switch (upgrade.data.upgradeType)
        {
            case UpgradeType.ClickPower:
                // Met à jour la puissance du clic sur le bouton Grow
                growButton.clickPower = upgrade.currentValue;
                break;

            case UpgradeType.AutoGrowPower:
                // Met à jour la puissance de croissance automatique pour tous les pots
                for (int i = 0; i < potManager.potAutomationSettings.Length; i++)
                {
                    potManager.potAutomationSettings[i].autoGrowPower = Mathf.RoundToInt(upgrade.currentValue);
                }
                break;

            case UpgradeType.AutoGrowSpeed:
                // Met à jour la vitesse de croissance automatique (plus la valeur est élevée, plus l'intervalle est court)
                float newInterval = 1f / upgrade.currentValue; // Convertit la vitesse en intervalle
                for (int i = 0; i < potManager.potAutomationSettings.Length; i++)
                {
                    potManager.potAutomationSettings[i].autoGrowInterval = newInterval;
                }
                break;

            case UpgradeType.PlantsPerHarvest:
                // Met à jour le nombre de plantes produites par récolte pour tous les pots
                for (int i = 0; i < potManager.potAutomationSettings.Length; i++)
                {
                    potManager.potAutomationSettings[i].plantsPerProduction = Mathf.RoundToInt(upgrade.currentValue);
                }
                break;
        }

        // Met à jour l'affichage des informations pour chaque pot
        foreach (Transform slot in potManager.transform)
        {
            if (slot.childCount > 0)
            {
                GrowthCycle growthCycle = slot.GetComponentInChildren<GrowthCycle>();
                if (growthCycle != null)
                {
                    // Trouve les settings correspondants à ce pot
                    int slotIndex = slot.GetSiblingIndex();
                    if (slotIndex < potManager.potAutomationSettings.Length)
                    {
                        var settings = potManager.potAutomationSettings[slotIndex];
                        growthCycle.UpdatePotInfo(
                            settings.autoGrowPower,
                            Mathf.RoundToInt(1f / settings.autoGrowInterval),
                            settings.plantsPerProduction,
                            settings.isAutoGrowing,
                            settings.isAutoPlanting
                        );
                    }
                }
            }
        }
    }

    public float GetUpgradeValue(UpgradeType type)
    {
        return upgrades.ContainsKey(type) ? upgrades[type].currentValue : 0f;
    }

    public Upgrade GetUpgrade(UpgradeType type)
    {
        return upgrades.ContainsKey(type) ? upgrades[type] : null;
    }

    public IEnumerable<Upgrade> GetAllUpgrades()
    {
        return upgrades.Values;
    }
}