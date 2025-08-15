using Assets._2DScripts.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Attacker), typeof(Healer))]
public class VampirismAbility : MonoBehaviour, IChangeObservable
{
    [SerializeField] private Health _playerHealth;

    [SerializeField] private Vector2 _range = new Vector2(10f, 5f);

    [SerializeField] private int _duration = 6;
    [SerializeField] private int _reloadTime = 4;

    [SerializeField] private float _attackRate = 0.5f;

    public event Action<bool> UsingAbility;
    public event Action<float, float> ValueChanged;

    private Attacker _attacker;
    private Healer _healer;

    private WaitForSecondsRealtime _stepInterval = new WaitForSecondsRealtime(1f);

    private Coroutine _abilityCoroutine;
    private Coroutine _workCoroutine;

    private bool _isReloading = false;

    private float _remainingTime = 0f;

    public Vector2 AbilityRange => _range;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _healer = GetComponent<Healer>();
    }

    private void Start()
    {
        _remainingTime = _duration;
        ValueChanged?.Invoke(_remainingTime, _duration);
    }

    public void TryStartWork()
    {
        if (_workCoroutine != null || _isReloading)
            return;

        _workCoroutine = StartCoroutine(Work());
    }

    private IEnumerator UseAbilityPerCooldown()
    {
        WaitForSecondsRealtime attackCooldown = new WaitForSecondsRealtime(_attackRate);

        while (enabled)
        {
            PayUpHealth();
            yield return attackCooldown;
        }
    }

    private IEnumerator Work()
    {
        _remainingTime = _duration;

        TryStartVampirismAbility();

        while (_remainingTime > 0)
        {
            _remainingTime--;
            ValueChanged?.Invoke(_remainingTime, _duration);

            yield return _stepInterval;
        }

        TryStopVampirismAbility();

        StartCoroutine(Reload());

        _workCoroutine = null;
    }

    private IEnumerator Reload()
    {
        _isReloading = true;

        _remainingTime = 0f;

        while (_remainingTime < _reloadTime)
        {
            _remainingTime++;
            ValueChanged?.Invoke(_remainingTime, _reloadTime);

            yield return _stepInterval;
        }

        _remainingTime = _duration;
        ValueChanged?.Invoke(_remainingTime, _duration);
        _isReloading = false;

        yield break;
    }

    private void TryStartVampirismAbility()
    {
        if (_abilityCoroutine != null)
            return;

        UsingAbility?.Invoke(true);
        _abilityCoroutine = StartCoroutine(UseAbilityPerCooldown());
    }

    private void TryStopVampirismAbility()
    {
        if (_abilityCoroutine == null)
            return;

        UsingAbility?.Invoke(false);
        StopCoroutine(_abilityCoroutine);
        _abilityCoroutine = null;
    }

    private void PayUpHealth()
    {
        foreach (Health enemyHealth in GetEnemiesHealth())
        {
            _attacker.Impact(enemyHealth);
            _healer.Impact(_playerHealth);
        }
    }

    private List<Health> GetEnemiesHealth() =>
        Physics2D.OverlapBoxAll(transform.position, _range, 0f)
             .Where(hit => hit.GetComponent<Enemy>() != null)
             .Select(enemy => enemy.GetComponent<Health>())
             .ToList();
}
