using System;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;

    [SerializeField] private Health _health;

    public event Action Respawned;

    private void OnEnable()
    {
        _health.Dead += Respawn;
    }

    private void OnDisable()
    {
        _health.Dead -= Respawn;
    }

    public void Respawn()
    {
        _health.gameObject.transform.position = _spawnPosition.transform.position;
        Respawned?.Invoke();
    }
}
