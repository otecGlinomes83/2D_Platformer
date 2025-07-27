using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private ItemCollector _itemCollector;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private DamageAbler _damageAbler;
    [SerializeField] private Respawner _respawner;

    private PlayerInput _playerInput;

    private List<Coin> _coins = new List<Coin>();

    public bool IsAlive => _damageAbler.CurrentHealth > 0;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _respawner.Respawn();
    }

    private void OnEnable()
    {
        _itemCollector.ItemCollected += ItemSorter;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _itemCollector.ItemCollected -= ItemSorter;
        _playerInput.Disable();
    }

    private void Update()
    {
        if (_playerInput.Player.Jump.triggered)
            _jumper.Jump();

        Vector2 direction = new Vector2(_playerInput.Player.Move.ReadValue<float>(), 0);

        _mover.Move(direction);
    }

    private void ItemSorter(Item item)
    {
        if (item is Coin coin)
            AddCoin(coin);

        if (item is Aid aid)
            Heal(aid);
    }

    private void AddCoin(Coin collectedCoin)
    {
        _coins.Add(collectedCoin);
        Debug.Log($"Собрана монетка! Всего:{_coins.Count}");
    }

    private void Heal(Aid aid)
    {
        _damageAbler.Heal(aid.Health);
        Debug.Log($"Собрана Аптечка! текущее хп:{_damageAbler.CurrentHealth}");
    }
}