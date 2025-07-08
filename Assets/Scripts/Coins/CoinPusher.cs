using UnityEngine;

public class CoinPusher
{
    private float _minVelocityX = -2f;
    private float _maxVelocityX = 2f;
    private float _velocityY = 6f;

    public void Push(Coin coin)
    {
        coin.Rigidbody.linearVelocity = new Vector2(UnityEngine.Random.Range(_minVelocityX, _maxVelocityX), _velocityY);
    }
}
