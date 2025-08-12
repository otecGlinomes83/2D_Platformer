using R3;
using UnityEngine;

public abstract class HealthBarBase : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected CompositeDisposable _disposable = new CompositeDisposable();

    protected virtual void Awake()
    {
        _health.PublicCurrentHealth.CombineLatest(_health.PublicMaxHealth, (current, max) => (current, max)).Subscribe(_ => UpdateView()).AddTo(_disposable);
    }

    protected virtual void OnDisable() =>
        _disposable?.Dispose();

    protected abstract void UpdateView();
}
