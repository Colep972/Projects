using System.Collections.Generic;
using UnityEngine;

public class SeedInventory : MonoBehaviour
{
    public static SeedInventory Instance { get; private set; }

    [SerializeField] private List<SeedData> availableSeeds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<SeedData> GetAllSeeds()
    {
        return availableSeeds;
    }

    public bool HasSeed(SeedData seed)
    {
        return availableSeeds.Contains(seed);
    }
}
