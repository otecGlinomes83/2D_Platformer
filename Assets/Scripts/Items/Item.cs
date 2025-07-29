using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public abstract class Item : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public abstract void Use(Player player);
}