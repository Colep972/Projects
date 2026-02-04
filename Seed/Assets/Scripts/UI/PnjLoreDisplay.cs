using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PnjLoreDisplay : MonoBehaviour
{
    [Header("Text Mesh Pro Configuration")]
    public TextMeshProUGUI textDisplay;

    [Header("Image Configuration")]
    public GameObject image1;
    public GameObject image2;

    [Header("Pot Manager Reference")]
    public PotManager potManager;

    [Header("Display Settings")]
    public float displayDuration = 3f;
    public float fadeDuration = 1f;
    public int initialMilestone = 10;
    public float milestoneMultiplier = 2f;

    private Coroutine currentCoroutine;
    private int cmptMilestone;
    private int lastMilestone = 0;
    private int nextMilestone;
    private int currentProduction;

    private CanvasGroup textCanvasGroup;
    private CanvasGroup image1CanvasGroup;
    private CanvasGroup image2CanvasGroup;

    public bool isPersistentMessageActive = false;
    public List<string> introMessages;

    private Coroutine hideCoroutine;
    public static PnjLoreDisplay Instance { get; private set; }

    public bool priority;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        cmptMilestone = 0;
        nextMilestone = initialMilestone;
        currentProduction = 0;

        Debug.Log($"Parent GameObject active: {gameObject.activeSelf}");
        foreach (Transform child in transform)
        {
            Debug.Log($"Child {child.name} active: {child.gameObject.activeSelf}");
        }


        textCanvasGroup = EnsureCanvasGroup(textDisplay?.gameObject);
        image1CanvasGroup = EnsureCanvasGroup(image1);
        image2CanvasGroup = EnsureCanvasGroup(image2);

        if (potManager == null)
        {
            Debug.LogError("PotManager reference is not assigned.");
        }
        priority = false;
        if (introMessages.Count > 0)
        {
            StartCoroutine(ShowIntroMessages());
        }
    }

    private IEnumerator ShowIntroMessages()
    {
        int cmpt = 0;
        foreach (string message in introMessages)
        {
            yield return StartCoroutine(DisplayTextRoutine(message));
            yield return new WaitForSeconds(0.5f); // petite pause entre les messages
            cmpt++;
        }
        if (cmpt == 9)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.LogError("NEXTTTTTTTTTTTTTT");
        }
    }

    void Update()
    {
        if (potManager != null)
        {
            currentProduction = potManager.growButton.totalPlantesProduites;
            if (currentProduction >= nextMilestone)
            {
                if (cmptMilestone == 6 && !SeedInventoryUI.Instance.availableSeeds[1].unlocked)
                {
                    DisplayMessagePublic("You unlocked another seed ! Continue to work hard ");
                    SeedInventoryUI.Instance.availableSeeds[1].unlocked = true;
                    SeedInventoryUI.Instance.GenerateSeedButtons();
                    PlantDropdownUI.Instance.PopulateDropdown();
                }
                if (cmptMilestone == 11 && !SeedInventoryUI.Instance.availableSeeds[2].unlocked)
                {
                    DisplayMessagePublic("You unlocked another seed ! You're close to the end");
                    SeedInventoryUI.Instance.availableSeeds[2].unlocked = true;
                    SeedInventoryUI.Instance.GenerateSeedButtons();
                    PlantDropdownUI.Instance.PopulateDropdown();
                }
                if (cmptMilestone == 15 && MoneyManager.Instance.GetMoney() > 50000 &&
                    SeedInventoryUI.Instance.plantSlotMap[SeedInventoryUI.Instance.availableSeeds[0]]
                    .GetNumber() > 500 && SeedInventoryUI.Instance.plantSlotMap[SeedInventoryUI.Instance.availableSeeds
                    [1]].GetNumber() > 500 &&
                    SeedInventoryUI.Instance.plantSlotMap[SeedInventoryUI.Instance.availableSeeds[2]]
                    .GetNumber() > 500)
                {
                    DisplayMessagePublic("You successfully finish the alpha well done ");
                }
                if (cmptMilestone != 6 && cmptMilestone != 13)
                {
                    DisplayMessagePublic("You produced " + currentProduction + " plants well done !");
                }
                lastMilestone = nextMilestone;
                nextMilestone = Mathf.CeilToInt(nextMilestone * milestoneMultiplier);
                cmptMilestone++;
            }
        }
    }

    public void DisplayMessagePublic(string message)
    {
        Debug.Log("[PNJ] Demande d’affichage : " + message);

        // Si un message persistant est affiché, on ne fait rien
        if (isPersistentMessageActive)
        {
            Debug.Log("[PNJ] Message bloqué car un message persistant est actif.");
            return;
        }

        // S'il y a déjà un message non persistant, on l'arrête proprement
        if (currentCoroutine != null)
        {
            Debug.Log("[PNJ] Message interrompu pour afficher un nouveau message.");
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        // Démarre le nouveau message
        currentCoroutine = StartCoroutine(DisplayTextRoutine(message));
    }


    private IEnumerator DisplayTextRoutine(string message)
    {
        textDisplay.text = message;
        textDisplay.gameObject.SetActive(true);
        if (image1 != null) image1.SetActive(true);
        if (image2 != null) image2.SetActive(true);

        yield return StartCoroutine(FadeElements(0, 1, fadeDuration));
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeElements(1, 0, fadeDuration));

        textDisplay.gameObject.SetActive(false);
        if (image1 != null) image1.SetActive(false);
        if (image2 != null) image2.SetActive(false);

        currentCoroutine = null;
    }

    public void HideConsoleBox()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        hideCoroutine = StartCoroutine(HideBoxRoutine());
    }

    private IEnumerator HideBoxRoutine()
    {
        yield return StartCoroutine(FadeElements(1, 0, fadeDuration));

        if (textDisplay != null) textDisplay.gameObject.SetActive(false);
        if (image1 != null) image1.SetActive(false);
        if (image2 != null) image2.SetActive(false);

        currentCoroutine = null;
    }

    private IEnumerator FadeElements(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            if (textCanvasGroup != null) textCanvasGroup.alpha = alpha;
            if (image1CanvasGroup != null) image1CanvasGroup.alpha = alpha;
            if (image2CanvasGroup != null) image2CanvasGroup.alpha = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (textCanvasGroup != null) textCanvasGroup.alpha = endAlpha;
        if (image1CanvasGroup != null) image1CanvasGroup.alpha = endAlpha;
        if (image2CanvasGroup != null) image2CanvasGroup.alpha = endAlpha;
    }

    private CanvasGroup EnsureCanvasGroup(GameObject obj)
    {
        if (obj == null) return null;

        CanvasGroup group = obj.GetComponent<CanvasGroup>();
        if (group == null)
        {
            group = obj.AddComponent<CanvasGroup>();
        }
        return group;
    }

    public bool IsMessageActive()
    {
        return currentCoroutine != null;
    }

    public void DisplayPersistentMessage(string message)
    {
        isPersistentMessageActive = true;
        textDisplay.text = message;

        textDisplay.gameObject.SetActive(true);
        if (image1 != null) image1.SetActive(true);
        if (image2 != null) image2.SetActive(true);

        // Réinitialise l'alpha au cas où
        var textCG = textDisplay.GetComponent<CanvasGroup>();
        if (textCG != null) textCG.alpha = 1f;
    }



    public void HidePersistentMessage()
    {
        isPersistentMessageActive = false;

        if (textDisplay != null) textDisplay.gameObject.SetActive(false);
        if (image1 != null) image1.SetActive(false);
        if (image2 != null) image2.SetActive(false);
    }


    public int GetNextMilestone()
    {
        return nextMilestone;
    }

    public void SetNextMilestone(int value)
    {
        nextMilestone = value;
    }

    public int GetMilestoneCount()
    {
        return cmptMilestone;
    }

    public void SetMilestoneCount(int count)
    {
        cmptMilestone = count;
    }

    public int GetCurrentProduction()
    {
        return currentProduction;
    }

    public void SetCurrentProduction(int count)
    {
        currentProduction = count;
    }

}
