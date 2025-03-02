using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSaveData : MonoBehaviour
{
    public string id;        // Identifiant unique (correspond � UpgradeData)
    public int currentLevel; // Niveau actuel de l'am�lioration
    public bool isUnlocked;  // Si l'am�lioration est d�bloqu�e
    public float currentValue; // Valeur actuelle de l'am�lioration
    public int currentPrice; // Prix actuel
}
