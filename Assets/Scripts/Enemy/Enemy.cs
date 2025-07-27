using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Patroller), typeof(PlayerChaser))]
[RequireComponent(typeof(DamageAbler), typeof(Respawner))]
[RequireComponent(typeof(Rotator))]
public class Enemy : MonoBehaviour
{
   [SerializeField] private PlayerDetector _playerDetector;

    private Patroller _patroller;
    private PlayerChaser _stalker;

    private DamageAbler _damageAbler;
    private Respawner _respawner;

    private void Awake()
    {
        _patroller = GetComponent<Patroller>();
        _stalker = GetComponent<PlayerChaser>();
        _damageAbler = GetComponent<DamageAbler>();
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

    private void StartChase(Player player)
    {
        _patroller.TryStopPatrolling();
        _stalker.TryStartChase(player);
    }

    private void StartPatrolling()
    {
        _stalker.TryStopChase();
        _patroller.TryStartPatrolling();
    }
}