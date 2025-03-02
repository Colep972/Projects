using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSaveData : MonoBehaviour
{
    public string id;        // Identifiant unique (correspond à UpgradeData)
    public int currentLevel; // Niveau actuel de l'amélioration
    public bool isUnlocked;  // Si l'amélioration est débloquée
    public float currentValue; // Valeur actuelle de l'amélioration
    public int currentPrice; // Prix actuel
}
