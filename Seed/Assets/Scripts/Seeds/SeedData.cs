using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Inventory/Seed")]
public class SeedData : ScriptableObject
{
    public string seedName;
    public Sprite icon;
    public GameObject[] poussePrefabs;
    public Color pousseColor;
    public bool unlocked;
}
