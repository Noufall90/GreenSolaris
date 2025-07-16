using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _model;
    [SerializeField] private Transform _camera;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _turnSpeed = 720f;

    private Vector3 _input;

    private void Update()
    {
        GatherInput();
        RotateModel();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GatherInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _input = new Vector3(horizontal, 0, vertical).normalized;
    }

    private void MovePlayer()
    {
        if (_input == Vector3.zero) return;

        // Get camera directions
        Vector3 camForward = _camera.forward;
        Vector3 camRight = _camera.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * _input.z + camRight * _input.x;
        Vector3 targetPosition = _rigidBody.position + moveDirection * _moveSpeed * Time.fixedDeltaTime;
        _rigidBody.MovePosition(targetPosition);
    }

    private void RotateModel()
    {
        if (_input == Vector3.zero) return;

        Vector3 camForward = _camera.forward;
        Vector3 camRight = _camera.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * _input.z + camRight * _input.x;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }
}
