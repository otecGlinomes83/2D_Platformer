using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;

    [SerializeField] private float _jumpForce = 8f;

    private Rigidbody2D _rigidbody;

    private bool _canJump = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _groundDetector.GroundDetected += SwitchJump;
    }

    private void OnDisable()
    {
        _groundDetector.GroundDetected -= SwitchJump;
    }

    public void Jump()
    {
        if (_canJump)
        {
            _canJump = false;
            _rigidbody.linearVelocity = Vector2.up * _jumpForce;
        }
    }

    private void SwitchJump(bool canJump) =>
        _canJump = canJump;
}