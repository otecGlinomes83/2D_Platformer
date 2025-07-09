using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 6f;
    [SerializeField] private float _acceleration = 0.1f;

    private float _directionArrivalThreshold = 0.01f;
    private float _currentSpeed = 0;

    private float _startScaleX;

    private bool _isLookingLeft = false;

    public bool IsRunning { get; private set; }

    private void Awake()
    {
        _startScaleX = transform.localScale.x;
    }

    private void Update()
    {
        if (_currentSpeed != 0)
        {
            IsRunning = true;
            return;
        }

        IsRunning = false;
    }

    public void Move(Vector2 direction)
    {
        float targetSpeed = direction.x * _maxSpeed;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, _acceleration);

        transform.Translate(Vector2.right * _currentSpeed * Time.deltaTime);

        if (direction.x < -_directionArrivalThreshold)
        {
            if (_isLookingLeft == false)
            {
                transform.localScale = new Vector2(-_startScaleX, transform.localScale.y);
                _isLookingLeft = true;
            }
        }
        else if (direction.x > _directionArrivalThreshold)
        {
            transform.localScale = new Vector2(_startScaleX, transform.localScale.y);
            _isLookingLeft = false;
        }
    }
}