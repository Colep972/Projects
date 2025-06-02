using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrowButton : MonoBehaviour
{
    [Header("UI Components")]
    public Button button;
    public TextMeshProUGUI totalPlantesText;
    public TextMeshProUGUI all;
    public TextMeshProUGUI totalSellablePlants;

    [Header("Game Settings")]
    public Transform potsParent;
    public float clickPower = 1.0f;  // Cette valeur sera mise à jour par l'UpgradeManager
    public int totalPlantesProduites = 0;

    [Header("Audio Settings")]
    [SerializeField] public AudioSource clickSound;
    [SerializeField] private AudioScript audioScript;

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("Button not assigned in GrowButton!");
        }

        if (potsParent == null)
        {
            Debug.LogError("PotsParent not assigned in GrowButton!");
        }

        UpdateTotalPlantesText();
    }

    private void OnClick()
    {
        PlayClickSound();

        // Debug pour vérifier la puissance actuelle
        Debug.Log($"Clicking with power: {clickPower}");

        // Parcourir tous les pots
        for (int i = 1; i <= 4; i++)
        {
            Transform potSlot = potsParent.Find($"Pot_Slot_{i}");
            if (potSlot != null && potSlot.childCount > 0)
            {
                Transform potChild = potSlot.GetChild(0);
                if (potChild != null)
                {
                    GrowthCycle growthCycle = potChild.GetComponent<GrowthCycle>();
                    if (growthCycle != null)
                    {
                        // Utiliser la puissance de clic pour faire pousser la plante
                        bool plantProduced = growthCycle.IncrementPousse(clickPower);
                        

                        // Si une plante a été produite
                        if (plantProduced)
                        {
                            totalPlantesProduites += growthCycle.Production;
                            UpdateTotalPlantesText();
                        }
                    }
                }
            }
        }
    }

    public void UpdateTotalPlantesText()
    {
        if (totalPlantesText != null)
        {
            totalPlantesText.text = totalPlantesProduites.ToString();
        }

        if (all != null)
        {
            all.text = totalPlantesProduites.ToString();
        }

        if(totalSellablePlants != null)
        {
            totalSellablePlants.text = totalPlantesProduites.ToString();
        }
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            clickSound.pitch = Random.Range(0.8f, 1.2f);
            if (audioScript != null)
            {
                audioScript.PlaySound(clickSound);
            }
        }
    }
}