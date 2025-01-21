using UnityEngine;

[CreateAssetMenu(fileName = "New Pot Upgrade", menuName = "Upgrades/Pot Upgrade Data")]
public class PotUpgradeData : ScriptableObject
{
    public string potName;
    public Sprite icon;
    public int slotIndex; // 0-3 pour les 4 slots
    public int[] levelCosts = new int[4]; // Coût pour chaque niveau (0->1->2->3)
    [TextArea]
    public string description;
}