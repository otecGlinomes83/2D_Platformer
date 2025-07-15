using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CoinCollector : MonoBehaviour
{
    [SerializeField] private float _checkRadius = 1;

    private CircleCollider2D _collectZone;

    public event Action<Coin> CoinCollected;

    private void Awake()
    {
        _collectZone = GetComponent<CircleCollider2D>();

        _collectZone.isTrigger = true;
        _collectZone.radius = _checkRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
            CoinCollected?.Invoke(coin);
    }
}
