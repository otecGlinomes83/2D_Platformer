using R3;
using UnityEngine;

[RequireComponent(typeof(Pusher))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _itemPrefab;

    [SerializeField] private float _spawnRate = 5f;
    [SerializeField] private ItemCollector _itemCollector;

    private Pusher _pusher;
    private Item _item;

    private CompositeDisposable _disposables = new CompositeDisposable();

    private void Awake()
    {
        _pusher = GetComponent<Pusher>();
    }

    private void Start()
    {
        _itemCollector.ItemCollected
    .Where(item => item == _item)
    .Subscribe(item =>
    {
        item.gameObject.SetActive(false);
        Invoke(nameof(SpawnItem), _spawnRate);
    })
    .AddTo(_disposables);

        SpawnItem();
    }

    private void OnDisable()
    {
        _disposables.Dispose();
    }

    private void SpawnItem()
    {
        if (_item == null)
        {
            _item = Instantiate(_itemPrefab, transform);
        }

        _item.gameObject.SetActive(true);
        _item.transform.position = transform.position;
        _pusher.Push(_item.Rigidbody);
    }
}
