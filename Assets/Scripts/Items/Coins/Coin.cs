using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Coin : Item
{
    protected override void Awake() { base.Awake(); }
}