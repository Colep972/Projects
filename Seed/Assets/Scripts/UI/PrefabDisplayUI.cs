using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDisplayUI : MonoBehaviour
{
    [Header("Prefab to Display")]
    [SerializeField] private GameObject prefab;

    [Header("UI Container (Panel)")]
    [SerializeField] private Transform panelContainer;

    [Header("Options")]
    [SerializeField] private bool clearBeforeDisplay = true;

    private GameObject currentInstance;

    private void Start()
    {
        DisplayPrefab();
    }

    public void DisplayPrefab()
    {
        if (panelContainer == null)
        {
            Debug.LogWarning("Panel container not assigned!");
            return;
        }

        if (prefab == null)
        {
            Debug.LogWarning("Prefab not assigned!");
            return;
        }

        // Clear previous content if needed
        if (clearBeforeDisplay)
        {
            foreach (Transform child in panelContainer)
            {
                Destroy(child.gameObject);
            }
        }

        // Instantiate the prefab inside the panel
        currentInstance = Instantiate(prefab, panelContainer);
        currentInstance.transform.localScale = Vector3.one; // ensure proper scaling
    }

    public void Clear()
    {
        if (currentInstance != null)
        {
            Destroy(currentInstance);
            currentInstance = null;
        }
    }
}
