using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
    public GrowButton _growButton;
    public PotManager _potManager;
    public MoneyManager _moneyManager;
    public GetName _factoryName;

    public GameData GetGameData()
    {
        GameData gameData = new GameData
        {
            factoryName = _factoryName.name,
            totalPlants = _growButton.totalPlantesProduites,
            coins = _moneyManager.GetMoney(),
            unlockedUpgrades = new List<string>() /* Ajoutez les améliorations débloquées */,
            pots = new List<PotData>()
        };

        for (int i = 0; i < _potManager.potAutomationSettings.Length; i++)
        {
            PotData potData = new PotData
            {
                slotIndex = i,
                potState = _potManager.GetPotState(i),
                pousse = _potManager.GetPotPousse(i),
                produced = _potManager.GetPotProduced(i),
                isAutoGrow = _potManager.potAutomationSettings[i].isAutoGrowing,
                isAutoPlant = _potManager.potAutomationSettings[i].isAutoPlanting
            };
            gameData.pots.Add(potData);
        }

        return gameData;
    }

    public void LoadGameData(GameData gameData)
    {
        if (gameData == null) return;

        _growButton.totalPlantesProduites = gameData.totalPlants;

        for (int i = 0; i < gameData.pots.Count; i++)
        {
            PotData potData = gameData.pots[i];
            _potManager.AssignPotToSlot(_potManager.GetSlotByIndex(potData.slotIndex), potData.potState);

            Transform potSlot = _potManager.GetSlotByIndex(potData.slotIndex).transform;
            GrowthCycle growthCycle = potSlot.GetComponentInChildren<GrowthCycle>();

            if (growthCycle != null)
            {
                growthCycle.pousse = potData.pousse;
                growthCycle.IncrementProduced(potData.produced);
            }

            _potManager.potAutomationSettings[potData.slotIndex].isAutoGrowing = potData.isAutoGrow;
            _potManager.potAutomationSettings[potData.slotIndex].isAutoPlanting = potData.isAutoPlant;
        }
    }
}
