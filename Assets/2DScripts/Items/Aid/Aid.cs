using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Aid : Item
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;

    private int _heal;

    protected override void Awake()
    {
        base.Awake();
        _heal = Random.Range(_minHealth, _maxHealth);
    }

    public override void Use(Player player) =>
        player.Health.Heal(_heal);
}
