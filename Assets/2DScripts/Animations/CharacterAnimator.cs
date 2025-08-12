using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    private static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool(IsRunning, _mover.IsRunning);
    }
}