using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _model;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _turnSpeed = 720f; // degrees per second

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

        Vector3 moveDirection = _input.ToIso();
        Vector3 targetPosition = _rigidBody.position + moveDirection * _moveSpeed * Time.fixedDeltaTime;
        _rigidBody.MovePosition(targetPosition);
    }

    private void RotateModel()
    {
        if (_input == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }
}
