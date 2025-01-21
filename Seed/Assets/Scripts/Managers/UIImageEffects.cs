using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIImageEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Hover Effects")]
    public float hoverScaleMultiplier = 1.1f;
    public float hoverScaleSpeed = 10f;
    public Color hoverTintColor = new Color(1f, 1f, 1f, 1f);

    [Header("Click Effects")]
    public float clickScaleMultiplier = 0.9f;
    public Color clickTintColor = new Color(0.8f, 0.8f, 0.8f, 1f);

    // Ajoute un délégué pour le clic
    public delegate void ClickEventHandler(PointerEventData eventData);
    public event ClickEventHandler OnPointerClick;

    private UnityEngine.UI.Image image;
    private Vector3 originalScale;
    private Color originalColor;
    private Coroutine scaleCoroutine;
    private bool isHovered = false;

    private void Awake()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        originalScale = transform.localScale;
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleTo(hoverScaleMultiplier, hoverTintColor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleTo(1f, originalColor));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleTo(clickScaleMultiplier, clickTintColor));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleTo(isHovered ? hoverScaleMultiplier : 1f,
                                               isHovered ? hoverTintColor : originalColor));

        // Déclenche l'événement de clic
        OnPointerClick?.Invoke(eventData);
    }

    private IEnumerator ScaleTo(float targetScale, Color targetColor)
    {
        Vector3 targetScaleVector = originalScale * targetScale;

        while (Vector3.Distance(transform.localScale, targetScaleVector) > 0.001f ||
               ColorDistance(image.color, targetColor) > 0.001f)
        {
            // Scale
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                targetScaleVector,
                Time.deltaTime * hoverScaleSpeed
            );

            // Color
            image.color = Color.Lerp(
                image.color,
                targetColor,
                Time.deltaTime * hoverScaleSpeed
            );

            yield return null;
        }

        transform.localScale = targetScaleVector;
        image.color = targetColor;
    }

    private float ColorDistance(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) + Mathf.Abs(a.g - b.g) + Mathf.Abs(a.b - b.b) + Mathf.Abs(a.a - b.a);
    }
}