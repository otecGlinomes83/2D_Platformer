using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Pusher))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _itemPrefab;

    [SerializeField] private float _spawnRate = 5f;

    private Pusher _pusher;
    private Item _item;

    private void Awake()
    {
        _pusher = GetComponent<Pusher>();
    }

    private void Start()
    {
        SpawnItem();
    }

    private void OnDisable()
    {
        _item.Collected -= DeactivateItem;
    }

    private void DeactivateItem(Item item)
    {
        _item.gameObject.SetActive(false);
        StartCoroutine(RespawnDelayed());
    }

    private IEnumerator RespawnDelayed()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(_spawnRate);

        yield return delay;

        SpawnItem();

        yield break;
    }

    private void SpawnItem()
    {
        if (_item == null)
        {
            _item = Instantiate(_itemPrefab, transform);
            _item.Collected += DeactivateItem;
        }

        _item.gameObject.SetActive(true);
        _item.transform.position = transform.position;
        _pusher.Push(_item.Rigidbody);
    }
}
