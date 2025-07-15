using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private CoinCollector _coinsCollector;
    [SerializeField] private Rotator _rotator;

    private PlayerInput _playerInput;

    private List<Coin> _coins = new List<Coin>();

    private void Awake()
    {
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
            _jumper.Jump();

        Vector2 direction = new Vector2(_playerInput.Player.Move.ReadValue<float>(), 0);

        _mover.Move(direction);
    }

    private void AddCoin(Coin coin)
    {
        _coins.Add(coin);

        Debug.Log($"Собрана монетка! Всего:{_coins.Count}");
    }
}