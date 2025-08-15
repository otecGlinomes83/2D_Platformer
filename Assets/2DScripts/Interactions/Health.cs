using System;
using UnityEngine;

public class Health : MonoBehaviour, IChangeObservable
{
    [SerializeField] private float _maxHealth = 100f;

    public event Action<float, float> ValueChanged;
    public event Action Dead;

    private float _currentHealth;

    public bool IsAlive => _currentHealth > 0f;

    private void Start()
    {
        _currentHealth = _maxHealth;
        ValueChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Max(_currentHealth - Mathf.Max(damage, 0f), 0f);

        if (IsAlive == false)
        {
            Dead?.Invoke();
            _currentHealth = _maxHealth;
        }

        ValueChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void Heal(float health)
    {
        _currentHealth = Mathf.Min(_maxHealth, health + _currentHealth);
        ValueChanged?.Invoke(_currentHealth, _maxHealth);
    }
}