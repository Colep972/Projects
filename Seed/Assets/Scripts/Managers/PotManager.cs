
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Définition de la classe de configuration d'automation des pots
[System.Serializable]
public class PotAutomationSettings
{
    public float autoGrowInterval = 1.0f;
    public int autoGrowPower = 1;
    public float autoPlantInterval = 1.0f;
    public int plantsPerProduction = 1;
    public bool isAutoGrowing = false;
    public bool isAutoPlanting = false;
}

public class PotManager : MonoBehaviour
{
    public GrowButton growButton;

    [Header("-- SLOTS --")]
    public GameObject potSlot1;
    public GameObject potSlot2;
    public GameObject potSlot3;
    public GameObject potSlot4;

    [Header("-- PREFAB --")]
    public GameObject emptyPotPrefab;
    public GameObject potPrefab1;
    public GameObject potPrefab2;
    public GameObject potPrefab3;

    [Header("-- STATE --")]
    [SerializeField] private int _potStateSlot1 = 0;
    [SerializeField] private int _potStateSlot2 = 0;
    [SerializeField] private int _potStateSlot3 = 0;
    [SerializeField] private int _potStateSlot4 = 0;

    private readonly List<GameObject> objectsToDestroy = new List<GameObject>();


    public int GetPotState(int slotIndex)
    {
        return slotIndex switch
        {
            0 => potStateSlot1,
            1 => potStateSlot2,
            2 => potStateSlot3,
            3 => potStateSlot4,
            _ => 0
        };
    }

    public int GetPotPousse(int slotIndex)
    {
        Transform potSlot = transform.Find($"Pot_Slot_{slotIndex + 1}");
        if (potSlot != null)
        {
            GrowthCycle growthCycle = potSlot.GetComponentInChildren<GrowthCycle>();
            return growthCycle != null ? growthCycle.pousse : 0;
        }
        return 0;
    }

    public int GetPotProduced(int slotIndex)
    {
        Transform potSlot = transform.Find($"Pot_Slot_{slotIndex + 1}");
        if (potSlot != null)
        {
            GrowthCycle growthCycle = potSlot.GetComponentInChildren<GrowthCycle>();
            return growthCycle != null ? growthCycle.Produced : 0;
        }
        return 0;
    }


    // Propriétés avec mise à jour automatique
    public int potStateSlot1
    {
        get => _potStateSlot1;
        set
        {
            if (_potStateSlot1 != value)
            {
                _potStateSlot1 = value;

                if (Application.isPlaying)
                {
                    AssignPotToSlot(potSlot1, _potStateSlot1);
                }
            }
        }
    }

    public int potStateSlot2
    {
        get => _potStateSlot2;
        set
        {
            if (_potStateSlot2 != value)
            {
                _potStateSlot2 = value;
                if (Application.isPlaying)
                {
                    AssignPotToSlot(potSlot2, _potStateSlot2);
                }
            }
        }
    }

    public int potStateSlot3
    {
        get => _potStateSlot3;
        set
        {
            if (_potStateSlot3 != value)
            {
                _potStateSlot3 = value;
                if (Application.isPlaying)
                {
                    AssignPotToSlot(potSlot3, _potStateSlot3);
                }
            }
        }
    }

    public int potStateSlot4
    {
        get => _potStateSlot4;
        set
        {
            if (_potStateSlot4 != value)
            {
                _potStateSlot4 = value;
                if (Application.isPlaying)
                {
                    AssignPotToSlot(potSlot4, _potStateSlot4);
                }
            }
        }
    }

    [Header("-- AUTOMATION --")]
    public PotAutomationSettings[] potAutomationSettings = new PotAutomationSettings[4];
    private float[] autoGrowTimers = new float[4];

    [Header("-- Mouse Raycast Settings --")]
    public float raycastDistance = 10f;
    public float raycastScaleIncrease = 1.5f;
    public float scaleTransitionSpeed = 5f;
    public LayerMask potLayerMask;

    private Transform selectedPot;
    private Vector3 originalScale;

    private void Start()
    {
        // Initialiser les settings s'ils sont nuls
        for (int i = 0; i < potAutomationSettings.Length; i++)
        {
            if (potAutomationSettings[i] == null)
            {
                potAutomationSettings[i] = new PotAutomationSettings();
            }
        }
        UpdatePots();
    }

    void Update()
    {
        HandleMouseRaycastSelection();
        HandleAutomation();
        if (!Application.isPlaying)
        {
            foreach (var obj in objectsToDestroy)
            {
                DestroyImmediate(obj);
            }
            objectsToDestroy.Clear();
        }
    }

