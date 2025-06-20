using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;      // Igrač kojeg kamera prati
    [SerializeField] private Vector3 offset;        // Pomak kamere u odnosu na igrača
    [SerializeField] private float smoothSpeed = 0.125f; // Brzina praćenja

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
