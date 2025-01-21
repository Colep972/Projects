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
        // Mettre à jour le texte au démarrage
        UpdateText();
    }

    // Incrémente le prix et met à jour le texte
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

    // Décrémente le prix et met à jour le texte
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

    // Met à jour le texte pour afficher la valeur actuelle du prix
    private void UpdateText()
    {
        if (priceText != null)
        {
            priceText.text = currentPrice.ToString();
        }
        else
        {
            Debug.LogError("Le champ priceText n'est pas assigné !");
        }
    }

    // Récupère le prix actuel (utile pour d'autres scripts)
    public int GetPrice()
    {
        return currentPrice;
    }
}