    void HandleAutomation()
    {
        for (int i = 0; i < potAutomationSettings.Length; i++)
        {
            if (potAutomationSettings[i] != null && potAutomationSettings[i].isAutoGrowing)
            {
                Debug.Log("in Grow");
                AutoGrow(i);
            }

            if (potAutomationSettings[i] != null && potAutomationSettings[i].isAutoPlanting)
            {
                Debug.Log("in Plant");
                AutoPlant(i);
            }
        }
    }


    void OnValidate()
    {
        if (Application.isPlaying)
        {
            Debug.Log("OnValidate ignoré en mode Play.");
            return;
        }

        Debug.Log("OnValidate appelé. Mise à jour des pots.");
        UpdatePots();
    }

    void UpdatePots()
    {
        Debug.Log("Mise à jour des pots...");
        AssignPotToSlot(potSlot1, _potStateSlot1);
        AssignPotToSlot(potSlot2, _potStateSlot2);
        AssignPotToSlot(potSlot3, _potStateSlot3);
        AssignPotToSlot(potSlot4, _potStateSlot4);
    }


    // Dans la classe PotManager, modifiez AssignPotToSlot :

    public void AssignPotToSlot(GameObject slot, int potState)
    {
        if (slot == null)
        {
            Debug.LogError("Slot est null. Impossible d'assigner un pot.");
            return;
        }

        // Nettoyage des enfants existants
        foreach (Transform child in slot.transform)
        {
            if (Application.isPlaying)
            {
                Destroy(child.gameObject);
            }
            else
            {
                // Au lieu de détruire immédiatement, on ajoute à la liste
                objectsToDestroy.Add(child.gameObject);
            }
        }

        // Chargement du prefab correspondant
        GameObject potPrefab = GetPotPrefab(potState);
        if (potPrefab == null)
        {
            Debug.LogWarning($"Aucun prefab correspondant pour potState = {potState}");
            return;
        }

        // Instanciation du prefab
        GameObject potInstance = Instantiate(potPrefab, slot.transform);
        potInstance.transform.localPosition = Vector3.zero;
        if (potInstance != null)
        {
            potInstance.transform.localPosition = Vector3.zero;

            // Vérifier et configurer l'automatisation selon le potState
            int slotIndex = GetSlotIndex(slot);
            if (slotIndex >= 0 && slotIndex < potAutomationSettings.Length)
            {
                if (potState == 2) // Activer AutoGrow
                {
                    StartAutoGrowForSlot(slotIndex);
                    StopAutoPlantForSlot(slotIndex);
                }
                else if (potState == 3) // Activer AutoPlant
                {
                    StartAutoPlantForSlot(slotIndex);
                    StartAutoGrowForSlot(slotIndex);
                }
                else // Désactiver les deux pour les autres states
                {
                    StopAutoGrowForSlot(slotIndex);
                    StopAutoPlantForSlot(slotIndex);
                }
            }
        }
        else
        {
            Debug.LogError($"Échec de l'instanciation pour le prefab {potPrefab.name}.");
        }
    }





    GameObject GetPotPrefab(int potState)
    {
        return potState switch
        {
            0 => emptyPotPrefab,    
            1 => potPrefab1,        
            2 => potPrefab2,
            3 => potPrefab3,
            _ => emptyPotPrefab
        };
    }

