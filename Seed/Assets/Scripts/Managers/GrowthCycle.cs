using UnityEngine;
using TMPro;
using UnityEngine.Audio;

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
    public GameObject[] poussePrefabs;
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

    public float clickPower = 1.0f;

    public bool isMouseOver = false;

    private void Start()
    {
        if (produceAudio != null && sfxAudioMixerGroup != null)
        {
            produceAudio.outputAudioMixerGroup = sfxAudioMixerGroup;
        }

        SetTextMeshesVisibility(false);
        UpdateTextMeshes();
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

        // Convertir la puissance en entier et l'ajouter à pousse
        int powerIncrement = Mathf.RoundToInt(clickPower);
        Debug.Log($"Incrementing growth by {powerIncrement} (from clickPower: {clickPower})");

        pousse += powerIncrement;

        // Calculer le stade actuel
        int currentStage = Mathf.Clamp(pousse / 10, 0, maxStage - 1);

        // Vérifier si on a atteint le stade final
        if (pousse >= maxStage * 10)
        {
            Debug.Log("Plant reached max stage!");
            ActivateSeedState();
            return true;
        }

        // Mettre à jour les visuels
        UpdateGrowthStage(currentStage);
        UpdateCubeAppearance();

        // Calculer et afficher le pourcentage
        float percentage = Mathf.Clamp01((float)pousse / (maxStage * 10)) * 100;
        UpdatePoussePercentageText(Mathf.RoundToInt(percentage));

        return false;
    }

    public void Plant()
    {
        if (!isReadyToProduce)
        {
            return;
        }
        SeedData seedToPlant = FindFirstObjectByType<SeedInventoryUI>()?.GetSelectedSeed();
        if (seedToPlant == null)
        {
            Debug.LogError("No seed selected from SeedInventoryUI!");
            return;
        }

        currentSeed = seedToPlant;
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
        growthParticleSystem?.Play();
        PlayProduceAudio();
        poussePercentageText.text = "SEED";
        RemovePousseVisual();
        IncrementProduced(Production);
    }

    private void StartNewCycle()
    {
        poussePercentageText.text = "0%";
        pousse = 0;
        isReadyToProduce = false;
        UpdateCubeAppearance();
        UpdatePoussePercentageText(0);
    }

    private void UpdateGrowthStage(int stage)
    {
        if (currentPousse != null)
        {
            Destroy(currentPousse);
        }

        /* Ici pour modifier les pousses selon les graines */

        if (stage >= 0 && stage < poussePrefabs.Length)
        {
            currentPousse = Instantiate(poussePrefabs[stage], transform);
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
        if (poussePercentageText != null)
        {
            poussePercentageText.text = $"{percentage}%";
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
