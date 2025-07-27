using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DamageAblerDetector), typeof(TargetPusher))]
public class Attacker : MonoBehaviour
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _attackCooldown = 1f;

    private DamageAblerDetector _targetDetector;
    private TargetPusher _targetPusher;

    private DamageAbler _target;

    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _targetDetector = GetComponent<DamageAblerDetector>();
        _targetPusher = GetComponent<TargetPusher>();
    }

    private void OnEnable()
    {
        _targetDetector.TargetDetected += TryStartAttack;
        _targetDetector.TargetLost += TryStopAttack;
    }

    private void OnDisable()
    {
        _targetDetector.TargetDetected -= TryStartAttack;
        _targetDetector.TargetLost -= TryStopAttack;

    }

    private void TryStartAttack(DamageAbler target)
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
        _targetPusher.PushTarget(_target.Rigidbody);
    }
}
