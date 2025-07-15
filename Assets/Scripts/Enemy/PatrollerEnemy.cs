using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PatrollerEnemy : MonoBehaviour
{
    [SerializeField] private Patroller _patroller;
    [SerializeField] private Rotator _rotator;
}