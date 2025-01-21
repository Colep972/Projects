using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraNavigation : MonoBehaviour, IPointerClickHandler
{
    [Header("Camera References")]
    public Camera targetCamera;

    [Header("Position References")]
    public Transform cameraPositionPoint;
    public Transform cameraLookAtPoint;

    [Header("Movement Settings")]
    [Range(0.5f, 5f)]
    public float transitionDuration = 1.5f;

    [Header("Easing Settings")]
    [SerializeField]
    private AnimationCurve movementCurve = new AnimationCurve(
        new Keyframe(0, 0, 0, 0),
        new Keyframe(1, 1, 2, 0)
    );

    [Header("Audio Settings")]
    [SerializeField] private AudioSource clickAudio;
    [SerializeField] private AudioScript audioScript;

    private bool isMoving = false;

    private void Start()
    {
        // Validate required components
        if (audioScript == null)
        {
            audioScript = FindFirstObjectByType<AudioScript>();
            if (audioScript == null)
                Debug.LogError("AudioScript not found in scene!");
        }

        if (clickAudio == null)
            Debug.LogError("ClickAudio not assigned on " + gameObject.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isMoving && audioScript != null && clickAudio != null)
        {
            audioScript.PlaySound(clickAudio);
            MoveCameraToPosition();
        }
    }

    private void MoveCameraToPosition()
    {
        if (targetCamera == null || cameraPositionPoint == null || cameraLookAtPoint == null)
        {
            Debug.LogError("Missing references on " + gameObject.name);
            return;
        }

        StartCoroutine(MoveCamera());
    }

    private System.Collections.IEnumerator MoveCamera()
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPosition = targetCamera.transform.position;
        Quaternion startRotation = targetCamera.transform.rotation;

        Vector3 targetDirection = (cameraLookAtPoint.position - cameraPositionPoint.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / transitionDuration;
            float curveValue = movementCurve.Evaluate(normalizedTime);

            targetCamera.transform.position = Vector3.Lerp(
                startPosition,
                cameraPositionPoint.position,
                curveValue
            );

            targetCamera.transform.rotation = Quaternion.Slerp(
                startRotation,
                targetRotation,
                curveValue
            );

            yield return null;
        }

        targetCamera.transform.position = cameraPositionPoint.position;
        targetCamera.transform.rotation = targetRotation;

        isMoving = false;
    }
}
