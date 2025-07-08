using System.Collections;
using UnityEngine;

public class Enemy : MobileEntity
{
    [SerializeField] private Transform[] _wayPoints;

    private int _currentWayPointIndex = 0;

    private float _waypointArrivalThreshold = 0.2f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(MoveToWayPoints());
    }

    private IEnumerator MoveToWayPoints()
    {
        while (enabled)
        {
            if (Mathf.Abs(transform.position.x - _wayPoints[_currentWayPointIndex].position.x) <= _waypointArrivalThreshold)
            {
                SwitchWayPoint();
            }

            Mover.Move(GetDirection());

            yield return null;
        }
    }

    private Vector2 GetDirection()
    {
        float deltaX = _wayPoints[_currentWayPointIndex].position.x - transform.position.x;

        return new Vector2(Mathf.Sign(deltaX), 0);
    }

    private void SwitchWayPoint()
    {
        _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length;
    }
}