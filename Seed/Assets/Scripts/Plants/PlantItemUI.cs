using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlantItemUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text idText;

    public void Set(PlantsData plant, int id)
    {
        icon.sprite = plant.icon;
        nameText.text = plant.plantName;
        idText.text = id.ToString();
    }
}
