using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] float _maxSpeed;
    [SerializeField] float _acceleration;

    private PlayerInput _playerInput;

    private float _currentSpeed;
    private float _moveInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void Update()
    {
        _moveInput = _playerInput.Player.Move.ReadValue<float>();

        float targetSpeed = _moveInput * _maxSpeed;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, _acceleration);

        transform.Translate(Vector2.right * _currentSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
