using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private float _maxSpeed = 6f;
    [SerializeField] private float _acceleration = 0.05f;

    private SpriteRenderer _spriteRenderer;

    private float _currentSpeed = 0;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_currentSpeed > 0)
        {
            _animator.SetBool("IsRunning", true);
            return;
        }
        else if (_currentSpeed < 0)
        {
            _spriteRenderer.flipX = true;
            _animator.SetBool("IsRunning", true);
            return;
        }

        _spriteRenderer.flipX = false;
        _animator.SetBool("IsRunning", false);
    }

    public void Move(Vector2 direction)
    {
        float targetSpeed = direction.x * _maxSpeed;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, _acceleration);

        transform.Translate(Vector2.right * _currentSpeed * Time.deltaTime);
    }
}