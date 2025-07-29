using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
public class PlayerChaser : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private float _jumpThreshold = 2f;

    private Mover _mover;
    private Jumper _jumper;

    private Coroutine _chaseCoroutine;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
    }

    public void TryStartChase()
    {
        if (_chaseCoroutine == null)
            _chaseCoroutine = StartCoroutine(ChasePlayer());
    }

    public void TryStopChase()
    {
        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
        }
    }

    private IEnumerator ChasePlayer()
    {
        while (enabled)
        {
            if (_player.Health.IsAlive == false)
                TryStopChase();

            _mover.Move(GetDirectionX(_player));

            if (_player.transform.position.y > (transform.position.y + _jumpThreshold))
                _jumper.Jump();

            yield return null;
        }
    }

    private Vector2 GetDirectionX(Player player) =>
      new Vector2(player.transform.position.x - transform.position.x, 0f);
}
