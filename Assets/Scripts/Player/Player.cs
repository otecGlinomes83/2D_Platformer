using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CoinsCollector))]
public class Player : MobileEntity
{
    private CoinsCollector _coinsCollector;
    private PlayerInput _playerInput;

    private List<Coin> _coins = new List<Coin>();

    protected override void Awake()
    {
        base.Awake();

        _coinsCollector = GetComponent<CoinsCollector>();
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _coinsCollector.CoinCollected += AddCoin;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _coinsCollector.CoinCollected -= AddCoin;
        _playerInput.Disable();
    }

    private void Update()
    {
        if (_playerInput.Player.Jump.triggered)
            Jumper.Jump();


        Vector2 direction = new Vector2(_playerInput.Player.Move.ReadValue<float>(), 0);

        Mover.Move(direction);
    }

    private void AddCoin(Coin coin)
    {
        _coins.Add(coin);
        Debug.Log($"Собрана монетка! Всего:{_coins.Count}");
    }
}