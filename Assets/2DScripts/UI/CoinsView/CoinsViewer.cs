using R3;
using TMPro;
using UnityEngine;

public class CoinsViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Wallet _wallet;

    private CompositeDisposable _disposables = new CompositeDisposable();

    private void Awake()
    {
        _wallet.CoinsCount
            .Subscribe(newValue => _text.text = newValue.ToString())
            .AddTo(_disposables);
    }

    private void OnDisable()
    {
        _disposables.Dispose();
    }
}
