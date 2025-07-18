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
    public float clickPower = 1f;  // Cette valeur sera mise à jour par l'UpgradeManager
    public int totalPlantesProduites;

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
    }

    public void SetTotalPlantesFromSave(int amount)
    {
        totalPlantesProduites = amount;
        UpdateTotalPlantesText();
    }

    private void OnClick()
    {
        PlayClickSound();
        Debug.Log($"Clicking with power: {clickPower}");

        bool hasAnyPot = false;

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
                        hasAnyPot = true;
                        bool plantProduced = growthCycle.IncrementPousse(clickPower);

                        if (plantProduced)
                        {
                            totalPlantesProduites += growthCycle.Production;
                            UpdateTotalPlantesText();
                        }
                    }
                }
            }
        }
        if (!hasAnyPot)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("You need a pot before growing a plant!");
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