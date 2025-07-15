using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 6f;
    [SerializeField] private float _acceleration = 0.05f;

    public event Action<Vector2> DirectionChanged;

    private float _currentSpeed = 0f;
    private float _directionArrivalThreshold = 0.01f;

    public bool IsRunning { get; private set; }

    public void Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < _directionArrivalThreshold)
        {
            IsRunning = false;
            _currentSpeed = 0f;

            return;
        }
        else
        {
            IsRunning = true;
            DirectionChanged?.Invoke(direction);

            _currentSpeed = Mathf.MoveTowards(_currentSpeed, _maxSpeed, _acceleration);

            transform.Translate(direction * _currentSpeed * Time.deltaTime, Space.World);
        }
    }
}