using UnityEngine;
using TMPro;

public class AutoClickerGrow : MonoBehaviour
{
    [Header("Auto Clicker Settings")]
    public bool isAutoClickerEnabled = false; // Variable bool�enne pour activer/d�sactiver l'auto-click
    public float[] autoClickIntervals = new float[4]; // Intervalle de clic pour chaque slot (en secondes)
    public float[] autoClickPowers = new float[4]; // Puissance du clic pour chaque slot
    private bool[] isAutoClicking = new bool[4]; // Statut de l'auto-clicking pour chaque slot
    private float[] autoClickTimers = new float[4]; // Timer pour chaque slot
    private GrowthCycle[] growthCycles = new GrowthCycle[4]; // R�f�rences aux cycles de croissance de chaque pot

    [Header("UI Elements")]
    public TextMeshProUGUI[] potStatusTexts; // Textes de statut pour chaque pot (auto-clicker en cours ou non)
    public AudioSource clickSound; // Son pour le clic automatique

    private void Start()
    {
        // R�cup�rer les GrowthCycles pour chaque pot
        for (int i = 0; i < 4; i++)
        {
            Transform potSlot = transform.Find($"Pot_Slot_{i + 1}");
            if (potSlot != null)
            {
                growthCycles[i] = potSlot.GetComponentInChildren<GrowthCycle>();
            }
        }

        // Initialisation de l'auto-clicking en fonction de la variable bool�enne
        ToggleAutoClicker(isAutoClickerEnabled);
    }

    private void Update()
    {
        if (isAutoClickerEnabled) // Si l'auto-clicker est activ�
        {
            for (int i = 0; i < 4; i++)
            {
                if (growthCycles[i] != null && isAutoClicking[i])
                {
                    autoClickTimers[i] += Time.deltaTime;

                    // V�rifier si le d�lai d'intervalle de clic est �coul�
                    if (autoClickTimers[i] >= autoClickIntervals[i])
                    {
                        autoClickTimers[i] = 0f;
                        AutoClick(i);
                    }
                }
            }
        }
    }

    // M�thode pour activer ou d�sactiver l'auto-click pour chaque slot
    private void ToggleAutoClicker(bool isEnabled)
    {
        for (int i = 0; i < 4; i++)
        {
            isAutoClicking[i] = isEnabled;
            UpdatePotStatusText(i);
        }
    }

    // M�thode pour effectuer un clic automatique sur un pot
    private void AutoClick(int slotIndex)
    {
        if (growthCycles[slotIndex] != null)
        {
            growthCycles[slotIndex].IncrementPousse(autoClickPowers[slotIndex]);
            PlayClickSound();
        }
    }

    // M�thode pour mettre � jour le texte de statut de chaque pot
    private void UpdatePotStatusText(int slotIndex)
    {
        if (potStatusTexts != null && potStatusTexts.Length > slotIndex)
        {
            potStatusTexts[slotIndex].text = isAutoClicking[slotIndex] ? "Auto Click ON" : "Auto Click OFF";
        }
    }

    // M�thode pour jouer le son de clic
    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            clickSound.pitch = Random.Range(0.8f, 1.2f);
            clickSound.Play();
        }
    }
}
