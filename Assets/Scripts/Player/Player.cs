using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Respawner), typeof(Rotator))]
[RequireComponent(typeof(Mover), typeof(Jumper))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
   [SerializeField] private ItemCollector _itemCollector;

    private Mover _mover;
    private Jumper _jumper;
    private Respawner _respawner;

    private PlayerInput _playerInput;

    public Health Health { get; private set; }
    public Wallet Wallet { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _respawner = GetComponent<Respawner>();
        Health = GetComponent<Health>();

        _playerInput = new PlayerInput();
        Wallet = new Wallet();

        _respawner.Respawn();
    }

    private void OnEnable()
    {
        _itemCollector.ItemCollected += UseItem;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _itemCollector.ItemCollected -= UseItem;
        _playerInput.Disable();
    }

    private void Update()
    {
        if (_playerInput.Player.Jump.triggered)
            _jumper.Jump();

        Vector2 direction = new Vector2(_playerInput.Player.Move.ReadValue<float>(), 0);

        _mover.Move(direction);
    }

    private void UseItem(Item item) =>
        item.Use(this);
}