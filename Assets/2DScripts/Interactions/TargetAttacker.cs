using System.Collections;
using UnityEngine;

namespace Assets._2DScripts.Interactions
{
    [RequireComponent(typeof(Attacker))]
    public class TargetAttacker : MonoBehaviour
    {
        [SerializeField] private float _attackCooldown = 1f;

        private HealthDetector _healthDetector;
        private Health _targetHealth;

        private Attacker _attacker;
        private Coroutine _attackCoroutine;
        private WaitForSecondsRealtime _cooldown;

        private void Awake()
        {
            _healthDetector = GetComponent<HealthDetector>();
            _attacker = GetComponent<Attacker>();
            _cooldown = new WaitForSecondsRealtime(_attackCooldown);
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
            TryStopAttack();
        }

        private void TryStartAttack(Health target)
        {
            TryStopAttack();

            _targetHealth = target;
            _attackCoroutine = StartCoroutine(AttackPerCooldown());
        }

        private void TryStopAttack()
        {
            if (_attackCoroutine == null)
                return;

            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
            _targetHealth = null;
        }

        private IEnumerator AttackPerCooldown()
        {
            while (_targetHealth != null)
            {
                yield return _cooldown;

                if (_targetHealth != null)
                {
                    _attacker.Impact(_targetHealth);
                }
                else
                {
                    break;
                }
            }

            _attackCoroutine = null;
        }
    }
}