using System;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;

    [SerializeField] private DamageAbler _damageAbler;

    public event Action Respawned;

    private void OnEnable()
    {
        _damageAbler.Dead += Respawn;
    }

    private void OnDisable()
    {
        _damageAbler.Dead -= Respawn;
    }

    public void Respawn()
    {
        _damageAbler.gameObject.transform.position = _spawnPosition.transform.position;
        Respawned?.Invoke();
    }
}
