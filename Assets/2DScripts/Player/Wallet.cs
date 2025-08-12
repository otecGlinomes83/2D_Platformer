public class Wallet
{
    public int CoinsCount { get; private set; } = 0;

    public void AddCoins(int count)
    {
        if (count > 0)
            CoinsCount += count;
    }
}
