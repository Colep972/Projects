using UnityEngine;
using System;
using System.Collections;

public enum TutorialStep
{
    None = 0,
    GetFirstPot = 1,
    SelectFirstSeed = 2,
    PlantFirstSeed = 3,
    HarvestFirstPlant = 4,
    Completed = 5
}


public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    public TutorialStep currentStep;
    public bool tutorialCompleted;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (!tutorialCompleted && currentStep == TutorialStep.None && !GameState.shouldLoadGame)
        {
            currentStep = TutorialStep.None;
            tutorialCompleted = false;
            ShowCurrentStepMessage();
        }
        else
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("You can continue");
        }
    }

    public void AdvanceStep()
    {
        if (tutorialCompleted) return;

        currentStep++;
        if (currentStep >= TutorialStep.Completed)
        {
            tutorialCompleted = true;
            PnjTextDisplay.Instance.HidePersistentMessage();
            SaveSystem.Instance.Save();
            PnjTextDisplay.Instance.DisplayMessagePublic("Tutorial completed! Well done!");
            return;
        }

        ShowCurrentStepMessage();
        SaveSystem.Instance.Save();
    }

    public void ShowCurrentStepMessage()
    {
        switch (currentStep)
        {
            case TutorialStep.None:
                PnjTextDisplay.Instance.StartCoroutine(ShowWhenReady("Welcome! Let's start by buying your first pot. In the shopcart you can find it"));
                break;
            case TutorialStep.GetFirstPot:
                PnjTextDisplay.Instance.StartCoroutine(ShowWhenReady("Great! Now select a seed from your inventory. It's the red bag after click on seed."));
                break;
            case TutorialStep.SelectFirstSeed:
                PnjTextDisplay.Instance.StartCoroutine(ShowWhenReady("Nice choice! Now plant your seed. Click on the plant button"));
                break;
            case TutorialStep.PlantFirstSeed:
                PnjTextDisplay.Instance.StartCoroutine(ShowWhenReady("Good! Wait until your plant is ready and harvest it."));
                break;
            case TutorialStep.HarvestFirstPlant:
                PnjTextDisplay.Instance.StartCoroutine(ShowWhenReady("Nice! Now sell your first plant. In the shop, you can choose the price and the amount"));
                break;
            case TutorialStep.Completed:
                PnjTextDisplay.Instance.StartCoroutine(ShowWhenReady("Awesome! You’ve completed the tutorial, you can now start to find the rarest seed !"));
                break;
        }
    }

    private IEnumerator ShowWhenReady(string message)
    {
        while (PnjTextDisplay.Instance == null)
            yield return null;

        PnjTextDisplay.Instance.DisplayPersistentMessage(message);
    }

}
