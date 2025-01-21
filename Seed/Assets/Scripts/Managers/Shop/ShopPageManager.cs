using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopPageManager : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField] private GameObject upgradePage;
    [SerializeField] private GameObject sellPage;

    [Header("Buttons")]
    [SerializeField] private UnityEngine.UI.Image upgradeButton;
    [SerializeField] private UnityEngine.UI.Image sellButton;

    private UIImageEffects upgradeImageEffects;
    private UIImageEffects sellImageEffects;

    [Header("Colors")]
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = new Color(1, 1, 1, 0.5f);

    private void Awake()
    {
        upgradeImageEffects = upgradeButton.GetComponent<UIImageEffects>();
        sellImageEffects = sellButton.GetComponent<UIImageEffects>();

        if (upgradeImageEffects == null || sellImageEffects == null)
        {
            UnityEngine.Debug.LogError("Missing UIImageEffects components on shop buttons!");
        }
    }

    private void Start()
    {
        ShowUpgradePage();

        if (upgradeImageEffects != null)
        {
            upgradeImageEffects.OnPointerClick += (eventData) => ShowUpgradePage();
        }

        if (sellImageEffects != null)
        {
            sellImageEffects.OnPointerClick += (eventData) => ShowSellPage();
        }
    }

    private void OnDestroy()
    {
        // Nettoyage des événements pour éviter les fuites de mémoire
        if (upgradeImageEffects != null)
        {
            upgradeImageEffects.OnPointerClick -= (eventData) => ShowUpgradePage();
        }

        if (sellImageEffects != null)
        {
            sellImageEffects.OnPointerClick -= (eventData) => ShowSellPage();
        }
    }

    public void ShowUpgradePage()
    {
        UnityEngine.Debug.Log("Showing Upgrade Page");
        upgradePage.SetActive(true);
        sellPage.SetActive(false);

        upgradeButton.color = activeColor;
        sellButton.color = inactiveColor;
    }

    public void ShowSellPage()
    {
        UnityEngine.Debug.Log("Showing Sell Page");
        upgradePage.SetActive(false);
        sellPage.SetActive(true);

        upgradeButton.color = inactiveColor;
        sellButton.color = activeColor;
    }
}