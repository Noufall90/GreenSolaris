using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private Vector3 offset = new Vector3(-15, 12, -15);
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;

    private float currentAngle = 0f;

    private void Update()
    {
        HandleRotationInput();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Hitung rotasi offset berdasarkan currentAngle
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 rotatedOffset = rotation * offset;

        Vector3 desiredPosition = target.position + rotatedOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Kamera selalu menghadap target
        transform.LookAt(target.position);
    }

    private void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.Q))
            currentAngle -= rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
            currentAngle += rotationSpeed * Time.deltaTime;
    }
}
