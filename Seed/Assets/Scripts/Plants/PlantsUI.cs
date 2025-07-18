using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantsUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text plantNameText;
    public TMP_Text countText;

    private PlantsData plantData;

    public void Set(PlantsData data)
    {
        plantData = data;
        iconImage.sprite = data.icon;
        plantNameText.text = data.plantName;
        UpdateCount();
    }

    public void UpdateCount()
    {
        countText.text = plantData.number.ToString();

    }

    public int GetNumber()
    {
        return plantData.number;
    }

    public void SetNumber(int number)
    {
        plantData.number = number;
        UpdateCount();
    }

    public PlantsData getData()
    {
        return plantData;
    }
}
