using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : HealthBarBase
{
    [SerializeField] private Image _bar;

    [SerializeField] private float _barChangingSpeed = 0.25f;
    [SerializeField] private float _barChangingDelay = 0.25f;

    private Coroutine _smoothChangeBarCoroutine;
    private WaitForSecondsRealtime _delay;

    private float _defaultBarAmount = 1f;

    protected override void Awake()
    {
        base.Awake();
        _delay = new WaitForSecondsRealtime(_barChangingDelay);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _health.Dead -= SetBarToDefault;
    }

    protected override void UpdateView()
    {
        if (_smoothChangeBarCoroutine != null)
        {
            StopCoroutine(_smoothChangeBarCoroutine);
            _smoothChangeBarCoroutine = null;
        }

        _smoothChangeBarCoroutine = StartCoroutine(SmoothChangeBar());
    }

    private IEnumerator SmoothChangeBar()
    {
        float targetFill = _health.PublicCurrentHealth.CurrentValue / _health.PublicMaxHealth.CurrentValue;

        AdjustBar(targetFill);

        yield return _delay;

        while (Mathf.Approximately(_bar.fillAmount, targetFill) == false)
        {
            targetFill = _health.PublicCurrentHealth.CurrentValue / _health.PublicMaxHealth.CurrentValue;

            AdjustBar(targetFill);

            _bar.fillAmount = Mathf.MoveTowards(_bar.fillAmount, targetFill, _barChangingSpeed * Time.unscaledDeltaTime);

            yield return null;
        }

        _smoothChangeBarCoroutine = null;
    }

    private void AdjustBar(float targetFill)
    {
        if (_bar.fillAmount < targetFill)
            _bar.fillAmount = targetFill;
    }

    private void SetBarToDefault() =>
    _bar.fillAmount = _defaultBarAmount;
}
