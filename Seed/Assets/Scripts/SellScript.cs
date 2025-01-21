using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellScript : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI priceText;

    [Header("Price Settings")]
    [SerializeField] private int currentPrice = 1;
    [SerializeField] private int minPrice = 1;
    [SerializeField] private int maxPrice = 1000;

    private void Start()
    {
        // Mettre � jour le texte au d�marrage
        UpdateText();
    }

    // Incr�mente le prix et met � jour le texte
    public void More()
    {
        if (currentPrice < maxPrice)
        {
            currentPrice++;
            UpdateText();
        }
        else
        {
            Debug.LogWarning("Le prix a atteint sa valeur maximale !");
        }
    }

    // D�cr�mente le prix et met � jour le texte
    public void Less()
    {
        if (currentPrice > minPrice)
        {
            currentPrice--;
            UpdateText();
        }
        else
        {
            Debug.LogWarning("Le prix a atteint sa valeur minimale !");
        }
    }

    // Met � jour le texte pour afficher la valeur actuelle du prix
    private void UpdateText()
    {
        if (priceText != null)
        {
            priceText.text = currentPrice.ToString();
        }
        else
        {
            Debug.LogError("Le champ priceText n'est pas assign� !");
        }
    }

    // R�cup�re le prix actuel (utile pour d'autres scripts)
    public int GetPrice()
    {
        return currentPrice;
    }
}

