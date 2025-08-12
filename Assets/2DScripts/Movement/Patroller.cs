using UnityEngine;
using System.Collections;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;

    [SerializeField] private Mover _mover;

    private Coroutine _patrollCoroutine;

    private int _currentWayPointIndex = 0;

    private float _waypointArrivalThreshold = 0.2f;
    private float _switchDelay = 1f;

    private void Start()
    {
        TryStartPatrolling();
    }

    public void TryStartPatrolling()
    {
        if (_patrollCoroutine == null)
        {
            _patrollCoroutine = StartCoroutine(MoveToWayPoints());
        }
    }

    public void TryStopPatrolling()
    {
        if (_patrollCoroutine != null)
        {
            StopCoroutine(_patrollCoroutine);
            _patrollCoroutine = null;
        }
    }

    private IEnumerator MoveToWayPoints()
    {
        while (enabled)
        {
            if (Mathf.Abs(_wayPoints[_currentWayPointIndex].position.x - transform.position.x) <= _waypointArrivalThreshold)
            {
                SwitchWayPoint();
                yield return new WaitForSecondsRealtime(_switchDelay);
            }

            _mover.Move(GetDirection());

            yield return null;
        }
    }

    private Vector2 GetDirection() =>
         (_wayPoints[_currentWayPointIndex].position - transform.position).normalized;

    private void SwitchWayPoint() =>
        _currentWayPointIndex = ++_currentWayPointIndex % _wayPoints.Length;
}