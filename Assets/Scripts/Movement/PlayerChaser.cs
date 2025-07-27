using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
public class PlayerChaser : MonoBehaviour
{
    [SerializeField] private float _jumpThreshold = 2f;

    private Mover _mover;
    private Jumper _jumper;

    private Coroutine _chaseCoroutine;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
    }

    public void TryStartChase(Player player)
    {
        if (_chaseCoroutine == null)
            _chaseCoroutine = StartCoroutine(ChasePlayer(player));
    }

    public void TryStopChase()
    {
        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
        }
    }

    private IEnumerator ChasePlayer(Player player)
    {
        while (enabled)
        {
            if (player.IsAlive == false)
                TryStopChase();

            _mover.Move(new Vector2(GetDirection(player).x, 0f));

            if (player.transform.position.y > (transform.position.y + _jumpThreshold))
                _jumper.Jump();

            yield return null;
        }
    }

    private Vector2 GetDirection(Player player) =>
        (player.transform.position - transform.position);
}
