using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Mover))]
public class CharacterAnimator : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));

    private Animator _animator;

    private Mover _mover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _animator.SetBool(IsRunning, _mover.IsRunning);
    }
}