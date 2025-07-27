using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageAblerDetector : MonoBehaviour
{
    public event Action<DamageAbler> TargetDetected;
    public event Action TargetLost;

    private BoxCollider2D _detectZone;

    private void Awake()
    {
        _detectZone = GetComponent<BoxCollider2D>();
        _detectZone.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageAbler target))
            TargetDetected?.Invoke(target);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageAbler target))
            TargetLost?.Invoke();
    }
}
