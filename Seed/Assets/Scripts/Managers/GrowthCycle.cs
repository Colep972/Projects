using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using System.Linq;

public class GrowthCycle : MonoBehaviour
{
    public int Produced { get; private set; } = 0;
    public int GrowPower { get; private set; } = 1;
    public int GrowSpeed { get; private set; } = 1;
    public int Production { get; private set; } = 1;
    public bool isAutoGrowing { get; set; } = false;
    public bool isAutoPlanting { get; set; } = false;

    public int potState;
    public int pousse = 0;
    public bool isReadyToProduce = false;
    private GameObject[] currentPoussePrefabs;
    public ParticleSystem growthParticleSystem;
    public Transform cubeTransform;
    public Renderer cubeRenderer;
    public TextMesh poussePercentageText;
    public TextMesh producedText;
    public TextMesh growPowerText;
    public TextMesh growSpeedText;
    public TextMesh productionText;
    public TextMesh autoGrowText;
    public TextMesh autoPlantText;

    public AudioSource produceAudio;
    public AudioMixerGroup sfxAudioMixerGroup;

    private const int maxStage = 4;
    private const float minScaleX = 0.06f;
    private const float maxScaleX = 0.75f;
    private GameObject currentPousse;

    private SeedData currentSeed;
    private PlantsData currentPlantData;

    public float clickPower = 1.0f;

    public bool isMouseOver = false;

    private bool hasSeedPlanted = false;

    private void Start()
    {

        if (produceAudio != null && sfxAudioMixerGroup != null)
        {
            produceAudio.outputAudioMixerGroup = sfxAudioMixerGroup;
        }

        SetTextMeshesVisibility(false);
        UpdateTextMeshes();
        hasSeedPlanted = false;
        currentSeed = null;
        UpdateSeedStateVisual();
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
        SetTextMeshesVisibility(true);
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        SetTextMeshesVisibility(false);
    }

    private void UpdateSeedStateVisual()
    {
        if (poussePercentageText == null) return;

        if (!hasSeedPlanted || currentSeed == null)
        {
            poussePercentageText.text = "SEED";
        }
        else
        {
            float percentage = Mathf.Clamp01((float)pousse / (maxStage * 10)) * 100;
            poussePercentageText.text = $"{Mathf.RoundToInt(percentage)}%";
        }
    }

    private void SetTextMeshesVisibility(bool isVisible)
    {
        if (producedText != null) producedText.gameObject.SetActive(isVisible);
        if (growPowerText != null) growPowerText.gameObject.SetActive(isVisible);
        if (growSpeedText != null) growSpeedText.gameObject.SetActive(isVisible);
        if (productionText != null) productionText.gameObject.SetActive(isVisible);
        if (autoGrowText != null) autoGrowText.gameObject.SetActive(isVisible);
        if (autoPlantText != null) autoPlantText.gameObject.SetActive(isVisible);
    }

    public void UpdatePotInfo(int growPower, int growSpeed, int production, bool autoGrow, bool autoPlant)
    {
        GrowPower = growPower;
        GrowSpeed = growSpeed;
        Production = production;
        isAutoGrowing = autoGrow;
        isAutoPlanting = autoPlant;
        UpdateTextMeshes();
    }

    public int IncrementProduced(int amount)
    {
        Produced += amount;
        UpdateTextMeshes();
        return Produced;
    }

    public void UpdateTextMeshes()
    {
        if (producedText != null) producedText.text = $"Produced: {Produced}";
        if (growPowerText != null) growPowerText.text = $"Grow Power: {GrowPower}";
        if (growSpeedText != null) growSpeedText.text = $"GrowSpeed: {GrowSpeed}";
        if (productionText != null) productionText.text = $"Production: {Production}";
        if (autoGrowText != null) autoGrowText.text = $"AutoGrow: {isAutoGrowing}";
        if (autoPlantText != null) autoPlantText.text = $"AutoPlant: {isAutoPlanting}";
    }

    public bool IncrementPousse(float clickPower)
    {
        // Si déjà prêt à produire, ne rien faire
        if (isReadyToProduce)
        {
            return false;
        }

        if (!hasSeedPlanted)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("Cannot grow! No seed planted. Seed can be found in your inventory !");
            return false; // Block growing if no seed planted
        }

        // Convertir la puissance en entier et l'ajouter à pousse
        int powerIncrement = Mathf.RoundToInt(clickPower);
        Debug.Log($"Incrementing growth by {powerIncrement} (from clickPower: {clickPower})");

        pousse += powerIncrement;

        if (currentSeed == null)
        {
            Debug.LogError("seed NULL !!!!");
        }
        else
        {
            Debug.LogError("TRUUUUC !!!");
        }
        // Calculer le stade actuel
        int currentStage = Mathf.Clamp(pousse / 10, 0, maxStage - 1);

