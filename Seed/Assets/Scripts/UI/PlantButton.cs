using UnityEngine;
using UnityEngine.UI;

public class PlantButton : MonoBehaviour
{
    public Button button;
    public Transform potsParent;
    [Header("Audio Settings")]
    [SerializeField] public AudioSource clickSound;
    [SerializeField] private AudioScript audioScript;

    [Header("Animation Settings")]
    public Animator pigAnimator;
    public string growPigAnimationName = "GrowPig";
    public string pigManFXAnimationName = "PigManFX";
    public float animationDuration = 2f;

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        PlayClickSound();
        PlayGrowPigAnimation();

        for (int i = 1; i <= 4; i++)
        {
            Transform potSlot = potsParent.Find($"Pot_Slot_{i}");

            if (potSlot != null)
            {
                GrowthCycle growthCycle = potSlot.GetComponentInChildren<GrowthCycle>();
                if (growthCycle != null)
                {
                    growthCycle.Plant();
                    if (growthCycle.getCurrentSeed() == null)
                    {
                        Debug.LogError("seed NULL !!!!");
                    }
                }
            }
        }
    }

    private void PlayGrowPigAnimation()
    {
        if (pigAnimator != null)
        {
            pigAnimator.Play(growPigAnimationName);
            Invoke(nameof(PlayPigManFXAnimation), animationDuration);
        }
    }

    private void PlayPigManFXAnimation()
    {
        if (pigAnimator != null)
        {
            pigAnimator.Play(pigManFXAnimationName);
        }
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            clickSound.pitch = Random.Range(0.8f, 1.2f);
            if (audioScript != null)
            {
                audioScript.PlaySound(clickSound);
            }
            else
            {
                //clickSound.Play();
            }
        }
    }
}
