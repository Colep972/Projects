using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDataManager : MonoBehaviour
{
    public static PlantDataManager Instance {  get; private set; }
    [SerializeField] private List<SeedData> availableSeeds;
    [SerializeField] private List<Sprite> icons;
    public List<PlantsData> grownPlants { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        grownPlants = new List<PlantsData>();
        InitPlants();
    }
    private void InitPlants()
    {
        int cmpt = 0;
        grownPlants.Clear();
        foreach (SeedData seed in availableSeeds)
        {
            SeedData seedCopy = seed;
            string plantName = seedCopy.seedName;
            grownPlants.Add(new PlantsData(plantName, seedCopy, 0, icons[cmpt]));
            cmpt++;
        }
    }
}
