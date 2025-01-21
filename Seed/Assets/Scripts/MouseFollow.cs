using UnityEngine;

public class MouseRotationFollowWithOffset : MonoBehaviour
{
    public enum RotationAxis
    {
        X, // Rotation autour de l'axe X
        Y, // Rotation autour de l'axe Y
        Z  // Rotation autour de l'axe Z
    }

    public RotationAxis rotationAxis = RotationAxis.Z; // L'axe de rotation choisi dans l'inspecteur
    public Transform target; // L'objet à faire tourner pour suivre la souris
    public float rotationOffset = 0f; // Décalage de rotation ajouté à l'angle calculé

    void Update()
    {
        if (target != null)
        {
            Vector3 mousePosition = Input.mousePosition;

            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3 direction = mousePosition - target.position;

            direction.z = 0;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle += rotationOffset;

            switch (rotationAxis)
            {
                case RotationAxis.X:
                    target.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));
                    break;
                case RotationAxis.Y:
                    target.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
                    break;
                case RotationAxis.Z:
                    target.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    break;
            }
        }
    }
}
