using UnityEngine;
using UnityEngine.UI;

public class SimpleHealthBar : HealthBarBase
{
    [SerializeField] private Image _bar;

    protected override void UpdateView() =>
        _bar.fillAmount = _health.PublicCurrentHealth.CurrentValue / _health.PublicMaxHealth.CurrentValue;
}
