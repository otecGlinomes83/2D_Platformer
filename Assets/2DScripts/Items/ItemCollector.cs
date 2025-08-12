using R3;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemCollector : MonoBehaviour
{
    [SerializeField] private float _checkRadius = 1;

    private CircleCollider2D _collectZone;

    private Subject<Item> _itemCollected = new Subject<Item>();

    public Observable<Item> ItemCollected => _itemCollected;

    private void Awake()
    {
        _collectZone = GetComponent<CircleCollider2D>();

        _collectZone.isTrigger = true;
        _collectZone.radius = _checkRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Item item))
        {
            _itemCollected.OnNext(item);
        }
    }
}