using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerDetector : MonoBehaviour
{
    public event Action TargetDetected;
    public event Action TargetLost;

    private CircleCollider2D _detectZone;

    private void Awake()
    {
        _detectZone = GetComponent<CircleCollider2D>();
        _detectZone.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            TargetDetected?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            TargetLost?.Invoke();
    }
}
