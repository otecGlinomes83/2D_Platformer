using R3;
using UnityEngine;

public class Wallet:MonoBehaviour
{
    private ReactiveProperty<int> _coinsCount = new ReactiveProperty<int>();
    public ReadOnlyReactiveProperty<int> CoinsCount => _coinsCount.ToReadOnlyReactiveProperty();

    public void AddCoins(int count)
    {
        if (count > 0)
            _coinsCount.Value += count;
    }
}
