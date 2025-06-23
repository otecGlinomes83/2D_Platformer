using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private Transform _feetPosition;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRadius;

    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput.Enable();
    }

    private void Update()
    {
        if (_playerInput.Player.Jump.triggered)
            TryJump();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void TryJump()
    {
        if (Physics2D.OverlapCircle(_feetPosition.position, _checkRadius).TryGetComponent<Platform>(out _))
        {
            _rigidbody.linearVelocity = Vector2.up * _jumpForce;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_feetPosition.position, _checkRadius);
    }
}