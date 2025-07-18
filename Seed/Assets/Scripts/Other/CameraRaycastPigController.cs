using System.Collections;
using System.Collections.Generic;
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

    public bool tutorialMessageShown;

    private float messageCooldown = 1.5f;
    private float lastMessageTime = -10f;

    public bool hasShownTutorialOnce = false;

    private void Start()
    {
        tutorialMessageShown = false;
    }

    private void Update()
    {
        PerformRaycast();

        if (_PotManager != null)
        {
            if (!_PotManager.isFirstPotGotten)
            {
                if (!hasShownTutorialOnce && !tutorialMessageShown)
                {
                    Debug.Log("Affichage du message tutoriel");
                    DisplayTutorialMessage();
                    tutorialMessageShown = true;
                    hasShownTutorialOnce = true; // important
                }
                   
                if (SaveSystem.Instance.firstPotAgain)
                {
                    Debug.Log("Save System");
                    _PotManager.isFirstPotGotten = true;
                }
            }
            else
            {
                if (tutorialMessageShown)
                {
                    Debug.Log("[COCHON] Pot obtenu, on masque le message de tutoriel.");
                    tutorialMessageShown = false;
                    PnjTextDisplay.Instance.HideConsoleBox();
                    PnjTextDisplay.Instance.isPersistentMessageActive = false;
                }
            }
        }
    }

    private void PerformRaycast()
    {
        Vector3 inputPosition;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            inputPosition = Input.GetTouch(0).position;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            inputPosition = Input.mousePosition;
        }
        else
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, pigLayer))
        {
            currentTarget = hit.transform;
            pigAnimator = hit.collider.GetComponent<Animator>();

            if (pigAnimator != null)
                pigAnimator.SetTrigger(animationTriggerName);

            if (pigAudioSource != null)
                pigAudioSource.Play();

            if (_PotManager != null && _PotManager.isFirstPotGotten)
                DisplayRandomMessage();
        }
    }

    private void DisplayRandomMessage()
    {
        Debug.Log("[COCHON] Tentative d’affichage d’un message aléatoire");

        if (Time.time - lastMessageTime < messageCooldown) return;

        if (randomMessages != null && randomMessages.Count > 0)
        {
            string message = randomMessages[Random.Range(0, randomMessages.Count)];
            PnjTextDisplay.Instance.DisplayMessagePublic(message);
            lastMessageTime = Time.time;
        }
    }

    private void DisplayTutorialMessage()
    {
        PnjTextDisplay.Instance.DisplayPersistentMessage("Get a pot by going in the shop, plant a seed, and grow it to 100 %.");
    }
}
