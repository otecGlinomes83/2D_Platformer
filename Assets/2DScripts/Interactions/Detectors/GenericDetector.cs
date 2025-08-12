using System;
using UnityEngine;

public class GenericDetector<T> : MonoBehaviour where T : Component
{
    public event Action<T> TargetDetected;
    public event Action TargetLost;

    [SerializeField] private Collider2D _detectZone;

    private void Awake()
    {
        _detectZone.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<T>(out var T))
            TargetDetected?.Invoke(T);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<T>(out var T))
            TargetLost?.Invoke();
    }
}
