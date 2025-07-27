using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ItemPusher))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _itemPrefab;

    [SerializeField] private float _spawnRate = 5f;
    [SerializeField] private ItemCollector _itemCollector;

    private ItemPusher _itemPusher;

    private Coroutine _spawnCoroutine;
    private WaitForSecondsRealtime _spawnDelay;

    private Item _item;

    private void Awake()
    {
        _itemPusher = GetComponent<ItemPusher>();
        _spawnDelay = new WaitForSecondsRealtime(_spawnRate);
    }

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(RespawnDelayed());
    }

    private void OnEnable()
    {
        _itemCollector.ItemCollected += DeactivateItem;
    }

    private void OnDisable()
    {
        _itemCollector.ItemCollected -= DeactivateItem;
        StopSpawning();
    }

    private void StartRespawnDelayed()
    {
        StopSpawning();
        _spawnCoroutine = StartCoroutine(RespawnDelayed());
    }

    private void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator RespawnDelayed()
    {
        yield return _spawnDelay;

        ActivateItem();

        yield break;
    }

    private void ActivateItem()
    {
        if (_item == null)
        {
            _item = Instantiate(_itemPrefab, transform);
        }

        _item.gameObject.SetActive(true);
        _item.transform.position = transform.position;
        _itemPusher.Push(_item);
    }

    private void DeactivateItem(Item item)
    {
        if (item == _item)
        {
            _item.gameObject.SetActive(false);
            StartRespawnDelayed();
        }
    }
}
