using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    public event Action Dead;

    public Rigidbody2D Rigidbody { get; private set; }
    public bool IsAlive => CurrentHealth > 0;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        if (CurrentHealth < damage)
        {
            CurrentHealth = 0f;
        }
        else
        {
            CurrentHealth -= damage;
        }

        if (IsAlive == false)
        {
            CurrentHealth = _maxHealth;
            Dead?.Invoke();
        }
    }

    public void Heal(float health)
    {
        if (health + CurrentHealth > _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }
        else
        {
            CurrentHealth += health;
        }
    }
}