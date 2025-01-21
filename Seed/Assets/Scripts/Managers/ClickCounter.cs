using UnityEngine;
using UnityEngine.UI;using TMPro;

public class ClickCounter : MonoBehaviour
{

    public Button button;
    public TextMeshProUGUI clicksText;
    public int clickCount = 0;

    private void Start()
    {
        if (button == null || clicksText == null)
        {
            Debug.LogError("Veuillez attribuer le bouton et le TextMeshPro dans l'inspecteur.");
            return;
        }
        button.onClick.AddListener(OnButtonClick);
        UpdateClicksText();
    }
    private void OnButtonClick()
    {
        clickCount++;
        UpdateClicksText();
    }
    private void UpdateClicksText()
    {
        clicksText.text = $"CLICKS : {clickCount}";
    }
}
