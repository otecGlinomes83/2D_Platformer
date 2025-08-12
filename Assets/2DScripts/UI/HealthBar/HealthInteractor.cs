using UnityEngine;
using UnityEngine.UI;

public abstract class HealthInteractor : MonoBehaviour
{
    [SerializeField] protected Button _button;
    [SerializeField] protected Health _health;
    [SerializeField] protected float _value;

    private void OnEnable() =>
        _button.onClick.AddListener(InteractHealth);

    private void OnDisable() =>
        _button.onClick.RemoveListener(InteractHealth);

    protected abstract void InteractHealth();
}
