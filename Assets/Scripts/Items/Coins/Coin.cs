using UnityEngine;

public class Coin : Item
{
    [SerializeField] private int _maxCount = 5;
    [SerializeField] private int _minCount = 25;

    private int _count = 0;

    protected override void Awake()
    {
        base.Awake();
        _count = Random.Range(_minCount, _maxCount + 1);
    }

    public override void Use(Player player) =>
        player.Wallet.AddCoins(_count);
}