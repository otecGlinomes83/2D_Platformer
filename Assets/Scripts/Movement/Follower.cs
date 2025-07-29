using UnityEngine;

[RequireComponent(typeof(PlayerDetector), typeof(Mover))]
public class Follower : MonoBehaviour
{
    [SerializeField] private Player _player;

    private PlayerDetector _playerDetector;
    private Mover _mover;

    private bool _isAbleToMove = true;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    private void OnEnable()
    {
        _playerDetector.TargetDetected += StopMoveToPlayer;
        _playerDetector.TargetLost += StartMoveToPlayer;
    }

    private void OnDisable()
    {
        _playerDetector.TargetDetected -= StopMoveToPlayer;
        _playerDetector.TargetLost -= StartMoveToPlayer;
    }

    private void StopMoveToPlayer() =>
        _isAbleToMove = false;

    private void StartMoveToPlayer() =>
        _isAbleToMove = true;

    private void LateUpdate()
    {
        if (_isAbleToMove)
            _mover.Move(GetDirection());
    }

    private Vector2 GetDirection() =>
            _player.transform.position - transform.position;
}
