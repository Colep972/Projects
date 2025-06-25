using System.Collections;
using UnityEngine;
using TMPro;

public class PnjTextDisplay : MonoBehaviour
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
    public float milestoneMultiplier = 1.5f;

    private Coroutine currentCoroutine;
    private int cmptMilestone;
    private int lastMilestone = 0;
    private int nextMilestone;

    private CanvasGroup textCanvasGroup;
    private CanvasGroup image1CanvasGroup;
    private CanvasGroup image2CanvasGroup;

    public bool isPersistentMessageActive = false;
    public static PnjTextDisplay Instance { get; private set; }


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

        textCanvasGroup = EnsureCanvasGroup(textDisplay?.gameObject);
        image1CanvasGroup = EnsureCanvasGroup(image1);
        image2CanvasGroup = EnsureCanvasGroup(image2);

        if (textDisplay != null)
        {
            textDisplay.gameObject.SetActive(false);
            textCanvasGroup.alpha = 1;
        }

        if (image1 != null) image1.SetActive(false);
        if (image2 != null) image2.SetActive(false);

        if (potManager == null)
        {
            Debug.LogError("PotManager reference is not assigned.");
        }
    }

    void Update()
    {
        if (potManager != null)
        {
            int currentProduction = potManager.growButton.totalPlantesProduites;

            if (currentProduction >= nextMilestone)
            {
                if (cmptMilestone > 5)
                {
                    SeedInventoryUI.Instance.availableSeeds[1].unlocked = true;
                    SeedInventoryUI.Instance.GenerateSeedButtons();
                }
                if (cmptMilestone > 12)
                {
                    SeedInventoryUI.Instance.availableSeeds[2].unlocked = true;
                    SeedInventoryUI.Instance.GenerateSeedButtons();
                }
                DisplayMessagePublic("Vous avez produit " + currentProduction + " plante.s bravo !");
                lastMilestone = nextMilestone;
                nextMilestone = Mathf.CeilToInt(nextMilestone * milestoneMultiplier);
                cmptMilestone++;
            }
        }
    }

    public void DisplayMessagePublic(string message)
    {
        Debug.Log("[PNJ] Demande d’affichage : " + message);

        if (currentCoroutine != null)
        {
            Debug.Log("[PNJ] Message ignoré car un message est déjà en cours.");
            return;
        }
        if (isPersistentMessageActive)
        {
            Debug.Log("[PNJ] Message bloqué car un message persistant est actif.");
            return;
        }
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
        }
        currentCoroutine = StartCoroutine(HideBoxRoutine());
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
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        isPersistentMessageActive = true;

        textDisplay.text = message;
        textDisplay.gameObject.SetActive(true);
        if (image1 != null) image1.SetActive(true);
        if (image2 != null) image2.SetActive(true);

        var textCG = textDisplay.GetComponent<CanvasGroup>();
        if (textCG != null) textCG.alpha = 1f;
        if (image1 != null && image1.TryGetComponent(out CanvasGroup cg1)) cg1.alpha = 1f;
        if (image2 != null && image2.TryGetComponent(out CanvasGroup cg2)) cg2.alpha = 1f;
    }


}
