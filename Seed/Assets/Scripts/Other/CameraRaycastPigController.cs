using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class CameraRaycastPigController : MonoBehaviour
{
    public string animationTriggerName = "PlayPigAnimation";
    public AudioSource pigAudioSource;
    public PotManager _PotManager;
    public LayerMask pigLayer;
    public float raycastDistance = 100f;
    public List<string> randomMessages;

    private Animator pigAnimator;
    private Transform currentTarget = null;

    private PnjTextDisplay pnjTextDisplay;
    private bool tutorialMessageShown = false;

    private void Start()
    {
        pnjTextDisplay = Object.FindFirstObjectByType<PnjTextDisplay>();
        if (pnjTextDisplay == null)
        {
            Debug.LogError("PnjTextDisplay component not found in the scene.");
        }
        
    }

    private void Update()
    {
        PerformRaycast();
        if (_PotManager != null)
        {
            if (!_PotManager.isFirstPotGotten)
            {
                if (!tutorialMessageShown)
                {
                    DisplayTutorialMessage();
                }
            }
            else
            {
                tutorialMessageShown = true;
                if (tutorialMessageShown)
                {
                    if (pnjTextDisplay != null)
                    {
                        pnjTextDisplay.HideConsoleBox(); // Hide the tutorial message
                    }
                    tutorialMessageShown = false;
                }
            }
        }
    }

    private void PerformRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, pigLayer))
        {
            if (currentTarget != hit.transform)
            {
                currentTarget = hit.transform;
                pigAnimator = hit.collider.GetComponent<Animator>();

                if (pigAnimator != null)
                {
                    pigAnimator.SetTrigger(animationTriggerName);
                }

                if (pigAudioSource != null)
                {
                    pigAudioSource.Play();
                }
                if (_PotManager.isFirstPotGotten)
                    DisplayRandomMessage();
            }
        }
        else
        {
            currentTarget = null;
        }
    }

    private void DisplayRandomMessage()
    {
        if (randomMessages != null && randomMessages.Count > 0)
        {
            string message = randomMessages[Random.Range(0, randomMessages.Count)];
            if (pnjTextDisplay != null)
            {
                pnjTextDisplay.DisplayMessagePublic(message);
            }
            else
            {
                Debug.Log(message);
            }
        }
    }

    private void DisplayTutorialMessage()
    {
        if (pnjTextDisplay != null)
        {
            pnjTextDisplay.DisplayMessagePublic("Get a pot by going in the shop, plant a seed, and grow it to 100 %.");
        }
    }
}
