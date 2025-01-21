using UnityEngine;

public class PigMovements : MonoBehaviour
{
    public Animator animator;
    public string[] animationNames;
    [Range(0, 10)] public int animationIndex = 0;
    public float transitionDuration = 0.25f;

    private int currentAnimationIndex = -1;
    private float animationTimer = 0f;
    private float animationLength = 0f;

    void Start()
    {
        PlayAnimation(animationIndex);
    }

    void Update()
    {
        if (animationIndex != currentAnimationIndex)
        {
            PlayAnimation(animationIndex);
        }

        if (currentAnimationIndex != 0) // Si ce n'est pas l'animation par défaut
        {
            HandleAnimationTimer();
        }
    }

    private void PlayAnimation(int index)
    {
        if (animator != null && animationNames != null && index >= 0 && index < animationNames.Length)
        {
            animator.CrossFade(animationNames[index], transitionDuration);
            currentAnimationIndex = index;

            // Récupérer la durée de l'animation
            animationLength = GetAnimationLength(animationNames[index]);
            animationTimer = animationLength;
        }
    }

    private void HandleAnimationTimer()
    {
        animationTimer -= Time.deltaTime;
        if (animationTimer <= 0f)
        {
            PlayAnimation(0); // Revenir à l'animation d'index 0
        }
    }

    private float GetAnimationLength(string animationName)
    {
        if (animator != null)
        {
            RuntimeAnimatorController controller = animator.runtimeAnimatorController;
            foreach (AnimationClip clip in controller.animationClips)
            {
                if (clip.name == animationName)
                {
                    return clip.length;
                }
            }
        }
        return 0f;
    }
}
