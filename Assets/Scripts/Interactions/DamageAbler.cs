using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DamageAbler : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Respawner _respawner;

    public event Action Dead;

    public Rigidbody2D Rigidbody { get; private set; }
    public bool IsAlive => CurrentHealth > 0;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _respawner.Respawned += SetHealthDefault;
    }

    private void OnDisable()
    {
        _respawner.Respawned -= SetHealthDefault;
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

        Debug.Log($"{gameObject.name} - Получен урон! Текущее здоровье - '{CurrentHealth}'");

        if (IsAlive == false)
        {
            Dead?.Invoke();
            Debug.Log($"{gameObject.name} - умер!");
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

        Debug.Log($"Получено лечение! Текущее здоровье:{CurrentHealth}");
    }

    private void SetHealthDefault() =>
        CurrentHealth = _maxHealth;
}