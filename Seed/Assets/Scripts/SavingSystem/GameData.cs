using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public int totalPlants;
    public int coins;
    public string factoryName;
    public List<PotData> pots = new List<PotData>();
    public List<UpgradeSaveData> upgrades = new List<UpgradeSaveData>();
}

[System.Serializable]
public class PotData
{
    public int slotIndex;    // Index du slot
    public int potState;     // État du pot (niveau)
    public int pousse;       // Niveau de croissance
    public int produced;     // Plantes produites
    public bool isAutoGrow;  // Automatisation de la croissance
    public bool isAutoPlant; // Automatisation de la plantation
}

