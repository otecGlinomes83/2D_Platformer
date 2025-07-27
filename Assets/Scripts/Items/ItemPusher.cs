using UnityEngine;

public class ItemPusher : MonoBehaviour
{
    private float _minVelocityX = -2f;
    private float _maxVelocityX = 2f;
    private float _velocityY = 6f;

    public void Push(Item item) =>
        item.Rigidbody.linearVelocity = new Vector2(Random.Range(_minVelocityX, _maxVelocityX), _velocityY);
}
