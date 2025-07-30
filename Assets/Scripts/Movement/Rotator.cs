using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] private Mover _mover;

    private Quaternion _rightRotation = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion _leftRotation = Quaternion.Euler(0f, 180f, 0f);

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
            return;

        bool isShouldLookLeft = direction.x < -_directionArrivalThreshold;
        _model.rotation = isShouldLookLeft ? _leftRotation : _rightRotation;
    }
}