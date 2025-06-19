using UnityEngine;
using TMPro;

public class TestUIText : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            debugText.text = "Clic d�tect� ! " + Time.time;
            debugText.gameObject.SetActive(true);
        }
    }
}