        // Vérifier si on a atteint le stade final
        if (pousse >= maxStage * 10)
        {
            Debug.Log("Plant reached max stage!");
            SeedData plantedSeed = currentSeed;
            ActivateSeedState();
            PlantsData plantData = null;
            foreach (PlantsData plant in PlantDataManager.Instance.grownPlants)
            {
                if (plant.originSeed.seedName == plantedSeed.seedName)
                {
                    plantData = plant;
                    break;
                }
            }
            if (plantData != null)
            {
                plantData.number++;
                SeedInventoryUI.Instance.UpdatePlantSlot(plantedSeed);
            }
            return true;
        }

        // Mettre à jour les visuels
        UpdateGrowthStage(currentStage);
        UpdateCubeAppearance();

        // Calculer et afficher le pourcentage
        float percentage = Mathf.Clamp01((float)pousse / (maxStage * 10)) * 100;
        UpdateSeedStateVisual();

        return false;
    }
    
    public SeedData getCurrentSeed()
    {
        return currentSeed;
    }

    public void Plant()
    {
        if (hasSeedPlanted)
        {
            Debug.LogWarning("Seed already planted!");
            return;
        }

        SeedData selectedSeed = SeedInventoryUI.Instance.GetSelectedSeed();
        if (selectedSeed == null)
        {
            Debug.LogError("No seed selected!");
            UpdateSeedStateVisual();
            return;
        }
        currentSeed = selectedSeed;
        currentPoussePrefabs = currentSeed.poussePrefabs;
        hasSeedPlanted = true;
        pousse = 0;
        isReadyToProduce = false;
        Debug.Log("Planting seed: " + currentSeed.seedName);
        StartNewCycle();
    }

    void OnValidate()
    {
        if (!Application.isPlaying) return;

        if (pousse >= maxStage * 10 && !isReadyToProduce)
        {
            ActivateSeedState();
        }
        else if (isReadyToProduce)
        {
            poussePercentageText.text = "SEED";
        }
    }


    private void ActivateSeedState()
    {
        if (isReadyToProduce) return;

        isReadyToProduce = true;
        hasSeedPlanted = false;
        currentSeed = null;
        growthParticleSystem?.Play();
        PlayProduceAudio();
        RemovePousseVisual();
        IncrementProduced(Production);
        UpdateSeedStateVisual();
    }


    private void StartNewCycle()
    {
        if (currentSeed == null)
        {
            isReadyToProduce = true;
            UpdateSeedStateVisual();
            return;
        }
        pousse = 0;
        isReadyToProduce = false;
        UpdateCubeAppearance();
        UpdateSeedStateVisual();
    }

    private void UpdateGrowthStage(int stage)
    {
        if (currentPousse != null)
        {
            Destroy(currentPousse);
        }

        if (currentSeed == null || currentSeed.poussePrefabs == null)
        {
            Debug.LogWarning("No seed or pousse prefabs available.");
            return;
        }

        if (stage >= 0 && stage < currentSeed.poussePrefabs.Length)
        {
            GameObject pousseInstance = Instantiate(currentSeed.poussePrefabs[stage], transform);
            currentPousse = pousseInstance;

            // Applique la couleur de pousse définie dans la graine
            Color seedColor = currentSeed.pousseColor;

            foreach (var renderer in pousseInstance.GetComponentsInChildren<Renderer>())
            {
                if (renderer.material.HasProperty("_Color"))
                {
                    renderer.material.color = seedColor;
                }
            }
        }
    }


    private void RemovePousseVisual()
    {
        if (currentPousse != null)
        {
            Destroy(currentPousse);
        }
    }

    private void UpdateCubeAppearance()
    {
        if (cubeTransform != null && cubeRenderer != null)
        {
            float normalizedPousse = Mathf.Clamp01((float)pousse / (maxStage * 10));
            float newScaleX = Mathf.Lerp(minScaleX, maxScaleX, normalizedPousse);
            cubeTransform.localScale = new Vector3(newScaleX, cubeTransform.localScale.y, cubeTransform.localScale.z);

            Color newColor = Color.Lerp(Color.white, Color.green, normalizedPousse);
            cubeRenderer.material.color = newColor;
        }
    }

    private void UpdatePoussePercentageText(int percentage)
    {
        if (poussePercentageText == null) return;

        if (!hasSeedPlanted || currentSeed == null)
        {
            poussePercentageText.text = "SEED"; //Display SEED if nothing is planted
        }
        else
        {
            poussePercentageText.text = $"{percentage}%"; //Display % normally
        }
    }

    private void PlayProduceAudio()
    {
        if (produceAudio != null)
        {
            produceAudio.Play();
            Invoke(nameof(StopProduceAudio), produceAudio.clip.length);
        }
    }

    private void StopProduceAudio()
    {
        if (produceAudio != null && produceAudio.isPlaying)
        {
            produceAudio.Stop();
        }
    }
}
