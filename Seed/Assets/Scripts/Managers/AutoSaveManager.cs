using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoSaveManager : MonoBehaviour
{
    public Toggle autoSaveToggle;
    public float autoSaveInterval = 300f; // 5 minutes (en secondes)

    private Coroutine autoSaveCoroutine;

    private void Start()
    {
        // Ajouter l'�couteur � la checkbox
        autoSaveToggle.onValueChanged.AddListener(OnToggleChanged);

        // Si la checkbox est d�j� coch�e au d�marrage, on commence � autosave
        if (autoSaveToggle.isOn)
        {
            StartAutoSave();
        }
    }

    private void OnDestroy()
    {
        // Important pour �viter les erreurs
        autoSaveToggle.onValueChanged.RemoveListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            StartAutoSave();
            PnjTextDisplay.Instance.DisplayMessagePublic("Auto save is on");
        }
        else
        {
            StopAutoSave();
            PnjTextDisplay.Instance.DisplayMessagePublic("Auto save is off");
        }
            
    }
    

    private void StartAutoSave()
    {
        if (autoSaveCoroutine == null)
            autoSaveCoroutine = StartCoroutine(AutoSaveRoutine());
    }

    private void StopAutoSave()
    {
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
            autoSaveCoroutine = null;
        }
    }

    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            SaveSystem.Instance.Save();
        }
    }
}
