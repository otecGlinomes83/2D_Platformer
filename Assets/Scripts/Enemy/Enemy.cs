using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Patroller), typeof(PlayerChaser))]
[RequireComponent(typeof(Health), typeof(Respawner))]
[RequireComponent(typeof(Rotator))]
public class Enemy : MonoBehaviour
{
   [SerializeField] private PlayerDetector _playerDetector;

    private Patroller _patroller;
    private PlayerChaser _playerChaser;

    private Respawner _respawner;

    private void Awake()
    {
        _patroller = GetComponent<Patroller>();
        _playerChaser = GetComponent<PlayerChaser>();
        _respawner = GetComponent<Respawner>();

        _respawner.Respawn();
        _patroller.TryStartPatrolling();
    }

    private void OnEnable()
    {
        _playerDetector.TargetDetected += StartChase;
        _playerDetector.TargetLost += StartPatrolling;
    }

    private void OnDisable()
    {
        _playerDetector.TargetDetected -= StartChase;
        _playerDetector.TargetLost -= StartPatrolling;
    }

    private void StartChase()
    {
        _patroller.TryStopPatrolling();
        _playerChaser.TryStartChase();
    }

    private void StartPatrolling()
    {
        _playerChaser.TryStopChase();
        _patroller.TryStartPatrolling();
    }
}