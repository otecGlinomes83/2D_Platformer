using Assets._2DScripts.Interactions.HealthInteractors;
using UnityEngine;

[RequireComponent(typeof(Pusher))]
public class Attacker : HealthImpactor
{
    private Pusher _pusher;

    private void Awake()
    {
        _pusher = GetComponent<Pusher>();
    }

    public override void Impact(Health health)
    {
        health.TakeDamage(_impactValue);

        if (health.TryGetComponent(out Rigidbody2D _targetRigidbody))
            _pusher.Push(_targetRigidbody);
    }
}
