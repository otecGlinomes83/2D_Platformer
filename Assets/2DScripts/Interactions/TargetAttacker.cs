using System.Collections;
using UnityEngine;

namespace Assets._2DScripts.Interactions
{
    [RequireComponent(typeof(Attacker))]
    public class TargetAttacker : MonoBehaviour
    {
        [SerializeField] private float _attackCooldown = 1f;

        private HealthDetector _healthDetector;

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

        private void TryStartAttack(Health targetHealth)
        {
            TryStopAttack();
            _attackCoroutine = StartCoroutine(AttackPerCooldown(targetHealth));
        }

        private void TryStopAttack()
        {
            if (_attackCoroutine == null)
                return;

            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
            _cooldown.Reset();
        }

        private IEnumerator AttackPerCooldown(Health targetHealth)
        {
            while (enabled)
            {
                yield return _cooldown;

                if (targetHealth != null)
                {
                    _attacker.Impact(targetHealth);
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