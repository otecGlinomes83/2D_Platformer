using UnityEngine;

public class Aid : Item
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;

    public int Health { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Health = Random.Range(_minHealth, _maxHealth);
    }
}
