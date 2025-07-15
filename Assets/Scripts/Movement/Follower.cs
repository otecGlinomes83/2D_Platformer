using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Mover _mover;

    private BoxCollider2D _unfollowZone;

    private Coroutine _moveCoroutine;

    private void Awake()
    {
        _unfollowZone = GetComponent<BoxCollider2D>();

        _unfollowZone.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            TryStopMoveToPlayer();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            StartMoveToPlayer();
        }
    }

    private void StartMoveToPlayer()
    {
        if (gameObject.activeInHierarchy)
        {
            TryStopMoveToPlayer();
            _moveCoroutine = StartCoroutine(MoveToPlayer());
        }
    }

    private IEnumerator MoveToPlayer()
    {
        while (enabled)
        {
            Vector2 direction = _target.position - transform.position;

            _mover.Move(direction);

            yield return null;
        }
    }

    private void TryStopMoveToPlayer()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }
}
