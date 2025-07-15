using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] private Mover _mover;

    private bool _isLookingLeft = false;

    private float _directionArrivalThreshold = 0.01f;

    private void OnEnable()
    {
        _mover.DirectionChanged += Rotate;
    }

    private void OnDisable()
    {
        _mover.DirectionChanged -= Rotate;
    }

    private void Rotate(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < _directionArrivalThreshold)
        {
            RotateRight();
            _isLookingLeft = false;
            return;
        }

        if (direction.x > _directionArrivalThreshold)
        {
            RotateRight();
            _isLookingLeft = false;
        }
        else
        {
            if (_isLookingLeft)
                return;

            RotateLeft();
            _isLookingLeft = true;
        }
    }

    private void RotateRight() =>
        _model.rotation = Quaternion.Euler(0f, 0f, 0f);

    private void RotateLeft() =>
        _model.rotation = Quaternion.Euler(0f, 180f, 0f);
}