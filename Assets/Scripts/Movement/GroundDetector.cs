using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private Transform _feetPosition;

    [SerializeField] private float _checkRadius = 0.05f;

    public event Action<bool> GroundDetected;

    private void FixedUpdate()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_feetPosition.position, _checkRadius, _groundLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].TryGetComponent<Collider2D>(out _))
            {
                GroundDetected?.Invoke(true);
                return;
            }
        }

        GroundDetected?.Invoke(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (_feetPosition != null)
            Gizmos.DrawWireSphere(_feetPosition.position, _checkRadius);
    }
}
