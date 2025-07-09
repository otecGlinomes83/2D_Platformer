using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Mover), typeof(Jumper))]
[RequireComponent(typeof(CharacterAnimator))]
public class Creature : MonoBehaviour
{
    protected Jumper Jumper;
    protected Mover Mover;
    protected Rigidbody2D Rigidbody;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Jumper = gameObject.GetComponent<Jumper>();
        Mover = gameObject.GetComponent<Mover>();
    }
}
