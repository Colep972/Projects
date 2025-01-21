using UnityEngine;

public class RandomSurfaceMovement : MonoBehaviour
{
    [Header("Surface Settings")]
    public Vector3 surfaceCenter = Vector3.zero;
    public Vector2 surfaceSize = new Vector2(10f, 10f);

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 2f;
    public float pauseTime = 2f;
    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        SetRandomTargetPosition();
    }

    private void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-surfaceSize.x / 2f, surfaceSize.x / 2f);
        float randomZ = Random.Range(-surfaceSize.y / 2f, surfaceSize.y / 2f);
        targetPosition = new Vector3(surfaceCenter.x + randomX, surfaceCenter.y, surfaceCenter.z + randomZ);
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        isMoving = true;

        // Tourner vers la cible
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        float rotationTime = 0f;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            rotationTime += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Mathf.Clamp01(rotationTime));
            yield return null;
        }

        // Déplacement vers la cible avec lissage
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float elapsedTime = 0f;
        float duration = distance / moveSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);
            float smoothedProgress = movementCurve.Evaluate(progress);
            transform.position = Vector3.Lerp(startPosition, targetPosition, smoothedProgress);
            yield return null;
        }

        transform.position = targetPosition;

        // Pause à la position cible
        yield return new WaitForSeconds(pauseTime);

        // Choisir une nouvelle position aléatoire
        SetRandomTargetPosition();
        isMoving = false;
    }

    private void OnDrawGizmos()
    {
        // Dessine la surface dans l'éditeur Unity
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(surfaceCenter, new Vector3(surfaceSize.x, 0.1f, surfaceSize.y));

        // Dessine la position cible actuelle
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 0.2f);
    }
}
