using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HealthDetector), typeof(Pusher))]
public class Attacker : MonoBehaviour
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _attackCooldown = 1f;

    private HealthDetector _healthDetector;
    private Pusher _targetPusher;

    private Health _target;

    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _healthDetector = GetComponent<HealthDetector>();
        _targetPusher = GetComponent<Pusher>();
    }

    private void OnEnable()
    {
        _healthDetector.TargetDetected += TryStartAttack;
        _healthDetector.TargetLost += TryStopAttack;
    }

    private void OnDisable()
    {
        _healthDetector.TargetDetected -= TryStartAttack;
        _healthDetector.TargetLost -= TryStopAttack;

    }

    private void TryStartAttack(Health target)
    {
        if (_attackCoroutine == null)
        {
            _target = target;
            _attackCoroutine = StartCoroutine(AttackPerCooldown());
        }
    }

    private void TryStopAttack()
    {
        if (_attackCoroutine != null)
        {
            _target = null;

            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    private IEnumerator AttackPerCooldown()
    {
        WaitForSecondsRealtime gettingReady = new WaitForSecondsRealtime(_attackCooldown);

        while (enabled)
        {
            yield return gettingReady;

            Attack();
        }
    }

    private void Attack()
    {
        _target.TakeDamage(_damage);
        _targetPusher.Push(_target.Rigidbody);
    }
}
