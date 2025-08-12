using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] private PushMode _pushMode;

    [Header("Position Based Settings")]
    [SerializeField, Min(1f)] private float _pushForce = 3f;

    [Header("Randomized Settings")]
    [SerializeField] private float _upwardForce = 1f;
    [SerializeField] private float _maxHorizontalForce = 1f;
    [SerializeField] private float _minHorizontalForce = -1f;

    public void Push(Rigidbody2D targetRigidbody)
    {
        Vector2 forceDirection = _pushMode switch
        {
            PushMode.PositionBased => GetPositionBasedForceDirection(targetRigidbody.transform.position),
            PushMode.Randomized => GetRandomForceDirection(),
            _ => forceDirection = Vector2.zero
        };

        targetRigidbody.AddForce(forceDirection * _pushForce, ForceMode2D.Impulse);
    }

    private Vector2 GetPositionBasedForceDirection(Vector3 targetPosition) =>
        (targetPosition - transform.position).normalized;

    private Vector2 GetRandomForceDirection() =>
        new Vector2(Random.Range(_minHorizontalForce, _maxHorizontalForce), _upwardForce);
}
