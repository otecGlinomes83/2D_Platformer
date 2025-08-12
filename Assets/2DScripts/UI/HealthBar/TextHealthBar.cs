using TMPro;
using UnityEngine;

public class TextHealthBar : HealthBarBase
{
    [SerializeField] private TMP_Text _textMeshPro;

    protected override void UpdateView() =>
         _textMeshPro.text = $"HP [{_health.PublicCurrentHealth.CurrentValue}/{_health.PublicMaxHealth.CurrentValue}]";
}
