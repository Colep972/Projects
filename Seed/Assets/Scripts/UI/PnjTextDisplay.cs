using System.Collections;
using UnityEngine;
using TMPro;

public class PnjTextDisplay : MonoBehaviour
{
    [Header("Text Mesh Pro Configuration")]
    public TextMeshProUGUI textDisplay; // TextMeshPro component to show the text

    [Header("Image Configuration")]
    public GameObject image1; // First image to display
    public GameObject image2; // Second image to display

    [Header("Pot Manager Reference")]
    public PotManager potManager; // Reference to the PotManager

    [Header("Display Settings")]
    public float displayDuration = 3f; // Duration the text will be visible
    public float fadeDuration = 1f; // Duration of the fade-out effect
    public int initialMilestone = 10; // Initial number of plants for the first milestone
    public float milestoneMultiplier = 1.5f; // Multiplier for the exponential milestones

    private Coroutine currentCoroutine;
    private int lastMilestone = 0;
    private int nextMilestone;

    void Start()
    {
        nextMilestone = initialMilestone;

        if (textDisplay != null)
        {
            EnsureCanvasGroup(textDisplay.gameObject);
            textDisplay.gameObject.SetActive(false);
            textDisplay.alpha = 1; // Ensure the text starts fully visible (for debugging)
        }
        else
        {
            Debug.LogError("Text Mesh Pro component is not assigned.");
        }

        if (image1 != null)
        {
            EnsureCanvasGroup(image1);
            image1.SetActive(false);
        }

        if (image2 != null)
        {
            EnsureCanvasGroup(image2);
            image2.SetActive(false);
        }

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
                Debug.Log($"Displaying message for milestone: {currentProduction}");
                DisplayMessage($"Vous avez produit {currentProduction} plante.s bravo !");
                lastMilestone = nextMilestone;
                nextMilestone = Mathf.CeilToInt(nextMilestone * milestoneMultiplier);
            }
        }
    }

    private void DisplayMessage(string message)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(DisplayTextRoutine(message));
    }

    private IEnumerator DisplayTextRoutine(string message)
    {
        if (textDisplay != null)
        {
            textDisplay.text = message;
            textDisplay.gameObject.SetActive(true);
            if (image1 != null) image1.SetActive(true);
            if (image2 != null) image2.SetActive(true);

            Debug.Log("Text and images are now visible.");

            // Fade in
            yield return StartCoroutine(FadeElements(0, 1, fadeDuration));

            // Wait for the display duration
            yield return new WaitForSeconds(displayDuration);

            // Fade out
            yield return StartCoroutine(FadeElements(1, 0, fadeDuration));

            textDisplay.gameObject.SetActive(false);
            if (image1 != null) image1.SetActive(false);
            if (image2 != null) image2.SetActive(false);
        }
        else
        {
            Debug.LogError("Text display is null and cannot be shown.");
        }
    }

    public void DisplayMessagePublic(string message)
    {
        DisplayMessage(message);
    }

    

    private IEnumerator FadeElements(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        CanvasGroup textCanvasGroup = textDisplay.GetComponent<CanvasGroup>();
        CanvasGroup image1CanvasGroup = image1 != null ? image1.GetComponent<CanvasGroup>() : null;
        CanvasGroup image2CanvasGroup = image2 != null ? image2.GetComponent<CanvasGroup>() : null;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            if (textCanvasGroup != null) textCanvasGroup.alpha = alpha;
            if (image1CanvasGroup != null) image1CanvasGroup.alpha = alpha;
            if (image2CanvasGroup != null) image2CanvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (textCanvasGroup != null) textCanvasGroup.alpha = endAlpha;
        if (image1CanvasGroup != null) image1CanvasGroup.alpha = endAlpha;
        if (image2CanvasGroup != null) image2CanvasGroup.alpha = endAlpha;
    }

    public void HideConsoleBox()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FadeElements(1, 0, fadeDuration));

        // After the fade out, fully disable the objects
        StartCoroutine(DisableAfterFade());
    }

    private IEnumerator DisableAfterFade()
    {
        yield return new WaitForSeconds(fadeDuration);

        if (textDisplay != null) textDisplay.gameObject.SetActive(false);
        if (image1 != null) image1.SetActive(false);
        if (image2 != null) image2.SetActive(false);
    }

    private void EnsureCanvasGroup(GameObject obj)
    {
        if (obj.GetComponent<CanvasGroup>() == null)
        {
            obj.AddComponent<CanvasGroup>();
        }
    }
}
