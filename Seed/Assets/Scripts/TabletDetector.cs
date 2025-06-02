using UnityEngine;

public class TabletDetector : MonoBehaviour
{
    [Header("UI à adapter pour tablette")]
    public GameObject[] tabletOnlyElements;         // Objets à activer sur tablette
    public RectTransform[] elementsToResize;        // UI à agrandir sur tablette
    public float tabletScaleFactor = 1.3f;          // Ex: 1.3 = +30% de taille

    public bool forceTabletMode = false; // Pour forcer en test

    void Start()
    {
        if (IsTablet() || forceTabletMode)
        {
            Debug.Log(" Mode tablette détecté !");
            ApplyTabletSettings();
        }
    }

    bool IsTablet()
    {
        float aspectRatio = (float)Screen.width / Screen.height;

        // Ratio classique tablette = entre 1.3 et 1.6 (ex: 4:3 ou 16:10)
        return aspectRatio < 1.6f && aspectRatio > 1.3f;
    }

    void ApplyTabletSettings()
    {
        // Active les objets spécifiques à la tablette
        foreach (var obj in tabletOnlyElements)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        // Agrandit les éléments sensibles
        foreach (var rect in elementsToResize)
        {
            if (rect != null)
                rect.localScale = Vector3.one * tabletScaleFactor;
        }
    }
}
