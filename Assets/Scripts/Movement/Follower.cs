using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Mover))]
public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Mover _mover;
    private BoxCollider2D _unfollowZone;

    private bool _isAbleToMove = true;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _unfollowZone = GetComponent<BoxCollider2D>();
        _unfollowZone.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
            _isAbleToMove = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
            _isAbleToMove = true;
    }

    private void LateUpdate()
    {
        if (_isAbleToMove)
        {
            Vector2 direction = _target.transform.position - transform.position;
            _mover.Move(direction);
        }
    }
}
