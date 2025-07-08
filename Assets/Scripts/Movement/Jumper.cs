using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundDetector))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 7f;

    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;

    private bool _canJump = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();
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
            _rigidbody.linearVelocity = Vector2.up * _jumpForce;
        }
    }

    private void SwitchJump(bool canJump)
    {
        _canJump = canJump;
    }
}