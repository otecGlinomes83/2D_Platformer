using R3;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    public event Action Dead;

    public ReadOnlyReactiveProperty<bool> IsAlive;
    public ReadOnlyReactiveProperty<float> PublicCurrentHealth => CurrentHealth;
    public ReadOnlyReactiveProperty<float> PublicMaxHealth => MaxHealth;

    private ReactiveProperty<float> CurrentHealth;
    private ReactiveProperty<float> MaxHealth;

    private void Awake()
    {
        MaxHealth = new ReactiveProperty<float>(_maxHealth);
        CurrentHealth = new ReactiveProperty<float>(_maxHealth);

        IsAlive = CurrentHealth.Select(health => health > 0).ToReadOnlyReactiveProperty();
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        if (CurrentHealth.Value < damage)
        {
            CurrentHealth.Value = 0f;
        }
        else
        {
            CurrentHealth.Value -= damage;
        }

        if (IsAlive.CurrentValue == false)
        {
            Dead?.Invoke();
            CurrentHealth.Value = _maxHealth;
        }
    }

    public void Heal(float health)
    {
        if (health + CurrentHealth.Value > _maxHealth)
        {
            CurrentHealth.Value = _maxHealth;
        }
        else
        {
            CurrentHealth.Value += health;
        }
    }
}