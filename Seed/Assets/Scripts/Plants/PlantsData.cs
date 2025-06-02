using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantsData
{
    public string plantName;
    public SeedData originSeed;
    public int number;
    public Sprite icon;

    public PlantsData(string name, SeedData seed, int numberOfPlant, Sprite image)
    {
        plantName = name;
        originSeed = seed;
        number = numberOfPlant;
        icon = image;
    }
}