    void HandleMouseRaycastSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, potLayerMask))
        {
            if (hit.transform != selectedPot)
            {
                if (selectedPot != null)
                {
                    StartCoroutine(ScaleTransition(selectedPot, Vector3.one));
                }

                selectedPot = hit.transform;
                originalScale = selectedPot.localScale;
                StartCoroutine(ScaleTransition(selectedPot, originalScale * raycastScaleIncrease));
                UpdateInfoDisplay(selectedPot);
            }
        }
        else if (selectedPot != null)
        {
            StartCoroutine(ScaleTransition(selectedPot, Vector3.one));
            selectedPot = null;
        }
    }

    System.Collections.IEnumerator ScaleTransition(Transform pot, Vector3 targetScale)
    {
        Vector3 startScale = pot.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            pot.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime);
            elapsedTime += Time.deltaTime * scaleTransitionSpeed;
            yield return null;
        }

        pot.localScale = targetScale;
    }

    void UpdateInfoDisplay(Transform pot)
    {
        GrowthCycle growthCycle = pot.GetComponent<GrowthCycle>();
        if (growthCycle != null)
        {
            int slotIndex = GetSlotIndex(pot.parent.gameObject);
            if (slotIndex >= 0 && slotIndex < potAutomationSettings.Length)
            {
                var settings = potAutomationSettings[slotIndex];
                growthCycle.UpdatePotInfo(
                    settings.autoGrowPower,
                    Mathf.RoundToInt(1f / settings.autoGrowInterval),
                    settings.plantsPerProduction,
                    settings.isAutoGrowing,
                    settings.isAutoPlanting
                );
            }
        }
    }

    public int GetSlotIndex(GameObject slot)
    {
        if (slot == potSlot1) return 0;
        if (slot == potSlot2) return 1;
        if (slot == potSlot3) return 2;
        if (slot == potSlot4) return 3;
        return -1;
    }

    private void AutoGrow(int slotIndex)
    {
        if (Time.time >= autoGrowTimers[slotIndex])
        {
            GameObject slot = GetSlotByIndex(slotIndex);
            if (slot != null && slot.transform.childCount > 0)
            {
                GrowthCycle growthCycle = slot.GetComponentInChildren<GrowthCycle>();
                if (growthCycle != null && !growthCycle.isReadyToProduce)
                {
                    if (growthCycle.IncrementPousse(potAutomationSettings[slotIndex].autoGrowPower))
                    {
                        int plantsProduced = potAutomationSettings[slotIndex].plantsPerProduction;
                        growthCycle.IncrementProduced(plantsProduced);
                        if (growButton != null)
                        {
                            growButton.totalPlantesProduites += plantsProduced;
                            growButton.UpdateTotalPlantesText();
                        }
                    }
                }
            }
            autoGrowTimers[slotIndex] = Time.time + potAutomationSettings[slotIndex].autoGrowInterval;
        }
    }

    private void AutoPlant(int slotIndex)
    {
        GameObject slot = GetSlotByIndex(slotIndex);
        if (slot != null && slot.transform.childCount > 0)
        {
            GrowthCycle growthCycle = slot.GetComponentInChildren<GrowthCycle>();
            if (growthCycle != null && growthCycle.isReadyToProduce)
            {
                growthCycle.Plant();
            }
        }
    }

    public GameObject GetSlotByIndex(int index)
    {
        return index switch
        {
            0 => potSlot1,
            1 => potSlot2,
            2 => potSlot3,
            3 => potSlot4,
            _ => null
        };
    }

    public void StartAutoGrowForSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < potAutomationSettings.Length)
        {
            potAutomationSettings[slotIndex].isAutoGrowing = true;
            GameObject slot = GetSlotByIndex(slotIndex);
            if (slot != null)
            {
                GrowthCycle growthCycle = slot.GetComponentInChildren<GrowthCycle>();
                if (growthCycle != null)
                {
                    growthCycle.isAutoGrowing = true;
                    growthCycle.UpdateTextMeshes();
                }
            }
            autoGrowTimers[slotIndex] = Time.time + potAutomationSettings[slotIndex].autoGrowInterval;
        }
    }

    public void StopAutoGrowForSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < potAutomationSettings.Length)
        {
            potAutomationSettings[slotIndex].isAutoGrowing = false;
            GameObject slot = GetSlotByIndex(slotIndex);
            if (slot != null)
            {
                GrowthCycle growthCycle = slot.GetComponentInChildren<GrowthCycle>();
                if (growthCycle != null)
                {
                    growthCycle.isAutoGrowing = false;
                    growthCycle.UpdateTextMeshes();
                }
            }
        }
    }

    public void StartAutoPlantForSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < potAutomationSettings.Length)
        {
            potAutomationSettings[slotIndex].isAutoPlanting = true;
            GameObject slot = GetSlotByIndex(slotIndex);
            if (slot != null)
            {
                GrowthCycle growthCycle = slot.GetComponentInChildren<GrowthCycle>();
                if (growthCycle != null)
                {
                    growthCycle.isAutoPlanting = true;
                    growthCycle.UpdateTextMeshes();
                }
            }
            autoGrowTimers[slotIndex] = Time.time + potAutomationSettings[slotIndex].autoGrowInterval;
        }
    }

    public void StopAutoPlantForSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < potAutomationSettings.Length)
        {
            potAutomationSettings[slotIndex].isAutoPlanting = false;
            GameObject slot = GetSlotByIndex(slotIndex);
            if (slot != null)
            {
                GrowthCycle growthCycle = slot.GetComponentInChildren<GrowthCycle>();
                if (growthCycle != null)
                {
                    growthCycle.isAutoPlanting = false;
                    growthCycle.UpdateTextMeshes();
                }
            }
        }
    }
}