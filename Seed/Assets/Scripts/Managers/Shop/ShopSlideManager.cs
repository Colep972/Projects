using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopSlideManager : MonoBehaviour
{
    [Header("References")]
    public RectTransform imageToSlide;              // L'image qui va slider
    public RectTransform centerPoint1;              // Premier point central
    public RectTransform centerPoint2;              // Second point central
    public Button shopButton;                       // Le bouton shop

    [Header("Animation Settings")]
    [Range(0.1f, 2f)]
    public float slideSpeed = 0.5f;                 // Vitesse de l'animation
    public AnimationCurve slideCurve = new AnimationCurve(
        new Keyframe(0, 0, 0, 1),
        new Keyframe(1, 1, 1, 0)
    );

    private bool isAtPoint1 = true;                 // True si on est au point 1, false si au point 2
    private Coroutine currentAnimation;             // Référence à l'animation en cours

    private void Start()
    {
        if (shopButton != null)
        {
            shopButton.onClick.AddListener(ToggleSlide);
        }
        else
        {
            UnityEngine.Debug.LogError("Shop button not assigned in ShopSlideManager!");
        }

        // Vérification des références
        if (imageToSlide == null || centerPoint1 == null || centerPoint2 == null)
        {
            UnityEngine.Debug.LogError("Missing references in ShopSlideManager!");
            enabled = false;
            return;
        }

        // Position initiale de l'image
        imageToSlide.position = centerPoint1.position;
    }

    public void ToggleSlide()
    {
        // Arrêter l'animation en cours si elle existe
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }

        // Démarrer la nouvelle animation
        Vector3 startPos = imageToSlide.position;
        Vector3 targetPos = isAtPoint1 ? centerPoint2.position : centerPoint1.position;

        currentAnimation = StartCoroutine(SlideAnimation(startPos, targetPos));

        // Inverser l'état
        isAtPoint1 = !isAtPoint1;
    }

    private IEnumerator SlideAnimation(Vector3 startPosition, Vector3 endPosition)
    {
        float elapsedTime = 0f;

        while (elapsedTime < slideSpeed)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / slideSpeed;

            // Utiliser la courbe d'animation pour un mouvement plus fluide
            float curveValue = slideCurve.Evaluate(normalizedTime);

            imageToSlide.position = Vector3.Lerp(startPosition, endPosition, curveValue);

            yield return null;
        }

        // S'assurer que l'image arrive exactement à la position finale
        imageToSlide.position = endPosition;
    }
}