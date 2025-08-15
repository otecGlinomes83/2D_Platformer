using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public abstract class Item : MonoBehaviour
{
    public event Action<Item> Collected;

    public Rigidbody2D Rigidbody { get; private set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public abstract void Use(Player player);

    public virtual void Collect()
    {
        Collected?.Invoke(this);
    }
}