using System;
using UnityEngine;

public class CoinsCollector : MonoBehaviour
{
    [SerializeField] private float _checkRadius = 1;

    [SerializeField] private Transform _collectorPosition;

    public event Action<Coin> CoinCollected;

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_collectorPosition.position, _checkRadius);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].TryGetComponent(out Coin coin))
            {
                CoinCollected?.Invoke(coin);
                Destroy(coin.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (_collectorPosition != null)
            Gizmos.DrawWireSphere(_collectorPosition.position, _checkRadius);
    }
}
